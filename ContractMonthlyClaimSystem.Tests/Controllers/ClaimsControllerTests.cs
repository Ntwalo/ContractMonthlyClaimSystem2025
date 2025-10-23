using System;
using ContractMonthlyClaimSystem.Controllers;
using ContractMonthlyClaimSystem.Infrastructure.FileStorage;
using ContractMonthlyClaimSystem.Models.ViewModels;
using ContractMonthlyClaimSystem.Services.InMemory;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ContractMonthlyClaimSystem.Tests.Controllers
{
    public class ClaimsControllerTests
    {
        [Fact]
        public void Create_InvalidModel_ReturnsView()
        {
            var claimSvc = new InMemoryClaimService();
            var lecSvc = new InMemoryLecturerService();
            var docSvc = new InMemoryDocumentService();
            var env = new Mock<IWebHostEnvironment>();
            env.Setup(e => e.WebRootPath).Returns(System.IO.Path.GetTempPath());
            var files = new LocalFileStorage(env.Object);
            var ctrl = new ClaimsController(claimSvc, lecSvc, docSvc, files);
            ctrl.ModelState.AddModelError("HoursWorked", "Required");

            var result = ctrl.Create(new ClaimCreateViewModel());
            Assert.IsType<ViewResult>(result);
        }
    }
}
