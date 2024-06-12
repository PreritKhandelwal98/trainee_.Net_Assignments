using EmployeeManagementSystem.DTO;
using EmployeeManagementSystem.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeBasicDetailsController : Controller
    {
        private readonly IEmployeeBasicDetailsService _basicDetailService;

        public EmployeeBasicDetailsController(IEmployeeBasicDetailsService basicDetailService)
        {
            _basicDetailService = basicDetailService;
        }

        [HttpPost]
        public async Task<EmployeeBasicDetailsDTO> AddBasicDetail(EmployeeBasicDetailsDTO basicDetailsDTO)
        {
            return await _basicDetailService.AddEmployeeBasicDetails(basicDetailsDTO);
        }

        [HttpGet]
        public async Task<IEnumerable<EmployeeBasicDetailsDTO>> GetAllEmployeeBasicDetails()
        {
            return await _basicDetailService.GetAllEmployeeBasicDetails();
        }

        [HttpGet("{id}")]
        public async Task<EmployeeBasicDetailsDTO> GetEmployeeBasicDetailsById(string id)
        {
            return await _basicDetailService.GetEmployeeBasicDetailsById(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployeeBasicDetails(string id, EmployeeBasicDetailsDTO basicDetailsDTO)
        {
            try
            {
                var updatedEmployee = await _basicDetailService.UpdateEmployeeBasicDetails(id, basicDetailsDTO);
                return Ok(updatedEmployee);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateVisitor (Controller): {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeBasiclDetails(string id)
        {
            await _basicDetailService.DeleteEmployeeBasicDetails(id);
            return NoContent();
        }
    }
}
