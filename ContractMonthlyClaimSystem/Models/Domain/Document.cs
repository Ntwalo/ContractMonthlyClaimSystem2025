using System;

namespace ContractMonthlyClaimSystem.Models.Domain
{
    public class Document
    {
        public Guid DocumentId { get; set; } = Guid.NewGuid();
        public Guid ClaimId { get; set; }
        public string FileName { get; set; } = "";
        public string FilePath { get; set; } = ""; // physical path under wwwroot/uploads/documents
        public DateTime UploadDate { get; set; } = DateTime.UtcNow;
    }
}
