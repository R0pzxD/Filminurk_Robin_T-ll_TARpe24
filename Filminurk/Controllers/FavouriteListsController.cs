using Microsoft.AspNetCore.Mvc;

namespace Filminurk.Controllers
{
    public class FavouriteListsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
