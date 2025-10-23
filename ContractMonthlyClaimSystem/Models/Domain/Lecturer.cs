using System;

namespace ContractMonthlyClaimSystem.Models.Domain
{
    public class Lecturer
    {
        public Guid LecturerId { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public decimal HourlyRate { get; set; }
        public string BankDetails { get; set; } = "";
    }
}
