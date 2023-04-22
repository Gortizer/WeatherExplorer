using WeatherExplorer.DAL.Models;

namespace WeatherExplorer.DAL
{
    public interface IWeatherRepository
    {
        public Task<City> GetCityByNameAsync(string cityName);
        public Task<WeatherCondition> GetWeatherConditionByNameAsync(string conditionName);
        public Task AddWeatherAsync(Weather weather);
        public Task AddWeatherConditionAsync(WeatherCondition condition);
        public Task SaveAsync();
    }
}
