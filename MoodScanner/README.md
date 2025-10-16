Face&Fun - .NET MAUI App

Face&Fun is a mobile app that suggests personalized activities based on the number of people in a photo, current weather, location, and user preferences. It uses AI for face detection, OpenWeatherMap for weather, and Geoapify/Foursquare Places API for location-based suggestions.

Setup
1. API Keys

You need API keys for:

OpenWeatherMap (Weather data)

Geoapify (Reverse geocoding / location)

Create a file in your project:
Resources/Raw/apikey.txt

Paste your API keys in two lines: <br>
YOUR_APIKEY_OPENWEATHER <br>
YOUR_APIKEY_GEOAPIFY

This file is already included in .gitignore, so your keys won’t be uploaded to GitHub.

2. Dependencies

.NET MAUI

Newtonsoft.Json
 (JSON parsing)

UltraFaceDotNet
 (Face detection)

MAUI Essentials (Location, MediaPicker, Geolocation, Geocoding)

3. Running the App

Open the project in Visual Studio 2022+ with MAUI workload.

Ensure you have apikey.txt in Resources/Raw as described above.

Build and run the app on an Android/iOS simulator or device.

Grant permissions for camera, location, and storage when prompted.

4. How the App Works

User opens the app → weather and location are fetched automatically.

User takes a photo → AI detects number of faces.

User selects preferences:

Romantic Mode (on/off)

Budget Limit (per person)

Distance Range (km)

Activity Type (restaurant, walk, picnic, art, indoor, outdoor, action-based)

The app generates a keyword based on all inputs.

The Foursquare Places API is queried using:

Keyword

User location

Distance radius

App shows a recommended activity with:

Name

Category

Address

Image (if available)

5. Notes

Location is retrieved using MAUI Essentials (Geolocation + Reverse Geocoding via Geoapify).

Weather is fetched from OpenWeatherMap API.

Recommended places come from Foursquare Places API, filtered by distance and category.
