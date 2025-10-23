using ContractMonthlyClaimSystem.Models.Domain;

namespace ContractMonthlyClaimSystem.Services.Interfaces
{
    public interface IDocumentService
    {
        Document BuildDocumentForClaim(Claim claim, string fileName, string filePath);
    }
}
