using System.Net;
using System.Text.Json;

namespace RevolveTestDiaryXf.Services
{
    /// <summary>
    /// Class that communicates with the openweathermap api to fetch the temperature for a given town.
    /// </summary>
    public class TrackWeatherService
    {
        private const int OPENWEATHERMAP_SUCCESS = 200;

        private string _apiKey;
        public TrackWeatherService(string apiKey)
        {
            _apiKey = apiKey;
        }

        /// <summary>
        /// Sends a request to the OpenWeatherMap API and returns the temperature for the given town,
        /// this is not a static method as MSTest cannot mock static methods.
        /// </summary>
        /// <param name="town"></param>
        /// <returns>Returns the weather of the town</returns>
        public Forecast GetTemperatureForTown(string town)
        {
            string rawJsonData;

            using (var client = new WebClient())
            {
                try
                {
                    // Use metric to get degrees Celcius
                    rawJsonData = client.DownloadString($"http://api.openweathermap.org/data/2.5/weather?appid={_apiKey}&q={town}&units=metric");
                }
                catch (WebException)
                {
                    return new Forecast();
                }
            }

            OpenWeatherMapObject weatherMapObject;
            try
            {
                weatherMapObject = JsonSerializer.Deserialize<OpenWeatherMapObject>(rawJsonData);
            }
            catch (JsonException)
            {
                return new Forecast();
            }

            if (weatherMapObject.cod == OPENWEATHERMAP_SUCCESS)
            {
                return new Forecast() { Temperature = weatherMapObject.main.temp, Description = weatherMapObject.weather.description };
            }
            return new Forecast();
        }

        public class Forecast
        {
            public double Temperature { get; set; }
            public string Description { get; set; }
        }

        /// <summary>
        /// Class to hold OpenWeatherMap API responses
        /// Currently only contains what this app needs, but is 
        /// easily expanded using the api docs: 
        /// https://openweathermap.org/current#current_JSON
        /// </summary>
        private class OpenWeatherMapObject
        {
            public int cod { get; set; }

            public Main main { get; set; }

            public Weather weather { get; set; }
        }

        /// <summary>
        /// Subdocument for the OpenWeatherMap API responses
        /// </summary>
        private class Main
        {
            public double temp { get; set; }
        }

        /// <summary>
        /// Subdocument for the OpenWeatherMap API responses
        /// </summary>
        private class Weather
        {
            public string description { get; set; }
        }
    }
}
