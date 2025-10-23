using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Filminurk.Core.Domain;
using Filminurk.Core.Dto;
using Filminurk.Core.ServiceInterface;

namespace Filminurk.ApplicationServices.Services
{
    public class MovieServices : IMovieServices
    {
        public Task<Movie> Create(MoviesDTO dTO)
        {
            throw new NotImplementedException();
        }

        public Task<Movie> DetailsAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
