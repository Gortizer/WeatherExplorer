namespace WeatherExplorer.DAL.Models
{
    public class Weather : Base
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
        public Guid WeatherConditionId { get; set; }
        public WeatherCondition WeatherCondition { get; set; }
        public Guid? CityId { get; set; }
        public City? City { get; set; }

    }
}