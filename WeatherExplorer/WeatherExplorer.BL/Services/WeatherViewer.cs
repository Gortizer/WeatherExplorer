using Microsoft.Extensions.Logging;
using WeatherExplorer.BL.Models;
using WeatherExplorer.BL.Services.Interfaces;
using WeatherExplorer.DAL;
using WeatherExplorer.DAL.Models;

namespace WeatherExplorer.BL.Services
{
    public class WeatherViewer : IViewer
    {
        private readonly IWeatherRepository _weatherRepository;
        private readonly ILogger<WeatherViewer> _logger;
        public WeatherViewer(IWeatherRepository weatherRepository, ILogger<WeatherViewer> logger)
        {
            _weatherRepository = weatherRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<ViewWeather>> GetWeather(int? year, int? month)
        {
            var weather = _weatherRepository.GetWeather(year, month).ToList();
            if (weather == null || weather.Count() < 1)
                return null;

            return await MapFromWeather(weather);
        }

        public async Task<IEnumerable<string>> GetCities()
        {
            var cities = await _weatherRepository.GetCities();
            return cities.Select(cities => cities.Name);
        }

        public async Task<string> GetCityByName(string cityName)
        {
            var city = await _weatherRepository.GetCityByNameAsync(cityName);
            return city.Name;
        }

        private async Task<Dictionary<Guid, string>> GetWeatherConditions(IEnumerable<Weather> weathers)
        {
            var conditionIds = weathers.Select(w => w.WeatherConditionId).Distinct();
            var conditionsFromDb = new Dictionary<Guid, string>();

            foreach (var condition in conditionIds)
            {
                var conditionFromDb = await _weatherRepository.GetWeatherConditionByIdAsync(condition);
                conditionsFromDb.Add(conditionFromDb.Id, conditionFromDb.Name);
            }

            return conditionsFromDb;
        }
        private async Task<IEnumerable<ViewWeather>> MapFromWeather(IEnumerable<Weather> weathers)
        {
            var weatherList = new List<ViewWeather>();
            var conditions = await GetWeatherConditions(weathers);
            try
            {
                Parallel.ForEach(weathers, weather =>
                {
                    conditions.TryGetValue(weather.WeatherConditionId, out var id);
                    weatherList.Add(new ViewWeather
                    {
                        Date = weather.Date,
                        Time = weather.Time,
                        Temprature = weather.Temprature,
                        Humidity = weather.Humidity,
                        DewPoint = weather.DewPoint,
                        Pressure = weather.Pressure,
                        WindDirection = weather.WindDirection,
                        WindSpeed = weather.WindSpeed,
                        Cloudy = weather.Cloudy,
                        LowerborderClouds = weather.LowerborderClouds,
                        HorizontalVisibility = weather.HorizontalVisibility,
                        WeatherConditionString = id
                    });
                });
                return weatherList;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}\n{ex.StackTrace}");
            }
            return null;
        }

    }
}
