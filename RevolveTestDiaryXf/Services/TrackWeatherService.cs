using RevolveTestDiaryXf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;

namespace RevolveTestDiaryXf.Services
{
    /// <summary>
    /// Class that communicates with the openweathermap api to fetch the temperature for a given town.
    /// </summary>
    public class TrackWeatherService
    {
        public Dictionary<string, Tuple<double, double>> TownToCoordMap = new Dictionary<string, Tuple<double, double>>()
        {
            {"Værnes", new Tuple<double, double>(63.463558, 10.925606) },
            {"EC Dahls", new Tuple<double, double>(63.440635, 10.429107) },
            {"Dragvoll", new Tuple<double, double>(63.406310, 10.470311) },
            {"Hockenheim", new Tuple<double, double>(49.330539, 8.564970) }
        };


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
        public Models.Weather GetCurrentWeatherForTown(string town)
        {
            if (town == null)
                return new Models.Weather();
            if (TownToCoordMap.TryGetValue(town, out var coords))
            {
                string rawJsonData;

                using (var client = new WebClient())
                {
                    try
                    {
                        // Use metric to get degrees Celcius
                        rawJsonData = client.DownloadString($"http://api.openweathermap.org/data/2.5/weather?appid={_apiKey}&lat={coords.Item1}&lon={coords.Item2}&units=metric");
                    }
                    catch (WebException)
                    {
                        return new Models.Weather();
                    }
                }

                OpenWeatherMapObjectCurrentWeather weatherMapObject;
                try
                {
                    weatherMapObject = JsonSerializer.Deserialize<OpenWeatherMapObjectCurrentWeather>(rawJsonData);
                }
                catch (JsonException)
                {
                    return new Models.Weather();
                }

                if (weatherMapObject.cod == OPENWEATHERMAP_SUCCESS)
                {
                    return new Models.Weather() { Temperature = weatherMapObject.main.temp, Description = weatherMapObject.weather.First().description, IsLoaded = true };
                }
            }
            return new Models.Weather();
        }

        public Models.Weather GetHistoricWeatherForTown(string town, long unixTime)
        {
            if (town == null)
                return new Models.Weather();
            if (TownToCoordMap.TryGetValue(town, out var coords))
            {
                string rawJsonData;

                var url = $"https://api.openweathermap.org/data/2.5/onecall/timemachine?lat={coords.Item1}&lon={coords.Item2}&dt={unixTime}&appid={_apiKey}&units=metric";
                using (var client = new WebClient())
                {
                    try
                    {
                        // Use metric to get degrees Celcius
                        rawJsonData = client.DownloadString(url);
                    }
                    catch (WebException)
                    {
                        return new Models.Weather();
                    }
                }
                OpenWeatherMapObjectHistoricWeather weatherMapObject;
                try
                {
                    weatherMapObject = JsonSerializer.Deserialize<OpenWeatherMapObjectHistoricWeather>(rawJsonData);
                    return new Models.Weather() { Temperature = weatherMapObject.current.temp, Description = weatherMapObject.current.weather.First().description, IsLoaded = true };
                }
                catch (JsonException)
                {
                    return new Models.Weather();
                }
            }
            return new Models.Weather();
        }

        /// <summary>
        /// Class to hold OpenWeatherMap API responses
        /// Currently only contains what this app needs, but is 
        /// easily expanded using the api docs: 
        /// https://openweathermap.org/current#current_JSON
        /// </summary>
        private class OpenWeatherMapObjectCurrentWeather
        {
            public int cod { get; set; }

            public Main main { get; set; }

            public List<Weather> weather { get; set; }
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


        private class OpenWeatherMapObjectHistoricWeather
        {
            public Current current { get; set; }
        }

        private class Current
        {
            public double temp { get; set; }
            public List<Weather> weather { get; set; }
        }

    }
}
