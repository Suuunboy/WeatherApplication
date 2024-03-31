using Microsoft.AspNetCore.Mvc;
using NPOI.XSSF.UserModel; 
using NPOI.SS.UserModel;

namespace WeatherApplication.Controllers
{
    public class WeatherController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Message = "Привет, мир!";

            return View();
        }

        [HttpPost]
        public IActionResult UploadFile(IFormFile file)
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

            List<string[]> data = new List<string[]>();

            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                stream.Position = 0;

                XSSFWorkbook workbook = new XSSFWorkbook(stream);
                XSSFSheet sheet = (XSSFSheet)workbook.GetSheetAt(0);

                for (int i = 2; i <= sheet.LastRowNum; i++)
                {
                    IRow row = sheet.GetRow(i);
                    List<string> rowData = new List<string>();
                    if (row != null)
                    {
                        for (int j = 0; j < row.LastCellNum; j++)
                        {
                            rowData.Add(row.GetCell(j).ToString());
                        }
                    }
                    data.Add(rowData.ToArray());
                }

            }

            ViewBag.Data = data;
            return View("Index");
        }
    }
}
