using EmployeeManagementSystem.DTO;
using EmployeeManagementSystem.Entities;
using EmployeeManagementSystem.Interface;
using EmployeeManagementSystem.ServiceFilters;
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
            var createdBasicDetail = await _basicDetailService.AddEmployeeBasicDetails(basicDetailsDTO);
            return createdBasicDetail;
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



        [HttpPost]
        [ServiceFilter(typeof(BuildEmployeeFilter))]
        public async Task<EmployeeFilterCriteria> GetAllEmployeesByPagination(EmployeeFilterCriteria employeeFilterCriteria)
        {
            var response = await _basicDetailService.GetAllEmployeesByPagination(employeeFilterCriteria);
            return response;
        }

        [HttpPost]
        public async Task<IActionResult> AddVisitorByMakePostRequest(VisitorDTO visitor)
        {
            var response = await _basicDetailService.AddVisitorByMakePostRequest(visitor);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetVisitorByMakePostRequest()
        {
            var response = await _basicDetailService.GetVisitorByMakePostRequest();
            return Ok(response);
        }


    }
}
