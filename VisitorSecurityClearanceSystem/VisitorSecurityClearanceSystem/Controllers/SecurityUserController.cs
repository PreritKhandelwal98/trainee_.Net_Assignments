using Microsoft.AspNetCore.Mvc;
using VisitorSecurityClearanceSystem.DTO;
using VisitorSecurityClearanceSystem.Interface;
using VisitorSecurityClearanceSystem.Services;

namespace VisitorSecurityClearanceSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SecurityUserController : Controller
    {
        private readonly ISecurityService _securityService;

        public SecurityUserController(ISecurityService securityService)
        {
            _securityService = securityService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginModel)
        {
            var securityUser = await _securityService.LoginSecurityUser(loginModel.Email, loginModel.Password);
            if (securityUser == null)
            {
                return Unauthorized("Invalid credentials");
            }

            return Ok(securityUser);
        }


        [HttpGet("{id}")]
        public async Task<SecurityDTO> GetSecurityById(string id)
        {
            return await _securityService.GetSecurityById(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSecurity(string id, SecurityDTO securityModel)
        {
            try
            {
                var updatedSecurity = await _securityService.UpdateSecurity(id, securityModel);
                return Ok(updatedSecurity);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateVisitor (Controller): {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /*[HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSecurity(string id)
        {
            await _securityService.DeleteSecurity(id);
            return NoContent();
        }*/
    }
}
