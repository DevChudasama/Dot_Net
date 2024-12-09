using Microsoft.AspNetCore.Mvc;

namespace Tea_post.Areas.Website.Controllers
{
    [Area("Website")]
    [Route("Website/[Controller]/[Action]")]
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View("_Contact");
        }
    }
}
