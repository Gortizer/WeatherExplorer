using Microsoft.Extensions.Logging;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using WeatherExplorer.BL.Models;
using WeatherExplorer.DAL;
using WeatherExplorer.DAL.Models;

namespace WeatherExplorer.BL.Services
{
    public class ExcelReader : IFileReader
    {
        private readonly IWeatherRepository _weatherRepository;
        private readonly ILogger<ExcelReader> _logger;

        public ExcelReader(IWeatherRepository weatherRepository, ILogger<ExcelReader> logger)
        {
            _weatherRepository = weatherRepository;
            _logger = logger;
        }
        public async Task ReadFileAsync(Stream dataStream, string cityName)
        {
            var result = new List<ViewWeather>();
            dataStream.Position = 0;
            var workbook = new XSSFWorkbook(dataStream);
            await Task.Run(async () =>
            {
                for (int sheetIndex = 0; sheetIndex < workbook.NumberOfSheets; sheetIndex++)
                {
                    var sheet = workbook.GetSheetAt(sheetIndex);
                    for (int cellIndex = 4; cellIndex < sheet.LastRowNum - 1; cellIndex++)
                    {
                        try
                        {
                            var row = sheet.GetRow(cellIndex);
                            var obj = new ViewWeather();
                            obj.Date = DateOnly.Parse(row.GetCell(0)?.StringCellValue);
                            obj.Time = TimeOnly.Parse(row.GetCell(1)?.StringCellValue);
                            obj.Temprature = row.GetCell(2) != null ? (float)row.GetCell(2).NumericCellValue : 0f;
                            obj.Humidity = row.GetCell(3) != null ? (byte)row.GetCell(3).NumericCellValue : (byte)0;
                            obj.DewPoint = row.GetCell(4) != null ? (float)row.GetCell(4).NumericCellValue : 0f;
                            obj.Pressure = row.GetCell(5) != null ? (int)row.GetCell(5).NumericCellValue : 0;
                            obj.WindDirection = string.IsNullOrWhiteSpace(row.GetCell(6)?.StringCellValue) ? "штиль" : row.GetCell(6)?.StringCellValue;
                            obj.WindSpeed = row.GetCell(7)?.CellType == CellType.Numeric ? (byte)row.GetCell(7).NumericCellValue : (byte)0;
                            obj.Cloudy = row.GetCell(8)?.CellType == CellType.Numeric ? (byte)row.GetCell(8).NumericCellValue : (byte)0;
                            obj.LowerborderClouds = row.GetCell(9)?.CellType == CellType.Numeric ? (byte)row.GetCell(9).NumericCellValue : (byte)0;
                            obj.HorizontalVisibility = row.GetCell(10)?.CellType == CellType.Numeric ? (byte)row.GetCell(10).NumericCellValue : (byte)0;
                            obj.WeatherConditionString = string.IsNullOrWhiteSpace(row.GetCell(11)?.StringCellValue) ? "Ясно" : row.GetCell(11)?.StringCellValue;

                            result.Add(obj);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex.Message);
                        }
                    }
                }
                await InsertInDb(result, cityName);
            });


            _logger.LogInformation("Succesful reading file");
        }

        public async Task AddCityAsync(string cityName)
        {
            await _weatherRepository.AddCityByNameAsync(cityName);
            await _weatherRepository.SaveAsync();
        }

        private async Task InsertInDb(IEnumerable<ViewWeather> weathers, string cityName)
        {
            if (weathers == null || string.IsNullOrWhiteSpace(cityName))
                return;

            var listToInsert = new List<Weather>();
            var city = await _weatherRepository.GetCityByNameAsync(cityName);
            if (city == null)
            {
                city = await _weatherRepository.AddCityByNameAsync(cityName);
            }

            var conditionNames = weathers.Select(w => w.WeatherConditionString).Distinct().ToList();

            var conditionsFromDb = new List<WeatherCondition>();
            foreach (var condition in conditionNames)
            {
                var conditionFromDb = await _weatherRepository.GetWeatherConditionByNameAsync(condition);
                if (conditionFromDb == null)
                    conditionFromDb = await _weatherRepository.AddWeatherConditionAsync(condition);
                conditionsFromDb.Add(conditionFromDb);
            }
            Parallel.ForEach(weathers, (weather) =>
            {
                var conditionId = conditionsFromDb.FirstOrDefault(condition => weather.WeatherConditionString == condition.Name).Id;
                listToInsert.Add(new Weather
                {
                    CityId = city.Id,
                    WeatherConditionId = conditionId,
                    WindDirection = weather.WindDirection,
                    WindSpeed = weather.WindSpeed,
                    Cloudy = weather.Cloudy,
                    Date = weather.Date,
                    Time = weather.Time,
                    DewPoint = weather.DewPoint,
                    Pressure = weather.Pressure,
                    Humidity = weather.Humidity,
                    LowerborderClouds = weather.LowerborderClouds,
                    HorizontalVisibility = weather.HorizontalVisibility,
                    Temprature = weather.Temprature

                });
            });
            try
            {
                await _weatherRepository.AddWeatherAsync(listToInsert);
                await _weatherRepository.SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}\n{ex.StackTrace}");
            }
        }
    }
}
