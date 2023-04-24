using Microsoft.AspNetCore.Mvc;
using WeatherExplorer.BL;
using WeatherExplorer.BL.Models;

namespace WeatherExplorer.Controllers
{
    public class WeatherController : Controller
    {
        private readonly IFileReader _fileReader;
        private readonly IViewer _viewer;
        private readonly ILogger _logger;

        public WeatherController(IFileReader fileReader, IViewer viewer, ILogger logger)
        {
            _fileReader = fileReader;
            _viewer = viewer;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> LoaderView()
        {
            var cities = await _viewer.GetCities();
            return View("LoaderView", cities.ToList());
        }

        [HttpGet]
        public async Task<IActionResult> GetWeather(int pageNumber = 30, int? year = null, int? month = null)
        {
            var weatherData = new List<ViewWeather>();
            try
            {
                if (year > 9999 || year < 1)
                    return BadRequest("Incorrect year");

                if (month > 12 || month < 1)
                    return BadRequest("Incorrect month");

                if (pageNumber < 1)
                    return BadRequest("Count entities less then 1");

                weatherData = _viewer.GetWeather(year, month).Result.ToList();

                if (weatherData != null && pageNumber != 0)
                      weatherData.Take(pageNumber);

            }
            catch (Exception ex)
            {
                ViewBag.Message = "Failed load weather";
                _logger.LogError($"{ex.Message}\n{ex.StackTrace}");
            }
            return View("WeatherView", weatherData);

        }

        [HttpPost]
        public async Task<IActionResult> UploadData(IFormFileCollection files, string cityName)
        {
            try
            {
                foreach (var file in files)
                {
                    if (!file.FileName.Contains(".xls"))
                    {
                        ViewBag.Message = "File Upload Failed";
                        continue;
                    }

                    if (file.Length < 1)
                    {
                        ViewBag.Message = "File less then 1 byte";
                        continue;
                    }

                    using (var stream = new MemoryStream())
                    {
                        file.CopyTo(stream);
                        await _fileReader.ReadFileAsync(stream, cityName);
                    }
                }
                ViewBag.Message = "Files uploaded succesful";
            }
            catch
            {
                ViewBag.Message = "File Upload Failed";
            }

            return View("WeatherView");
        }

        [HttpPost]
        public async Task<IActionResult> AddCity(string cityName)
        {
            await _fileReader.AddCityAsync(cityName);
            var cities = await _viewer.GetCities();
            return View("LoaderView", cities.ToList());
        }
    }
}
