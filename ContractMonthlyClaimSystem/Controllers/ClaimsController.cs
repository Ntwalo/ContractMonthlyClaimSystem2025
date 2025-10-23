using System;
using System.Linq;
using ContractMonthlyClaimSystem.Infrastructure.FileStorage;
using ContractMonthlyClaimSystem.Models.Domain;
using ContractMonthlyClaimSystem.Models.ViewModels;
using ContractMonthlyClaimSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ContractMonthlyClaimSystem.Controllers
{
    public class ClaimsController : Controller
    {
        private readonly IClaimService _claims;
        private readonly ILecturerService _lecturers;
        private readonly IDocumentService _docs;
        private readonly LocalFileStorage _files;

        public ClaimsController(IClaimService claims, ILecturerService lecturers, IDocumentService docs, LocalFileStorage files)
        {
            _claims = claims;
            _lecturers = lecturers;
            _docs = docs;
            _files = files;
        }

        // For demo: seed a lecturer and use that ID as current user
        private Guid EnsureDemoLecturer()
        {
            var lec = _lecturers.SeedDefaultLecturer();
            return lec.LecturerId;
        }

        public IActionResult Index()
        {
            var lecturerId = EnsureDemoLecturer();
            var lecturer = _lecturers.GetById(lecturerId)!;
            var list = _claims.GetClaimsForLecturer(lecturerId)
                .Select(c => new ClaimListItemViewModel
                {
                    ClaimId = c.ClaimId,
                    SubmissionDate = c.SubmissionDate,
                    HoursWorked = c.HoursWorked,
                    HourlyRate = c.HourlyRate,
                    Total = c.TotalAmount,
                    Status = c.Status,
                    LecturerName = lecturer.Name
                }).ToList();
            return View(list);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new ClaimCreateViewModel { HourlyRate = _lecturers.SeedDefaultLecturer().HourlyRate });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ClaimCreateViewModel vm)
        {
            var lecturerId = EnsureDemoLecturer();

            if (!ModelState.IsValid)
                return View(vm);

            try
            {
                var claim = _claims.CreateClaim(lecturerId, vm.HoursWorked, vm.HourlyRate, vm.Notes);

                if (vm.SupportingDocument != null && vm.SupportingDocument.Length > 0)
                {
                    // Validate file size/type
                    var allowed = new[] { ".pdf", ".docx", ".xlsx" };
                    var ext = System.IO.Path.GetExtension(vm.SupportingDocument.FileName).ToLowerInvariant();
                    if (!allowed.Contains(ext))
                        throw new ArgumentException("Invalid file type. Allowed: .pdf, .docx, .xlsx.");
                    if (vm.SupportingDocument.Length > 5 * 1024 * 1024)
                        throw new ArgumentException("File too large. Max 5 MB.");

                    var path = _files.Save(vm.SupportingDocument);
                    var doc = _docs.BuildDocumentForClaim(claim, vm.SupportingDocument.FileName, path);
                    _claims.AttachDocument(claim.ClaimId, doc);
                }

                TempData["Success"] = "Claim submitted successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(vm);
            }
        }

        public IActionResult Details(Guid id)
        {
            var claim = _claims.GetById(id);
            if (claim == null) return NotFound();

            var lecturer = _lecturers.GetById(claim.LecturerId)!;

            var vm = new ClaimDetailViewModel
            {
                ClaimId = claim.ClaimId,
                LecturerName = lecturer.Name,
                SubmissionDate = claim.SubmissionDate,
                HoursWorked = claim.HoursWorked,
                HourlyRate = claim.HourlyRate,
                Total = claim.TotalAmount,
                Status = claim.Status,
                Notes = claim.Notes,
                Documents = claim.Documents
            };
            return View(vm);
        }
    }
}
