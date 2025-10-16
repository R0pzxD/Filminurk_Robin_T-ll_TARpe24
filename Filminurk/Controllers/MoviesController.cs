using Filminurk.Core.Dto;
using Filminurk.Data;
using Filminurk.Models.Movies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Filminurk.Controllers
{
    public class MoviesController : Controller
    {
        private readonly FilminurkTARpe24Context _context;
        public MoviesController 
            (
            FilminurkTARpe24Context context,
            )
        {
            _context = context;
            _movieServices = movieServices;
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
        [HttpPost]
        public async Task<IActionResult> Create(MoviesCreateViewModel vm)
        {
            var dto = new MoviesDTO()
            {
                ID = vm.ID,
                Title= vm.Title,
                Description = vm.Description,
                FirstPublished= vm.FirstPublished,
                Director = vm.Director,
                Actors = vm.Actors,
                CurrentRating= vm.CurrentRating,
                OscarsWon= vm.OscarsWon,
                RottenTomatoes = vm.RottenTomatoes,
                IMDbRating= vm.IMDbRating,
                EntryCreatedAt= vm.EntryCreatedAt,
                EntryModifyAt= vm.EntryModifyAt,


            };
            var result = await _movieServices.Create(dto);
            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult>Delete(Guid id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var result = await _context.Movies
                .FirstOrDefaultAsync(m => m.ID == id);

        }
    }
}

