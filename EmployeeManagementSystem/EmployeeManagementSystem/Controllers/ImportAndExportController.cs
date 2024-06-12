using EmployeeManagementSystem.DTO;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Drawing;

namespace EmployeeManagementSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ImportAndExportController : Controller
    {
        private string GetStringFromCell(ExcelWorksheet worksheet, int row, int column)
        {
            var cellValue = worksheet.Cells[row, column].Value;
            return cellValue?.ToString()?.Trim();
        }

        [HttpPost("ImportExcel")]
        public async Task<IActionResult> ImportExcel(IFormFile formFile)
        {
            if (formFile == null || formFile.Length == 0)
            {
                return BadRequest("File is empty or null");
            }

            var visitors = new List<EmployeeAdditionalDetailsDTO>();
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (var stream = new MemoryStream())
            {
                await formFile.CopyToAsync(stream); // Ensure async copying
                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        var student = new EmployeeAdditionalDetailsDTO
                        {
                            Id = GetStringFromCell(worksheet, row, 2),
                            Name = GetStringFromCell(worksheet, row, 3),
                            Email = GetStringFromCell(worksheet, row, 4),
                            Phone = GetStringFromCell(worksheet, row, 5),
                            Address = GetStringFromCell(worksheet, row, 6),
                            CompanyName = GetStringFromCell(worksheet, row, 7),
                            Purpose = GetStringFromCell(worksheet, row, 8),
                            EntryTime = Convert.ToDateTime(GetStringFromCell(worksheet, row, 9)),
                            ExitTime = Convert.ToDateTime(GetStringFromCell(worksheet, row, 10)),
                            PassStatus = Convert.ToBoolean(GetStringFromCell(worksheet, row, 11)),
                            Role = GetStringFromCell(worksheet, row, 12),

                        };
                        await AddVisitor(student); // Ensure async method is awaited

                        visitors.Add(student);
                    }
                }
            }
            return Ok(visitors);
        }

        [HttpGet("ExportInExcel")]
        public async Task<IActionResult> Export()
        {
            var visitors = await _additionalDetailService.GetAllVisitors();

            // Ensure visitors is not null
            if (visitors == null)
            {
                return NotFound("No Visitor found to export.");
            }

            var visitorList = visitors.ToList();

            // Ensure visitorList is not empty
            if (!visitorList.Any())
            {
                return NotFound("No students found to export.");
            }

            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Students");

                // Add Header
                worksheet.Cells[1, 1].Value = "Id";
                worksheet.Cells[1, 2].Value = "Name";
                worksheet.Cells[1, 3].Value = "Email";
                worksheet.Cells[1, 4].Value = "Phone";
                worksheet.Cells[1, 5].Value = "Address";
                worksheet.Cells[1, 6].Value = "CompanyName";
                worksheet.Cells[1, 7].Value = "Purpose";
                worksheet.Cells[1, 8].Value = "EntryTime";
                worksheet.Cells[1, 9].Value = "ExitTime";
                worksheet.Cells[1, 10].Value = "PassStatus";
                worksheet.Cells[1, 11].Value = "Role";

                // Set Header Style
                using (var range = worksheet.Cells[1, 1, 1, 11])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.LightGreen);
                }

                for (int i = 0; i < visitorList.Count; i++)
                {
                    var student = visitorList[i];
                    worksheet.Cells[i + 2, 1].Value = student.Id;
                    worksheet.Cells[i + 2, 2].Value = student.Name;
                    worksheet.Cells[i + 2, 3].Value = student.Email;
                    worksheet.Cells[i + 2, 4].Value = student.Phone;
                    worksheet.Cells[i + 2, 5].Value = student.Address;
                    worksheet.Cells[i + 2, 6].Value = student.CompanyName;
                    worksheet.Cells[i + 2, 7].Value = student.Purpose;
                    worksheet.Cells[i + 2, 8].Value = student.EntryTime;
                    worksheet.Cells[i + 2, 9].Value = student.ExitTime;
                    worksheet.Cells[i + 2, 10].Value = student.PassStatus;
                    worksheet.Cells[i + 2, 11].Value = student.Role;
                }

                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                var fileName = "Visitors.xlsx";
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }
    }
}
