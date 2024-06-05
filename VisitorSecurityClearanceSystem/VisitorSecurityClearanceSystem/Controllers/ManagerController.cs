using Microsoft.AspNetCore.Mvc;
using VisitorSecurityClearanceSystem.DTO;
using VisitorSecurityClearanceSystem.Interface;
using VisitorSecurityClearanceSystem.Services;

namespace VisitorSecurityClearanceSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ManagerController : Controller
    {
        private readonly IManagerService _managerService;
        private readonly IOfficeService _officeService;
        private readonly ISecurityService _securityService;


        public ManagerController(IManagerService managerService, IOfficeService officeService, ISecurityService securityService)
        {
            _managerService = managerService;
            _officeService = officeService;
            _securityService = securityService;
        }

        [HttpPost]
        public async Task<ManagerDTO> AddManager(ManagerDTO managerModel)
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
        [HttpPost]
        public async Task<IActionResult> AddOfficeUser(OfficeDTO officeModel)
        {
            var office = await _officeService.AddOffice(officeModel);
            return Ok(office);
        }

        [HttpPost]
        public async Task<IActionResult> AddSecurityUser(SecurityDTO securityModel)
        {
            var security = await _securityService.AddSecurity(securityModel);
            return Ok(security);
        }


        /*[HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSecurity(string id)
        {
            await _managerService.DeleteManager(id);
            return NoContent();
        }*/
    }
}
