using System;
using System.Collections.Generic;
using ContractMonthlyClaimSystem.Models.Domain;

namespace ContractMonthlyClaimSystem.Services.Interfaces
{
    public interface IClaimService
    {
        Claim CreateClaim(Guid lecturerId, decimal hoursWorked, decimal hourlyRate, string notes);
        IEnumerable<Claim> GetClaimsForLecturer(Guid lecturerId);
        IEnumerable<Claim> GetAllPendingClaims();
        Claim? GetById(Guid claimId);
        void Approve(Guid claimId, string? comment = null);
        void Reject(Guid claimId, string? comment = null);
        void AttachDocument(Guid claimId, Document doc);
    }
}
