// Controllers/HomeController.cs
using Microsoft.AspNetCore.Mvc;

namespace ContractMonthlyClaimSystem.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }
}
