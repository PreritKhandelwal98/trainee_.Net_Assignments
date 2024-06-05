using Microsoft.AspNetCore.Mvc;
using VisitorSecurityClearanceSystem.DTO;
using VisitorSecurityClearanceSystem.Interface;

namespace VisitorSecurityClearanceSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ManagerController : Controller
    {
        private readonly IManagerService _managerService;

        public ManagerController(IManagerService managerService)
        {
            _managerService = managerService;
        }

        [HttpPost]
        public async Task<ManagerDTO> AddVisitor(ManagerDTO managerModel)
        {
            return await _managerService.AddManager(managerModel);
        }


        [HttpGet("{id}")]
        public async Task<ManagerDTO> GetManagerById(string id)
        {
            return await _managerService.GetManagerById(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateManager(string id, ManagerDTO managerModel)
        {

            try
            {
                var updatedManager = await _managerService.UpdateManager(id, managerModel);
                return Ok(updatedManager);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateVisitor (Controller): {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSecurity(string id)
        {
            await _managerService.DeleteManager(id);
            return NoContent();
        }
    }
}
