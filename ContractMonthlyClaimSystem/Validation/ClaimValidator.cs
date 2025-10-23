using ContractMonthlyClaimSystem.Models.Domain;

namespace ContractMonthlyClaimSystem.Validation
{
    public static class ClaimValidator
    {
        public static void ValidateForSubmission(Claim claim)
        {
            if (claim.HoursWorked <= 0)
                throw new ArgumentException("HoursWorked must be positive.");
            if (claim.HourlyRate <= 0)
                throw new ArgumentException("HourlyRate must be positive.");
            if (claim.TotalAmount != claim.HoursWorked * claim.HourlyRate)
                throw new ArgumentException("TotalAmount must equal HoursWorked * HourlyRate.");
        }
    }
}
