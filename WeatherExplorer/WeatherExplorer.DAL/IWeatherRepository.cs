using WeatherExplorer.DAL.Models;

namespace WeatherExplorer.DAL
{
    public interface IWeatherRepository
    {
        public Task AddWeatherAsync(IEnumerable<Weather> weather);
        public Task<City> AddCityByNameAsync(string cityName);
        public Task<WeatherCondition> AddWeatherConditionAsync(string condition);
        public Task<City> GetCityByNameAsync(string cityName);
        public Task<WeatherCondition> GetWeatherConditionByNameAsync(string conditionName);
        public IQueryable<Weather> GetWeather(int? year = null, int? month = null);
        public Task SaveAsync();
        public Task<IEnumerable<City>> GetCities();
        Task<WeatherCondition> GetWeatherConditionByIdAsync(Guid id);
    }
}
