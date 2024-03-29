﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherExplorer.BL.Models;
using WeatherExplorer.DAL.Models;

namespace WeatherExplorer.BL.Services.Interfaces
{
    public interface IViewer
    {
        public Task<IEnumerable<ViewWeather>> GetWeather(int? year, int? month);
        public Task<IEnumerable<string>> GetCities();
        public Task<string> GetCityByName(string cityName);
    }
}
