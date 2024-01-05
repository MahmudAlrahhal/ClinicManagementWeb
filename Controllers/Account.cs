using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementWeb.Controllers
{
    public class Account : Controller
    {
        public IActionResult Index(string Email, String Password)
        {
            return View();
        }
    }
}
