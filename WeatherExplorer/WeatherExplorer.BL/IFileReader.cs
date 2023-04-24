namespace WeatherExplorer.BL
{
    public interface IFileReader
    {
        public Task AddCityAsync(string cityName);
        public Task ReadFileAsync(Stream dataStream, string cityName);
    }
}

