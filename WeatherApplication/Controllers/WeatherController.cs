using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Collections.Generic;
using System.IO;
using WeatherApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;


namespace WeatherApplication.Controllers
{
    public class WeatherController : Controller
    {
        private readonly AppDbContext _context;

        public WeatherController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        


        public IActionResult DataByYearOrMonth(string year, string month)
        {
            
            if (string.IsNullOrEmpty(year) && string.IsNullOrEmpty(month))
            {
                return View();
            }

            var sqlQuery = "";

            if (string.IsNullOrEmpty(year))
            {
                sqlQuery = $"SELECT * FROM Weathers WHERE Date LIKE '%.{month}.%'";
                
            }
            else if (string.IsNullOrEmpty(month))
            {
                sqlQuery = $"SELECT * FROM Weathers WHERE Date LIKE '%{year}'";
                
            }
            else 
            {
                sqlQuery = $"SELECT * FROM Weathers WHERE Date LIKE '%{year}' AND Date LIKE '%.{month}.%'";

            }

            var weatherDataForYear = _context.Weathers.FromSqlRaw(sqlQuery);

            if (weatherDataForYear is null) { return View(); }
            else { return View(weatherDataForYear.ToList()); }


        }



        [HttpPost]
        public IActionResult UploadFile(IFormFile file)
        {
            try
            {
                if (file == null || file.Length <= 0)
                {
                    ViewBag.Error = "Файл не был выбран.";
                    return View("Index");
                }

                if (Path.GetExtension(file.FileName).ToLower() != ".xlsx")
                {
                    ViewBag.Error = "Поддерживаются только файлы с расширением .xlsx";
                    return View("Index");
                }

                List<Weather> weatherData = new List<Weather>();

                using (var stream = new MemoryStream())
                {
                    file.CopyTo(stream);
                    stream.Position = 0;

                    using (var workbook = new XSSFWorkbook(stream))
                    {
                        foreach (var sheet in workbook)
                        {
                            var firstRow = ((XSSFSheet)sheet).GetRow(0);

                            if (firstRow == null || (firstRow.GetCell(0) != null && !firstRow.GetCell(0).ToString().Contains("Архив")))
                            {
                                ViewBag.Error = "На одном из листов файла не найдена ожидаемая информация в первой строке.";
                                return View("Index");
                            }

                            for (int i = 5; i <= ((XSSFSheet)sheet).LastRowNum; i++)
                            {
                                var row = ((XSSFSheet)sheet).GetRow(i);
                                if (row != null)
                                {
                                    var weather = new Weather();

                                    for (int j = 0; j < row.LastCellNum; j++)
                                    {
                                        var cell = row.GetCell(j);
                                        if (cell != null)
                                        {
                                            switch (j)
                                            {
                                                case 0:
                                                    weather.Date = cell.ToString();
                                                    break;
                                                case 1:
                                                    weather.Time = cell.ToString();
                                                    break;
                                                case 2:
                                                    if (int.TryParse(cell.ToString(), out int temperature))
                                                        weather.T = temperature;
                                                    break;
                                                case 3:
                                                    if (double.TryParse(cell.ToString(), out double humidity))
                                                        weather.Humidity = humidity;
                                                    break;
                                                case 4:
                                                    if (double.TryParse(cell.ToString(), out double td))
                                                        weather.Td = td;
                                                    break;
                                                case 5:
                                                    if (int.TryParse(cell.ToString(), out int pressure))
                                                        weather.Pressure = pressure;
                                                    break;
                                                case 6:
                                                    weather.Direction = cell.ToString();
                                                    break;
                                                case 7:
                                                    if (int.TryParse(cell.ToString(), out int velocity))
                                                        weather.Velocity = velocity;
                                                    break;
                                                case 8:
                                                    if (int.TryParse(cell.ToString(), out int cloudy))
                                                        weather.Cloudy = cloudy;
                                                    break;
                                                case 9:
                                                    if (int.TryParse(cell.ToString(), out int h))
                                                        weather.h = h;
                                                    break;
                                                case 10:
                                                    if (int.TryParse(cell.ToString(), out int vv))
                                                        weather.VV = vv;
                                                    break;
                                                case 11:
                                                    weather.Event = cell.ToString();
                                                    break;
                                            }
                                        }
                                    }

                                    weatherData.Add(weather);
                                }
                            }
                        }
                    }
                }

                foreach (var weather in weatherData)
                {
                    var existingWeather = _context.Weathers.FirstOrDefault(w => w.Date == weather.Date && w.Time == weather.Time);
                    if (existingWeather == null)
                    {
                        _context.Weathers.Add(weather);
                    }
                    else
                    {
                        _context.Entry(existingWeather).CurrentValues.SetValues(weather);
                    }
                }

                _context.SaveChanges();

                ViewBag.Message = "Данные успешно загружены в базу данных.";
                return View("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Ошибка чтения";
                return View("Index");
            }
        }

    }
}
