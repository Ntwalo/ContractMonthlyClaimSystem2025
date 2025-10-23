using ContractMonthlyClaimSystem.Models.Domain;
using ContractMonthlyClaimSystem.Services.Interfaces;

namespace ContractMonthlyClaimSystem.Services.InMemory
{
    public class InMemoryDocumentService : IDocumentService
    {
        public Document BuildDocumentForClaim(Claim claim, string fileName, string filePath)
        {
            return new Document
            {
                ClaimId = claim.ClaimId,
                FileName = fileName,
                FilePath = filePath
            };
        }
    }
}
