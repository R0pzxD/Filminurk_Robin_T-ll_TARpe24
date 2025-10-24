namespace Filminurk.Models.Movies
{
    public class MoviesCreateViewModel
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateOnly FirstPublished { get; set; }
        public string Director { get; set; }
        public List<string>? Actors { get; set; }
        public double? CurrentRating { get; set; }
        //public List<UserComment>? Reviews { get; set; }

        public List<IFormFile> Files { get; set; }
        public List<ImageViewModel> Images { get; set; }  = new List<ImageViewModel>();

        public int? OscarsWon { get; set; }
        public decimal? RottenTomatoes { get; set; }
        public decimal? IMDbRating { get; set; }

        public DateTime? EntryCreatedAt { get; set; }
        public DateTime? EntryModifyAt { get; set; }

    }
}
