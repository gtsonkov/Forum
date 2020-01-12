using Microsoft.AspNetCore.Mvc;

namespace Forum.Controllers
{
    public class CommentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}