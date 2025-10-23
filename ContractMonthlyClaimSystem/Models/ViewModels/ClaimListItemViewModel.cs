using System;
using ContractMonthlyClaimSystem.Models.Domain;

namespace ContractMonthlyClaimSystem.Models.ViewModels
{
    public class ClaimListItemViewModel
    {
        public Guid ClaimId { get; set; }
        public DateTime SubmissionDate { get; set; }
        public decimal HoursWorked { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal Total { get; set; }
        public ClaimStatus Status { get; set; }
        public string LecturerName { get; set; } = "";
    }
}
