using System;
using System.ComponentModel.DataAnnotations;

namespace ContractMonthlyClaimSystem.Models.ViewModels
{
    public class DecisionViewModel
    {
        [Required]
        public Guid ClaimId { get; set; }

        [MaxLength(300)]
        public string? Comment { get; set; }
    }
}
