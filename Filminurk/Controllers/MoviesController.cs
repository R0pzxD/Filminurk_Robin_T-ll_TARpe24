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
            IMovieServices movieServices,
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
        public async Task<IActionResult> Create(Models.Movies.MoviesCreateUpdateViewModel vm)
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
                return NotFound();
            }
            if (ModelState.IsValid )
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
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
                }).ToArrayAsync();

            var vm = new Models.Movies.MoviesCreateUpdateViewModel()
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
            };
            vm.Images.AddRange(images);
            return View("CreateUpdate", vm);
        }

        //[HttpPost]
        //public async Task<IActionResult> Update(Models.MoviesCreateUpdateViewModel vm)
        //{
        //    var dto = new MoviesDTO()
        //    {
        //        ID = vm.ID,
        //        Title = vm.Title,
        //        Description = vm.Description,
        //        FirstPublished = vm.FirstPublished,
        //        CurrentRating = vm.CurrentRating,
        //        OscarsWon = vm.OscarsWon,
        //        RottenTomatoes = vm.RottenTomatoes,
        //        IMDbRating = vm.IMDbRating,
        //        EntryCreatedAt = vm.EntryCreatedAt,
        //        EntryModifyAt = vm.EntryModifyAt,
        //        Director = vm.Director,
        //        Actors = vm.Actors,
        //        Files = vm.Files,
        //        FilesToApi = vm.Images
        //        .Select(x => new FileToApiDTO
        //        {
        //            ImageID = x.ImageID,
        //            MovieID = x.MovieID,
        //            FilePath = x.FilePath
        //        }).ToArray()
        //    };
        //    var result = null; /*await _movieServices.Update(dto);*/

        //    if (result == null)
        //    {
        //        return NotFound();
        //    }
        //    return RedirectToAction(nameof(Index));
        //}


        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var movie = await _movieServices.DetailsAsync(id);

            if (movie == null)
            {
                return NotFound();
            }
            ImageViewModel[] images = await FileFromDatabase(id);

            var vm = new MoviesDetailsViewModel();

            vm.ID = movie.ID;

            vm.EntryModifyAt = movie.EntryModifiedAt;
            vm.Director = movie.Director;
            vm.Actors = movie.Actors;
            vm.Images.AddRange(images);
            
            return View(vm);
        }


        private async Task<ImageViewModel[]> FileFromDatabase(Guid id)
        {
            return await _context.FilesToApi
                .Where(x => x.MovieID == id)
                .Select(y => new ImageViewModel { 
                    ImageID = y.ImageID,
                    MovieID = y.MovieID,
                    isPoster = y.isPoster,
                    FilePath = y.ExistingFilePath
                }).ToArrayAsync();
        }


        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
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
                    ImageID = y.ImageID,
                }).ToArrayAsync();
            var vm = new MoviesDeleteViewModel();
            vm.ID = vm.ID;
                vm.Title = vm.Title;
            vm.Description = vm.Description;
            vm.FirstPublished = vm.FirstPublished;
            vm.CurrentRating = vm.CurrentRating;
            vm.OscarsWon = vm.OscarsWon;
            vm.RottenTomatoes = vm.RottenTomatoes;
            vm.IMDbRating = vm.IMDbRating;
            vm.EntryCreatedAt = vm.EntryCreatedAt;
            vm.EntryModifyAt = vm.EntryModifyAt;
                vm.Director = vm.Director;
            vm.Actors = vm.Actors;
            vm.Images.AddRange(images);

            return View(vm);

        }

        //[HttpPost]
        //public async Task<IActionResult> DeleteConfirmation(Guid id)
        //{
        //    var movie = await _movieServices.Delete(id);
        //    if (movie == null)
        //    {
        //        return NotFound();
        //    }
        //    return RedirectToAction("Index");

        //}
        private async Task<ImageViewModel[]> FileFormDatabase(Guid id)
        {
            return await _context.FilesToApi
               .Where(x => x.MovieID == id)
               .Select(y => new ImageViewModel
               {
                   ImageID = y.ImageID,
                   MovieID = y.MovieID,
                   isPoster = y.isPoster,
                   FilePath = y.ExistingFilePath
               }).ToArrayAsync();
        }
    }
    }

