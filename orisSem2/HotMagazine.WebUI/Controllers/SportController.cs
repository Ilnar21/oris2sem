using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotMagazine.WebUI.Controllers
{
    [Authorize]
    public class SportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}