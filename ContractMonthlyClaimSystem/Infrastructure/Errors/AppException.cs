using System;

namespace ContractMonthlyClaimSystem.Infrastructure.Errors
{
    public class AppException : Exception
    {
        public AppException(string message) : base(message) { }
    }
}
