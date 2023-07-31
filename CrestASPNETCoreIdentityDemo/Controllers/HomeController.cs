using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using CrestASPNETCoreIdentityDemo.Pages.Account.Register;
using CrestASPNETCoreIdentityDemo.Models;

namespace CrestASPNETCoreIdentityDemo.Controllers
{
    [Route("api/home")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly UserManager<CrestUser> userManager;

        public HomeController(UserManager<CrestUser> userManager)
        {
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult About()
        {
            ViewData["Message"] = "Application Description Page";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Contact Page";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(model.Username);

                if (user == null)
                {
                    user = new CrestUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        Username = model.Username // Create method should generate normalised username
                    };

                    var result = await userManager.CreateAsync(user, model.Password); // Create method should hash password
                }

                return View("Success");
            }

            return View();
        }
    }
}
