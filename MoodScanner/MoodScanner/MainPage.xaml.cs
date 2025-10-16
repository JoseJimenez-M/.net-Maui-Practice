using Microsoft.Maui;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Devices.Sensors;
using Microsoft.Maui.Storage;
using MoodScanner.Services;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net.Http;


namespace MoodScanner
{
    public partial class MainPage : ContentPage
    {

        private readonly IDetectService _myserviceclient;

        public MainPage()
        {
            InitializeComponent();
            _myserviceclient = (IDetectService)(new DetectService());
        }
        public static byte[] ConvertStreamToBytes(Stream stream)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

                if (photo != null)
                {
                    // Save the file into local storage
                    string localFilePath = System.IO.Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                    using Stream sourceStream = await photo.OpenReadAsync();
                    using FileStream localFileStream = File.OpenWrite(localFilePath);

                    await sourceStream.CopyToAsync(localFileStream);

                    // Flush and close the streams
                    await localFileStream.FlushAsync();
                    localFileStream.Close();

                    using Stream imageStream = File.OpenRead(localFilePath);


                    byte[] imageBytes = ConvertStreamToBytes(imageStream);


                    var detectResult = this._myserviceclient.Detect(imageBytes);
                    if (detectResult == null)
                        return;

                    if (detectResult.Boxes.Count > 0)
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", "Human Face Detected: " + detectResult.Boxes.Count, "Ok");
                        PeopleCountLabel.Text = detectResult.Boxes.Count.ToString();
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", "No Human Face Detected", "Ok");
                    }
                }
            }

            //RECOMMEND:
            var (suggestion, activityKeyword) = CollectAndGenerateKeyword();
            RecommendedActivityTitle.Text = suggestion;

            var location = await Geolocation.Default.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10)));
            if (location == null)
            {
                RecommendedActivityLabel.Text = "Location not found";
                return;
            }

            double latitude = location.Latitude;
            double longitude = location.Longitude;
            int distance = 50000;


            var place = await FoursquareService.GetNearbyPlaceWithPhotoAsync(activityKeyword, latitude, longitude, distance);
            //var place = await FoursquareService.GetNearbyPlaceWithPhotoAsync("park", latitude, longitude, 5000);

            if (place != null)
            {
                RecommendedActivityLabel.Text = $"{place.Name}\n{place.Category}\n{place.Address}";
                if (!string.IsNullOrEmpty(place.ImageUrl))
                {
                    PlaceImage.Source = place.ImageUrl;
                    PlaceImage.IsVisible = true;
                }
            }
            else
            {
                RecommendedActivityLabel.Text = "No nearby place found.";
                PlaceImage.Source = null;
            }



        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await UpdateLocationAsync();
            await UpdateWeatherAsync(); 
        }


        //LOCATION
        private async Task UpdateLocationAsync()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                var location = await Geolocation.Default.GetLocationAsync(request);

                if (location != null)
                {
                    var placemarks = await Geocoding.Default.GetPlacemarksAsync(location.Latitude, location.Longitude);
                    var placemark = placemarks?.FirstOrDefault();

                    if (placemark != null)
                    {
                        string street = placemark.Thoroughfare ?? "";
                        string number = placemark.SubThoroughfare ?? "";
                        string city = placemark.Locality ?? "";

                        LocationLabel.Text = $"{street} {number}, {city}".Trim();
                    }
                    else
                    {
                        LocationLabel.Text = "Unknown location";
                    }
                }
                else
                {
                    LocationLabel.Text = "Location not found";
                }
            }
            catch (FeatureNotSupportedException)
            {
                LocationLabel.Text = "Feature not supported";
            }
            catch (PermissionException)
            {
                LocationLabel.Text = "Permission denied";
            }
            catch (Exception)
            {
                LocationLabel.Text = "Error retrieving location";
            }
        }


        //WEATHER
        private async Task UpdateWeatherAsync()
        {
            try
            {
                
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                var location = await Geolocation.Default.GetLocationAsync(request);

                if (location == null)
                {
                    WeatherLabel.Text = "Location not found";
                    return;
                }

                double lat = location.Latitude;
                double lon = location.Longitude;

                //API Key OpenWeatherMap
                string apiKey;

                using (var stream = await FileSystem.OpenAppPackageFileAsync("apikey.txt"))
                using (var reader = new StreamReader(stream))
                {
                    apiKey = reader.ReadLine()?.Trim();
                }


                string url = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={apiKey}&units=metric";

                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetStringAsync(url);
                    var json = JObject.Parse(response);

                    string weatherMain = json["weather"]?[0]?["main"]?.ToString() ?? "-";
                    string weatherDesc = json["weather"]?[0]?["description"]?.ToString() ?? "-";
                    string temp = json["main"]?["temp"]?.ToString() ?? "-";

                    WeatherLabel.Text = $"{weatherMain} ({weatherDesc}), {temp}°C";
                }
            }
            catch (Exception ex)
            {
                WeatherLabel.Text = "Error retrieving weather";
                System.Diagnostics.Debug.WriteLine("Weather API error: " + ex.Message);
            }

        }


        //RECOMMENDATION:

        private (string suggestion, string keyWord) CollectAndGenerateKeyword()
        {
            string weather = WeatherLabel.Text?.Trim() ?? "";
            string location = LocationLabel.Text?.Trim() ?? "-";
            int.TryParse(PeopleCountLabel.Text, out int peopleCount);
            bool romanticMode = RomanticModeSwitch.IsToggled;
            string budget = BudgetPicker.SelectedItem?.ToString() ?? "";
            string distance = DistancePicker.SelectedItem?.ToString() ?? "";
            string activityType = ActivityPicker.SelectedItem?.ToString() ?? "";

            return ActivityService.GetSuggestedActivity(
                weather, peopleCount, romanticMode, budget, distance, activityType
            );
        }


    }
}