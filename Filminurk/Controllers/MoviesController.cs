using Filminurk.Data;
using Filminurk.Models.Movies;
using Microsoft.AspNetCore.Mvc;

namespace Filminurk.Controllers
{
    public class MoviesController : Controller
    {
        private readonly FilminurkTARpe24Context _context;
        public MoviesController (FilminurkTARpe24Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var result = _context.Movies.Select(x => new MoviesIndexViewModel
            {
                ID = x.ID,
                Title = x.Title,
                FirstPublished = x.FirstPublished,
                CurrentRating = x.CurrentRating,
                OscarsWon = x.OscarsWon,
                IMDbRating = x.IMDbRating,
                
            });
            return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            MoviesIndexViewModel result = new();
            return View("Create", result);
        }
    }
}

