using System;
using ContractMonthlyClaimSystem.Models.Domain;
using ContractMonthlyClaimSystem.Validation;
using Xunit;

namespace ContractMonthlyClaimSystem.Tests.Validation
{
    public class ClaimValidatorTests
    {
        [Fact]
        public void ValidateForSubmission_Throws_OnNegativeHours()
        {
            var c = new Claim { HoursWorked = -1, HourlyRate = 100, TotalAmount = -100 };
            Assert.Throws<ArgumentException>(() => ClaimValidator.ValidateForSubmission(c));
        }

        [Fact]
        public void ValidateForSubmission_Throws_WhenTotalMismatch()
        {
            var c = new Claim { HoursWorked = 2, HourlyRate = 100, TotalAmount = 150 };
            Assert.Throws<ArgumentException>(() => ClaimValidator.ValidateForSubmission(c));
        }

        [Fact]
        public void ValidateForSubmission_Passes_OnCorrectTotals()
        {
            var c = new Claim { HoursWorked = 2, HourlyRate = 100, TotalAmount = 200 };
            ClaimValidator.ValidateForSubmission(c);
        }
    }
}
