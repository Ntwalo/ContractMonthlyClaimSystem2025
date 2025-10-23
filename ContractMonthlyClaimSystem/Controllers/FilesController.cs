using System;
using ContractMonthlyClaimSystem.Infrastructure.FileStorage;
using ContractMonthlyClaimSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ContractMonthlyClaimSystem.Controllers
{
    public class FilesController : Controller
    {
        private readonly IClaimService _claims;
        private readonly IDocumentService _docs;
        private readonly LocalFileStorage _files;

        public FilesController(IClaimService claims, IDocumentService docs, LocalFileStorage files)
        {
            _claims = claims;
            _docs = docs;
            _files = files;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upload(Guid claimId, IFormFile file)
        {
            try
            {
                var claim = _claims.GetById(claimId);
                if (claim == null) return NotFound();

                var allowed = new[] { ".pdf", ".docx", ".xlsx" };
                var ext = System.IO.Path.GetExtension(file.FileName).ToLowerInvariant();
                if (!allowed.Contains(ext))
                    throw new ArgumentException("Invalid file type. Allowed: .pdf, .docx, .xlsx.");
                if (file.Length > 5 * 1024 * 1024)
                    throw new ArgumentException("File too large. Max 5 MB.");

                var path = _files.Save(file);
                var doc = _docs.BuildDocumentForClaim(claim, file.FileName, path);
                _claims.AttachDocument(claimId, doc);

                TempData["Success"] = "File uploaded.";
                return RedirectToAction("Details", "Claims", new { id = claimId });
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Details", "Claims", new { id = claimId });
            }
        }
    }
}
