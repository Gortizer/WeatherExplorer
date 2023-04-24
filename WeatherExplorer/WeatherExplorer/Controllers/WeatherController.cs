using Microsoft.AspNetCore.Mvc;
using WeatherExplorer.BL;

namespace WeatherExplorer.Controllers
{
    public class WeatherController : Controller
    {
        private readonly IFileReader _fileReader;
        private readonly IViewer _viewer;

        public WeatherController(IFileReader fileReader, IViewer viewer)
        {
            _fileReader = fileReader;
            _viewer = viewer;
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
            try
            {
                if (year > 9999 || year < 1)

                    return BadRequest(year);

                if (month > 12 || month < 1)
                    return BadRequest(month);

                if (pageNumber < 1)
                    return BadRequest(pageNumber);

                var weatherData = await _viewer.GetWeather(year, month);

                if (weatherData != null && pageNumber != 0)
                    weatherData = weatherData.Take(pageNumber);

                return View("WeatherView", weatherData);
            }
            catch 
            { }
            return View("WeatherView");

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
