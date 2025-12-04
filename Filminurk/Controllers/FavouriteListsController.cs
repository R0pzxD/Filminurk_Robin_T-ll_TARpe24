using System.Security.Cryptography.Xml;
using Filminurk.Core.Domain;
using Filminurk.Core.Dto;
using Filminurk.Core.ServiceInterface;
using Filminurk.Data;
using Filminurk.Models;
using Filminurk.Models.FavouriteLists;
using Filminurk.Models.Movies;
using Microsoft.AspNetCore.Mvc;

namespace Filminurk.Controllers
{
    public class FavouriteListsController : Controller
    {
        private readonly FilminurkTARpe24Context _context;
        private readonly IFavouriteListsServices _favouriteListsServices;
        public FavouriteListsController(FilminurkTARpe24Context context,
            IFavouriteListsServices favouriteListsServices)
        {
            _context = context;
            _favouriteListsServices = favouriteListsServices;
        }
        public IActionResult Index()
        {
            var resultingLists = _context.FavouriteLists
                .OrderByDescending(y => y.ListCreatedAt)
                .Select(x => new FavouriteListsIndexViewModel
                {
                    FavouriteListID = x.FavouriteListID,
                    ListsBelongsToUser = x.ListsBelongsToUser,
                    IsMovieOrActor = x.IsMovieOrActor,
                    ListName = x.ListName,
                    ListDescription = x.ListDescription,
                    ListCreatedAt = x.ListCreatedAt,
                    Images = (List<FavouriteListIndexImageViewModel>)_context.FilesToDatabase
                   .Where(ml => ml.ListID == x.FavouriteListID)
                   .Select(Li => new FavouriteListIndexImageViewModel
                   {
                       ListID = Li.ListID,
                       ImageData = Li.ImageData,
                       ImageTitle = Li.ImageTitle,
                       Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(Li.ImageData)),
                       ImageID = Li.ImageID,
                   })


                }

                );

