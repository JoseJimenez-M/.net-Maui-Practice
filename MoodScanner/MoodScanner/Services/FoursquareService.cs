using MoodScanner.Models;
using System.IO;
using System.Net.Http.Headers;
using System.Text.Json;

namespace MoodScanner.Services
{
    public static class FoursquareService
    {
        private static string ApiKey
        {
            get
            {
                string key = "";
                try
                {
                    using var stream = FileSystem.OpenAppPackageFileAsync("apikey.txt").Result;
                    using var reader = new StreamReader(stream);
                    reader.ReadLine();
                    key = reader.ReadLine()?.Trim();
                }
                catch
                {
                    key = "";
                }
                return key;
            }
        }

        public static async Task<Place> GetNearbyPlaceWithPhotoAsync(string keyword, double latitude, double longitude, int radiusMeters = 5000)
        {
            using var client = new HttpClient();

            string categories = Uri.EscapeDataString($"catering.{keyword.ToLower()}");
            
            string url = $"https://api.geoapify.com/v2/places?categories={categories}&filter=circle:{longitude},{latitude},{radiusMeters}&limit=1&apiKey={ApiKey}";

            var response = await client.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);
            var features = doc.RootElement.GetProperty("features");
            if (features.GetArrayLength() == 0)
                return null;

            var first = features[0];
            var props = first.GetProperty("properties");

            string name = props.GetProperty("name").GetString();
            string category = keyword;
            string address = props.TryGetProperty("formatted", out var addrProp)
                                 ? addrProp.GetString()
                                 : (props.TryGetProperty("street", out var st) ? st.GetString() : null);
            double dist = props.TryGetProperty("distance", out var dprop) ? dprop.GetDouble() : 0;

            string imageUrl = null;
            if (props.TryGetProperty("wiki_media", out var mediaProp) && mediaProp.ValueKind == JsonValueKind.String)
            {
                imageUrl = mediaProp.GetString();
            }

            return new Place
            {
                Name = name,
                Category = category,
                Address = address,
                Distance = dist,
                ImageUrl = imageUrl
            };
        }
    }
}