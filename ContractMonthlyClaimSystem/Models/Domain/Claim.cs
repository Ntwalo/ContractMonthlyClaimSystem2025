using System;
using System.Collections.Generic;

namespace ContractMonthlyClaimSystem.Models.Domain
{
    public class Claim
    {
        public Guid ClaimId { get; set; } = Guid.NewGuid();
        public Guid LecturerId { get; set; }
        public decimal HoursWorked { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal TotalAmount { get; set; } // snapshot at submission
        public ClaimStatus Status { get; set; } = ClaimStatus.Pending;
        public DateTime SubmissionDate { get; set; } = DateTime.UtcNow;
        public string Notes { get; set; } = "";
        public List<Document> Documents { get; set; } = new();
    }
}
