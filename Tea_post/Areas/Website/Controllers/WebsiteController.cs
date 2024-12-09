using Microsoft.AspNetCore.Mvc;

namespace Tea_post.Areas.Website.Controllers
{
    [Area("Website")]
    [Route("Website/[controller]/[Action]")]
    public class WebsiteController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
