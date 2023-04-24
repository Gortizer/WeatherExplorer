namespace WeatherExplorer.BL.Services.Interfaces
{
    public interface IFileReader
    {
        public Task AddCityAsync(string cityName);
        public Task ReadFileAsync(Stream dataStream, string cityName);
    }
}

