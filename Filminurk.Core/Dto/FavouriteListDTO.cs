using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Filminurk.Core.Domain;
using Microsoft.AspNetCore.Http;

namespace Filminurk.Core.Dto
{
    public class FavouriteListDTO
    {
        [Key]
        public Guid FavouriteListID { get; set; }
        public string ListsBelongsToUser { get; set; }
        public bool IsMovieOrActor { get; set; }
        public string ListName { get; set; }
        public string? ListDescription { get; set; }
        public bool IsPrivate { get; set; }
        public List<Movie>? ListOfMovies { get; set; }
        public DateTime? ListCreatedAt { get; set; }
        public DateTime? ListModifiedAt { get; set; }
        public DateTime? ListDeletedAt { get; set; }
        public bool IsReported { get; set; } = false;
    }
}
