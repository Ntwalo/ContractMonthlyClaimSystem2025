using ContractMonthlyClaimSystem.Models.Domain;
using ContractMonthlyClaimSystem.Services.InMemory;
using System;
using Xunit;

namespace ContractMonthlyClaimSystem.Tests.Services
{
    public class ClaimServiceTests
    {
        [Fact]
        public void CreateClaim_ComputesTotal_AndPending()
        {
            var svc = new InMemoryClaimService();
            var claim = svc.CreateClaim(Guid.NewGuid(), 8m, 500m, "Test");

            Assert.Equal(4000m, claim.TotalAmount);
            Assert.Equal(ClaimStatus.Pending, claim.Status);
        }

        [Fact]
        public void Approve_ChangesStatus()
        {
            var svc = new InMemoryClaimService();
            var c = svc.CreateClaim(Guid.NewGuid(), 8m, 500m, "");

            svc.Approve(c.ClaimId, "ok");

            Assert.Equal(ClaimStatus.Approved, svc.GetById(c.ClaimId)!.Status);
        }

        [Fact]
        public void Reject_ChangesStatus()
        {
            var svc = new InMemoryClaimService();
            var c = svc.CreateClaim(Guid.NewGuid(), 8m, 500m, "");

            svc.Reject(c.ClaimId, "bad");

            Assert.Equal(ClaimStatus.Rejected, svc.GetById(c.ClaimId)!.Status);
        }

        [Fact]
        public void CreateClaim_WithNegativeHours_Throws()
        {
            var svc = new InMemoryClaimService();

            Assert.Throws<ArgumentException>(() =>
            {
                svc.CreateClaim(Guid.NewGuid(), -5m, 100m, "Invalid");
            });
        }

        [Fact]
        public void CreateClaim_WithNegativeRate_Throws()
        {
            var svc = new InMemoryClaimService();

            Assert.Throws<ArgumentException>(() =>
            {
                svc.CreateClaim(Guid.NewGuid(), 5m, -100m, "Invalid");
            });
        }
    }
}
