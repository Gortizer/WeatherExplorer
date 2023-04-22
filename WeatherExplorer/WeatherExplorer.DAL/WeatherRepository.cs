using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task AddWeatherAsync(Weather weather)
        {
            await _context.AddRangeAsync(weather);
        }

        public async Task AddWeatherConditionAsync(WeatherCondition condition)
        {
            await _context.AddAsync(condition);
        }

        public async Task<City> GetCityByNameAsync(string cityName)
        {
            return await _context.Cities.FirstOrDefaultAsync(city => city.Name == cityName);
        }

        public async Task<WeatherCondition> GetWeatherConditionByNameAsync(string conditionName)
        {
            return await _context.WeatherConditions.FirstOrDefaultAsync(condition => condition.Name == conditionName);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
