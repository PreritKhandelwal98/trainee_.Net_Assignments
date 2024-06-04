using Microsoft.AspNetCore.Mvc;
using Retail_Shop.DTO;
using Retail_Shop.Interface;

namespace Retail_Shop.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RetailShopController : Controller
    {
        private readonly IRetailShopService _retailShopService;

        public RetailShopController(IRetailShopService retailShopService)
        {
            _retailShopService = retailShopService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRetailShop(RetailShopDTO retailShopDTO)
        {
            try
            {
                var createdRetailShop = await _retailShopService.CreateRetailShop(retailShopDTO);
                return CreatedAtAction(nameof(GetRetailShopById), new { id = createdRetailShop.Id }, createdRetailShop);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here
                Console.WriteLine($"Error in CreateRetailShop (Controller): {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRetailShop(string id, RetailShopDTO retailShopDTO)
        {
            try
            {
                var updatedRetailShop = await _retailShopService.UpdateRetailShop(id, retailShopDTO);
                return Ok(updatedRetailShop);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here
                Console.WriteLine($"Error in UpdateRetailShop (Controller): {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRetailShopById(string id)
        {
            try
            {
                var retailShop = await _retailShopService.GetRetailShopById(id);
                if (retailShop == null)
                {
                    return NotFound();
                }
                return Ok(retailShop);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here
                Console.WriteLine($"Error in GetRetailShopById (Controller): {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
