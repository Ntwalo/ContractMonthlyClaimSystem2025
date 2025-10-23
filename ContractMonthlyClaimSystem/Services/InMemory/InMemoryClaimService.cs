using System;
using System.Collections.Generic;
using System.Linq;
using ContractMonthlyClaimSystem.Models.Domain;
using ContractMonthlyClaimSystem.Services.Interfaces;
using ContractMonthlyClaimSystem.Validation;

namespace ContractMonthlyClaimSystem.Services.InMemory
{
    public class InMemoryClaimService : IClaimService
    {
        private readonly List<Claim> _claims = new();

        public Claim CreateClaim(Guid lecturerId, decimal hoursWorked, decimal hourlyRate, string notes)
        {
            var claim = new Claim
            {
                LecturerId = lecturerId,
                HoursWorked = hoursWorked,
                HourlyRate = hourlyRate,
                TotalAmount = hoursWorked * hourlyRate,
                Notes = notes ?? ""
            };
            ClaimValidator.ValidateForSubmission(claim);
            _claims.Add(claim);
            return claim;
        }

        public IEnumerable<Claim> GetClaimsForLecturer(Guid lecturerId) =>
            _claims.Where(c => c.LecturerId == lecturerId).OrderByDescending(c => c.SubmissionDate);

        public IEnumerable<Claim> GetAllPendingClaims() =>
            _claims.Where(c => c.Status == ClaimStatus.Pending).OrderByDescending(c => c.SubmissionDate);

        public Claim? GetById(Guid claimId) => _claims.FirstOrDefault(c => c.ClaimId == claimId);

        public void Approve(Guid claimId, string? comment = null)
        {
            var c = GetById(claimId) ?? throw new ArgumentException("Claim not found.");
            c.Status = ClaimStatus.Approved;
            if (!string.IsNullOrWhiteSpace(comment))
                c.Notes = (c.Notes ?? "") + $" | Approved: {comment}";
        }

        public void Reject(Guid claimId, string? comment = null)
        {
            var c = GetById(claimId) ?? throw new ArgumentException("Claim not found.");
            c.Status = ClaimStatus.Rejected;
            if (!string.IsNullOrWhiteSpace(comment))
                c.Notes = (c.Notes ?? "") + $" | Rejected: {comment}";
        }

        public void AttachDocument(Guid claimId, Document doc)
        {
            var c = GetById(claimId) ?? throw new ArgumentException("Claim not found.");
            c.Documents.Add(doc);
        }
    }
}
