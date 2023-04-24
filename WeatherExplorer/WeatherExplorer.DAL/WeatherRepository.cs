using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WeatherExplorer.DAL.Models;

namespace WeatherExplorer.DAL
{
    public class WeatherRepository : IWeatherRepository
    {
        private readonly WeatherExplorerContext _context;

        public WeatherRepository(WeatherExplorerContext context)
        {
            _context = context;
        }

        public async Task AddWeatherAsync(IEnumerable<Weather> weather)
        {
            await _context.AddRangeAsync(weather);
        }

        public async Task<WeatherCondition> AddWeatherConditionAsync(string conditionName)
        {
            var condition = new WeatherCondition { Name = conditionName };
            await _context.AddAsync(condition);
            return condition;
        }

        public async Task<City> AddCityByNameAsync(string cityName)
        {
            var city = new City { Name = cityName };
            await _context.AddAsync(city);
            return city;
        }

        public async Task<City> GetCityByNameAsync(string cityName)
        {
            return await _context.Cities.FirstOrDefaultAsync(city => city.Name.ToLower() == cityName.ToLower());
        }

        public async Task<IEnumerable<City>> GetCities()
        {
            return await _context.Cities.ToListAsync();
        }

        public IQueryable<Weather> GetWeather(int? year = null, int? month = null)
        {
            if (month == null)
                return _context.Weathers.Where(weather => weather.Date.Year == year);
            if (year == null)
                return _context.Weathers.Where(weather => weather.Date.Month == month);
            if (year != null && month != null)
                return _context.Weathers.Where(weather => weather.Date.Year == year && weather.Date.Month == month);
            return _context.Weathers.Where(weather => weather.Date.Year == DateTime.Now.Year && weather.Date.Month == DateTime.Now.Month).Take(30);
        }

        public async Task<WeatherCondition> GetWeatherConditionByNameAsync(string conditionName)
        {
            return await _context.WeatherConditions.FirstOrDefaultAsync(condition => condition.Name == conditionName);
        }

        public async Task<WeatherCondition> GetWeatherConditionByIdAsync(Guid id)
        {
            return await _context.WeatherConditions.FirstOrDefaultAsync(condition => condition.Id == id);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
