using System.IO;
using Filminurk.ApplicationServices.Services;
using Filminurk.Core.Dto;
using Filminurk.Core.ServiceInterface;
using Filminurk.Data;
using Filminurk.Models;
using Filminurk.Models.Movies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Filminurk.Controllers
{
    public class MoviesController : Controller
    {
        private readonly FilminurkTARpe24Context _context;
        private readonly IMovieServices _movieServices;
        private readonly IFilesServices _filesServices;
        public MoviesController 
            (
            FilminurkTARpe24Context context,
            IMovieServices movieServices
,
            IFilesServices filesServices
            )
        {
            _context = context;
            _movieServices = movieServices;
            _filesServices = filesServices;
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
                Files = vm.Files,
                FilesToApi = vm.Images
                .Select(X => new FileToApiDTO
                {
                    ImageID = X.ImageID,
                    FilePath = X.FilePath,
                    MovieID = X.MovieID,
                    isPoster = X.isPoster,
                }).ToArray()


            };
            var result = await _movieServices.Create(dto);
            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var movie = await _movieServices.DetailsAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            var images = await _context.FilesToApi
                .Where(x => x.MovieID == id)
                .Select(y => new ImageViewModel
                {
                    FilePath = y.ExistingFilePath,
                    ImageID = id
                }).ToListAsync();

            var vm = new MoviesCreateViewModel()
            {
                ID = movie.ID,
                Title = movie.Title,
                Description = movie.Description,
                FirstPublished = movie.FirstPublished,
                Director = movie.Director,
                Actors = movie.Actors,
                CurrentRating = movie.CurrentRating,
                OscarsWon = movie.OscarsWon,
                RottenTomatoes = movie.RottenTomatoes,
                IMDbRating = movie.IMDbRating,
                EntryCreatedAt = movie.EntryCreatedAt,
                EntryModifyAt = movie.EntryModifiedAt,
                Images = images


            };
            return View("CreateUpdate", vm);
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
            return View(result);
        }
    }
}

