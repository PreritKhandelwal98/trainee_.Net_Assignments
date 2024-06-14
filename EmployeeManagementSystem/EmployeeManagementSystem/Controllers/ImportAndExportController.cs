using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Drawing;
using EmployeeManagementSystem.Interface;

namespace EmployeeManagementSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ImportAndExportController : Controller
    {
        private readonly IEmployeeBasicDetailsService _basicDetailsService;
        private readonly IEmployeeAdditionalDetailsService _additionalDetailsService;

        public ImportAndExportController(IEmployeeBasicDetailsService basicDetailsService, IEmployeeAdditionalDetailsService additionalDetailsService)
        {
            _basicDetailsService = basicDetailsService;
            _additionalDetailsService = additionalDetailsService;
        }

        private string GetStringFromCell(ExcelWorksheet worksheet, int row, int column)
        {
            var cellValue = worksheet.Cells[row, column].Value;
            return cellValue?.ToString()?.Trim();
        }

        /*[HttpPost("ImportExcel")]
        public async Task<IActionResult> ImportExcel(IFormFile formFile)
        {
            if (formFile == null || formFile.Length == 0)
            {
                return BadRequest("File is empty or null");
            }

            var visitors = new List<VisitorDTO>();
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
                        var student = new VisitorDTO
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
        }*/

        [HttpGet("ExportInExcel")]
        public async Task<IActionResult> Export()
        {
            var basicDetails = await _basicDetailsService.GetAllEmployeeBasicDetails();
            var additionalDetails = await _additionalDetailsService.GetAllEmployeeAdditionalDetails();

            // Merge data based on EmployeeID
            var dataToExport = from basic in basicDetails
                               join additional in additionalDetails on basic.EmployeeID equals additional.EmployeeBasicDetailsUId
                               select new
                               {
                                   SrNo = basic.EmployeeID,
                                   FirstName = basic.FirstName,
                                   LastName = basic.LastName,
                                   Email = basic.Email,
                                   Phone = basic.Mobile,
                                   ReportingManagerName = basic.ReportingManagerName,
                                   DateOfBirth = additional.PersonalDetails.DateOfBirth,
                                   DateOfJoining = additional.WorkInformation.DateOfJoining
                               };

            if (!dataToExport.Any())
            {
                return NotFound("No data found to export.");
            }

            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Employees");

                // Add Header
                worksheet.Cells[1, 1].Value = "Sr.No";
                worksheet.Cells[1, 2].Value = "First Name";
                worksheet.Cells[1, 3].Value = "Last Name";
                worksheet.Cells[1, 4].Value = "Email";
                worksheet.Cells[1, 5].Value = "Phone No";
                worksheet.Cells[1, 6].Value = "Reporting Manager Name";
                worksheet.Cells[1, 7].Value = "Date Of Birth";
                worksheet.Cells[1, 8].Value = "Date of Joining";

                // Set Header Style
                using (var range = worksheet.Cells[1, 1, 1, 8])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.LightGreen);
                }

                var rowIndex = 2;
                foreach (var data in dataToExport)
                {
                    worksheet.Cells[rowIndex, 1].Value = data.SrNo;
                    worksheet.Cells[rowIndex, 2].Value = data.FirstName;
                    worksheet.Cells[rowIndex, 3].Value = data.LastName;
                    worksheet.Cells[rowIndex, 4].Value = data.Email;
                    worksheet.Cells[rowIndex, 5].Value = data.Phone;
                    worksheet.Cells[rowIndex, 6].Value = data.ReportingManagerName;
                    worksheet.Cells[rowIndex, 7].Value = data.DateOfBirth.ToShortDateString();
                    worksheet.Cells[rowIndex, 8].Value = data.DateOfJoining.ToShortDateString();
                    rowIndex++;
                }

                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                var fileName = "Employees.xlsx";
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }
    }
}
