namespace MovieHub.Models
{

    public sealed class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Overview { get; set; } = string.Empty;
        public string? PosterPath { get; set; }
        public string ReleaseDate { get; set; } = string.Empty;
        public double VoteAverage { get; set; }

        public string PosterUrl =>
            string.IsNullOrWhiteSpace(PosterPath) ? "" : Services.Constants.ImageBase + PosterPath;
    }
}
