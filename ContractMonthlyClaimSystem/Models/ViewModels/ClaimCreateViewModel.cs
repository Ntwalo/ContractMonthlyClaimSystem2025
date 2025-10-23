using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace ContractMonthlyClaimSystem.Models.ViewModels
{
    public class ClaimCreateViewModel
    {
        [Required, Range(0.1, 1000)]
        public decimal HoursWorked { get; set; }

        [Required, Range(0.1, 10000)]
        public decimal HourlyRate { get; set; }

        [MaxLength(500)]
        public string Notes { get; set; } = "";

        // optional initial file upload
        public IFormFile? SupportingDocument { get; set; }
    }
}
