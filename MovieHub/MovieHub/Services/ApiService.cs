using System.Net.Http.Headers;
using System.Text.Json;
using MovieHub.Models;

namespace MovieHub.Services
{
    public sealed class ApiService
    {
        private readonly HttpClient _http = new();
        private readonly JsonSerializerOptions _json = new() { PropertyNameCaseInsensitive = true };

        public ApiService()
        {
            // v4 bearer auth
            _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Constants.TmdbV4Token);

            _http.DefaultRequestHeaders.UserAgent.Add(
                new ProductInfoHeaderValue("MovieHub", "1.0"));
        }

        private static string BuildUrl(string pathAndQuery)
            => $"{Constants.TmdbBase.TrimEnd('/')}/{pathAndQuery.TrimStart('/')}";

        private async Task<T> GetAsync<T>(string pathAndQuery, CancellationToken ct)
        {
            var url = BuildUrl(pathAndQuery);
            System.Diagnostics.Debug.WriteLine($"TMDB GET => {url}");

            using var res = await _http.GetAsync(url, ct);
            var body = await res.Content.ReadAsStringAsync(ct);

            if (!res.IsSuccessStatusCode)
                throw new HttpRequestException(
                    $"HTTP {(int)res.StatusCode} {res.ReasonPhrase}\nURL: {url}\nBody: {body}");

            return JsonSerializer.Deserialize<T>(body, _json)
                   ?? throw new InvalidOperationException("Empty JSON.");
        }

        public async Task<List<Genre>> GetGenresAsync(CancellationToken ct = default)
        {
            var dto = await GetAsync<TmdbGenreList>("genre/movie/list?language=en-US", ct);
            return (dto.Genres ?? new()).OrderBy(g => g.Name).ToList();
        }

        public async Task<List<Movie>> GetNowPlayingAsync(CancellationToken ct = default)
        {
            var dto = await GetAsync<TmdbMoviePage>("movie/now_playing?language=en-US&page=1", ct);
            return (dto.Results ?? new()).Select(r => r.ToModel()).ToList();
        }

        public async Task<List<Movie>> SearchMoviesAsync(string query, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(query)) return await GetNowPlayingAsync(ct);
            var q = Uri.EscapeDataString(query.Trim());
            var dto = await GetAsync<TmdbMoviePage>(
                $"search/movie?query={q}&include_adult=false&language=en-US&page=1", ct);
            return (dto.Results ?? new()).Select(r => r.ToModel()).ToList();
        }

        public async Task<List<Movie>> GetMoviesByGenreAsync(int genreId, CancellationToken ct = default)
        {
            var dto = await GetAsync<TmdbMoviePage>(
                $"discover/movie?with_genres={genreId}&sort_by=popularity.desc&language=en-US&page=1", ct);
            return (dto.Results ?? new()).Select(r => r.ToModel()).ToList();
        }
    }
}
