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
                    Images = (List<FavouriteListIndexViewModel>)_context.FilesToDatabase
                   .Where(ml => ml.ListID == x.FavouriteListID)
                   .Select(Li => new FavouriteListIndexViewModel
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
            foreach(var stringID in userHasSelected)
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
            List<Guid> convertedIDs = new List<Guid>();
            if(newListDto.ListOfMovies != null)
            {
                convertedIDs = MovieToId(newListDto.ListOfMovies);
            }

            var newList = await _favouriteListsServices.Create(newListDto, convertedIDs);
            if(newList != null)
            {
                return BadRequest();
            }
            return RedirectToAction("Index", vm); 
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
