using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Filminurk.Core.Domain;
using Filminurk.Core.Dto;
using Filminurk.Core.ServiceInterface;
using Filminurk.Data;
using Microsoft.EntityFrameworkCore;

namespace Filminurk.ApplicationServices.Services
{
    public class MovieServices : IMovieServices
    {
        private readonly FilminurkTARpe24Context _context;
        private readonly IFilesServices _filesServices;
        
        public MovieServices(FilminurkTARpe24Context context, IFilesServices filesServices)
        {
            _context = context;
            _filesServices = filesServices;
        }

        public async Task<Movie> Create(MoviesDTO dTO)
        {
            Movie movie = new Movie();
            movie.ID = Guid.NewGuid();
            movie.Actors = dTO.Actors;
            movie.Title = dTO.Title;
            movie.Description = dTO.Description;
            movie.FirstPublished = (DateOnly)dTO.FirstPublished;
            movie.Director = dTO.Director;
            movie.CurrentRating = dTO.CurrentRating;
            movie.OscarsWon = dTO.OscarsWon;
            movie.RottenTomatoes = dTO.RottenTomatoes;
            movie.IMDbRating = dTO.IMDbRating;
            movie.EntryCreatedAt = DateTime.Now;
            movie.EntryModifiedAt = DateTime.Now;
            _filesServices.FilesToApi(dTO, movie);

            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();
            return movie;
        }

        public async Task<Movie> DetailsAsync(Guid id)
        {
           var movie = await _context.Movies.FirstOrDefaultAsync(d => d.ID == id);
            return movie;
        }
        public async Task<Movie> Update(MoviesDTO dTO)
        {
            Movie movie = new Movie();
            movie.ID = (Guid)dTO.ID;
            movie.Actors = dTO.Actors;
            movie.Title = dTO.Title;
            movie.Description = dTO.Description;
            movie.FirstPublished = (DateOnly)dTO.FirstPublished;
            movie.Director = dTO.Director;
            movie.CurrentRating = dTO.CurrentRating;
            movie.OscarsWon = dTO.OscarsWon;
            movie.RottenTomatoes = dTO.RottenTomatoes;
            movie.IMDbRating = dTO.IMDbRating;
            movie.EntryCreatedAt = dTO.EntryCreatedAt;
            movie.EntryModifiedAt = dTO.EntryModifyAt;
            _filesServices.FilesToApi(dTO, movie);

            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();
            return movie;
        }
        public async Task<Movie> Delete(Guid id)
        {
            var result = await _context.Movies
            .FirstOrDefaultAsync(x => x.ID == id);

            var files = await _context.FilesToApi
                .Where(x => x.MovieID == id)
                .Select(y => new FileToApiDTO
                {
                    ImageID = y.ImageID,
                    MovieID = y.MovieID,
                    FilePath = y.ExistingFilePath
                }).ToArrayAsync();
            await _filesServices.RemoveImagesFromApi(files);
            _context.Movies.Remove(result);
            await _context.SaveChangesAsync();
            return result;
        }
    }
}
