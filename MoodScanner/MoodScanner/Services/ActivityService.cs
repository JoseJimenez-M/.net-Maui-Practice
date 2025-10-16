using System;

namespace MoodScanner.Services
{
    public static class ActivityService
    {
        public static (string suggestion, string keyWord) GetSuggestedActivity(
            string weather, int peopleCount, bool romanticMode,
            string budget, string distance, string activityType)
        {
            string condition = weather.ToLower();
            string suggestion = "";
            string keyWord = "";

            if (romanticMode)
            {
                if (condition.Contains("rain"))
                {
                    keyWord = "restaurant"; 
                    suggestion = "Romantic dinner in a cozy restaurant";
                }
                else if (condition.Contains("snow"))
                {
                    keyWord = "park";
                    suggestion = "Walk together in a beautiful park";
                }
                else
                {
                    keyWord = "cafe";
                    suggestion = "Warm date with hot chocolate in a cafe";
                }
            }
            else
            {
                if (peopleCount <= 1)
                {
                    if (condition.Contains("rain"))
                    {
                        keyWord = "museum";
                        suggestion = "Visit an art museum";
                    }
                    else if (condition.Contains("snow"))
                    {
                        keyWord = "cafe";
                        suggestion = "Read a book at a quiet cafe";
                    }
                    else
                    {
                        keyWord = "museum";
                        suggestion = "Visit an art museum";
                    }
                }
                else if (peopleCount == 2)
                {
                    if (condition.Contains("rain"))
                    {
                        keyWord = "park";
                        suggestion = "Have a picnic in the park";
                    }
                    else if (condition.Contains("snow"))
                    {
                        keyWord = "cinema";
                        suggestion = "Watch a movie together";
                    }
                    else
                    {
                        keyWord = "restaurant";
                        suggestion = "Try a new restaurant nearby";
                    }
                }
                else
                {
                    if (condition.Contains("rain"))
                    {
                        keyWord = "sports_centre"; 
                        suggestion = "Go bowling or to an indoor escape room";
                    }
                    else if (condition.Contains("snow"))
                    {
                        keyWord = "cafe";
                        suggestion = "Board games night at a cafe";
                    }
                    else
                    {
                        keyWord = "park"; 
                        suggestion = "Outdoor barbecue or sports activity";
                    }
                }
            }


            //if (!string.IsNullOrEmpty(activityType) &&
            //    !suggestion.ToLower().Contains(activityType.ToLower()))
            //{
            //    suggestion += $" — suggested for {activityType.ToLower()} mood.";
            //}

            return (suggestion, keyWord);
        }
    }
}
