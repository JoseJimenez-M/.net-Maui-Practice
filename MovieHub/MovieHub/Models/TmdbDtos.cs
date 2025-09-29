using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace MovieHub.Models
{
    public sealed class TmdbGenreList
    {
        [JsonPropertyName("genres")]
        public List<Genre> Genres { get; set; } = new();
    }

 
    public sealed class TmdbMoviePage
    {
        [JsonPropertyName("results")]
        public List<TmdbMovieDto> Results { get; set; } = new();
    }

    public sealed class TmdbMovieDto
    {
        [JsonPropertyName("id")] public int Id { get; set; }
        [JsonPropertyName("title")] public string Title { get; set; } = string.Empty;
        [JsonPropertyName("overview")] public string Overview { get; set; } = string.Empty;
        [JsonPropertyName("poster_path")] public string? PosterPath { get; set; }
        [JsonPropertyName("release_date")] public string ReleaseDate { get; set; } = string.Empty;
        [JsonPropertyName("vote_average")] public double VoteAverage { get; set; }

        public Movie ToModel() => new()
        {
            Id = Id,
            Title = Title,
            Overview = Overview,
            PosterPath = PosterPath,
            ReleaseDate = ReleaseDate,
            VoteAverage = VoteAverage
        };
    }
}