            return View(resultingLists);

        }
        [HttpGet]
        public IActionResult Create()
        {
            var movies = _context.Movies
                .OrderBy(m => m.Title)
                .Select(mo => new MoviesIndexViewModel
                {
                    ID = mo.ID,
                    Title = mo.Title,
                    FirstPublished = mo.FirstPublished,
                    CurrentRating = mo.CurrentRating,
                })
                .ToList();
            ViewData["allmovies"] = movies;
            ViewData["iserHasSelected"] = new List<string>();
            FavouriteListUserCreateViewModel vm = new();
            return View("UserCreate", vm);
        }
        [HttpPost]
        public async Task<IActionResult> UserCreate(FavouriteListUserCreateViewModel vm, List<string> userHasSelected,
            List<MoviesIndexViewModel> movies)
        {
            List<Guid> tempParse = new();
            foreach (var stringID in userHasSelected)
            {
                tempParse.Add(Guid.Parse(stringID));
            }
            var newListDto = new FavouriteListDTO() { };
            newListDto.ListName = vm.ListName;
            newListDto.ListDescription = vm.ListDescription;
            newListDto.IsMovieOrActor = vm.IsMovieOrActor;
            newListDto.IsPrivate = vm.IsPrivate;
            newListDto.ListCreatedAt = DateTime.UtcNow;
            newListDto.ListsBelongsToUser = "00000000-0000-0000-000000000001";
            newListDto.ListDeletedAt = vm.ListDeletedAt;
            newListDto.ListOfMovies = vm.ListOfMovies;

            foreach(var movieId in tempParse)
            {
                var thismovie = _context.Movies.Where(tm => tm.ID == movieId).ToList().Take(1);
                newListDto.ListOfMovies.Add((Movie)thismovie);
            }
            //List<Guid> convertedIDs = new List<Guid>();
            //if (newListDto.ListOfMovies != null)
            //{
            //    convertedIDs = MovieToId(newListDto.ListOfMovies);
            //}

            var newList = await _favouriteListsServices.Create(newListDto /*, convertedIDs*/);
            if (newList != null)
            {
                return BadRequest();
            }
            return RedirectToAction("Index", vm);
        }
        [HttpGet]
        public async Task<IActionResult> UserDetails(Guid id, Guid thisuserid)
        {
            if(id == null || thisuserid == null)
            {
                return BadRequest();
            }
            var thisList = _context.FavouriteLists
                .Where(tl => tl.FavouriteListID == id && tl.ListsBelongsToUser ==
                thisuserid.ToString())
                .Select(
                stl => new FavouriteListUserDetailsViewModel
                {
                    FavouriteListID = stl.FavouriteListID,
                    ListsBelongsToUser = stl.ListsBelongsToUser,
                    ListName = stl.ListName,
                    ListDescription = stl.ListDescription,
                    IsPrivate = stl.IsPrivate,
                    ListOfMovies = stl.ListOfMovies,
                    IsReported = stl.IsReported,
                //    Images = _context.FilesToDatabase
                //    .Where(i => i.ListID == stl.FavouriteListID)
                //    .Select(si => new FavouriteListIndexImageViewModel
                //    {
                //        ImageID = si.ImageID,
                //        ListID = si.ListID,
                //        ImageData = si.ImageData,
                //        ImageTitle = si.ImageTitle,
                //        Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(si.ImageData))
                //    }
                //    ).ToList().First()

                //}
                //);
            
            if(thisList == null)
            {
                return NotFound();
            }
            return View("Details", thisList.First());

        }
        [HttpPost]
        public async Task<IActionResult>  UserTogglePrivacy(Guid id)
        {
            FavouriteList thisList = _favouriteListsServices.DetailsAsync(id);
            FavouriteListDTO updatedList = new FavouriteListDTO();
            updatedList.FavouriteListID = thisList.FavouriteListID;
            updatedList.ListsBelongsToUser = thisList.ListsBelongsToUser;
            updatedList.ListName = thisList.ListName;
            updatedList.ListDescription = thisList.ListDescription;
            updatedList.IsPrivate = thisList.IsPrivate;
            updatedList.ListOfMovies = thisList.ListOfMovies;
            updatedList.IsReported = thisList.IsReported;
            updatedList.IsMovieOrActor = thisList.IsMovieOrActor;
            updatedList.ListCreatedAt = thisList.ListCreatedAt;
            updatedList.ListModifiedAt = DateTime.Now;
            updatedList.ListDeletedAt = DateTime.Now;

            var result = await _favouriteListsServices.Update(updatedList);
            if(result == null)
            {
                return NotFound();
            }
            if(result == null || result.IsPrivate != !result.IsPrivate)
            {
              return BadRequest();
            }
           
            //return View("Details", updatedList);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> UserDelete(Guid id)
        {
            var deletedList = await _favouriteListsServices.DetailsAsync(id);
            deletedList.ListDeletedAt = DateTime.Now;
            var dto = new FavouriteListDTO();
            dto.FavouriteListID = deletedList.FavouriteListID;

            dto.ListsBelongsToUser = deletedList.ListsBelongsToUser;
            dto.ListName = deletedList.ListName;
            dto.ListDescription = deletedList.ListDescription;
            dto.IsPrivate = deletedList.IsPrivate;
            dto.ListOfMovies = deletedList.ListOfMovies;
            dto.IsReported = deletedList.IsReported;
            dto.IsMovieOrActor = deletedList.IsMovieOrActor;
            dto.ListCreatedAt = deletedList.ListCreatedAt;
            dto.ListModifiedAt = DateTime.Now;
            dto.ListDeletedAt = deletedList.ListDeletedAt;
            



            var result = await _favouriteListsServices.Update(deletedList);
        }
        private List<Guid> MovieToId(List<Movie> listOfMovies)
        {
            var result = new List<Guid>();
            foreach (var movie in listOfMovies)
            {
                result.Add(movie.ID);
            }
            return result;
        }
    }
}
