using System;
using System.Linq;
using ContractMonthlyClaimSystem.Models.ViewModels;
using ContractMonthlyClaimSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ContractMonthlyClaimSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly IClaimService _claims;
        private readonly ILecturerService _lecturers;

        public AdminController(IClaimService claims, ILecturerService lecturers)
        {
            _claims = claims;
            _lecturers = lecturers;
        }

        public IActionResult Index()
        {
            var items = _claims.GetAllPendingClaims().Select(c =>
            {
                var lec = _lecturers.GetById(c.LecturerId)!;
                return new ClaimListItemViewModel
                {
                    ClaimId = c.ClaimId,
                    SubmissionDate = c.SubmissionDate,
                    HoursWorked = c.HoursWorked,
                    HourlyRate = c.HourlyRate,
                    Total = c.TotalAmount,
                    Status = c.Status,
                    LecturerName = lec.Name
                };
            }).ToList();

            return View(items);
        }

        public IActionResult Details(Guid id)
        {
            var c = _claims.GetById(id);
            if (c == null) return NotFound();
            var lec = _lecturers.GetById(c.LecturerId)!;

            var vm = new ClaimDetailViewModel
            {
                ClaimId = c.ClaimId,
                LecturerName = lec.Name,
                SubmissionDate = c.SubmissionDate,
                HoursWorked = c.HoursWorked,
                HourlyRate = c.HourlyRate,
                Total = c.TotalAmount,
                Status = c.Status,
                Notes = c.Notes,
                Documents = c.Documents
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Approve(DecisionViewModel vm)
        {
            try
            {
                _claims.Approve(vm.ClaimId, vm.Comment);
                TempData["Success"] = "Claim approved.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Reject(DecisionViewModel vm)
        {
            try
            {
                _claims.Reject(vm.ClaimId, vm.Comment);
                TempData["Success"] = "Claim rejected.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
