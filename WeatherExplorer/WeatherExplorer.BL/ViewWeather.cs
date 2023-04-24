using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherExplorer.DAL.Models;

namespace WeatherExplorer.BL
{
    public class ViewWeather
    {
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public float Temprature { get; set; }
        public byte Humidity { get; set; }
        public float DewPoint { get; set; }
        public int Pressure { get; set; }
        public string? WindDirection { get; set; }
        public byte? WindSpeed { get; set; }
        public byte? Cloudy { get; set; }
        public short? LowerborderClouds { get; set; }
        public byte? HorizontalVisibility { get; set; }
        public string? WeatherConditionString { get; set; }

    }
}
