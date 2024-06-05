using Microsoft.AspNetCore.Mvc;
using VisitorSecurityClearanceSystem.DTO;
using VisitorSecurityClearanceSystem.Interface;

namespace VisitorSecurityClearanceSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OfficeController : Controller
    {
        private readonly IOfficeService _officeService;


        public OfficeController(IOfficeService officeService)
        {
            _officeService = officeService;
        }

        [HttpPost]
        public async Task<OfficeDTO> AddOffice(OfficeDTO officeModel)
        {
            return await _officeService.AddOffice(officeModel);
        }


        [HttpGet("{id}")]
        public async Task<OfficeDTO> GetOfficeById(string id)
        {
            return await _officeService.GetOfficeById(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOffice(string id, OfficeDTO securityModel)
        {
            try
            {
                var updatedSecurity = await _officeService.UpdateOffice(id, securityModel);
                return Ok(updatedSecurity);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateVisitor (Controller): {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOffice(string id)
        {
            await _officeService.DeleteOffice(id);
            return NoContent();
        }
    }
}
