using BikeRentalMS.Models;
using BikeRentalMS.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace BikeRentalMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalController : ControllerBase
    {

        private readonly RentalService _rentalService;

        public RentalController(RentalService rentalService)
        {
            _rentalService = rentalService;
        }

        [HttpPost]
        public async Task<IActionResult> AddRental([FromBody] Rental rental)
        {
            try
            {
                await _rentalService.AddRentalAsync(rental);
                return Ok("Rental added successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to add rental.{ex.Message}");
            }

        }

        [HttpGet("{rentalId}")]
        public async Task<IActionResult> GetRentalById(int rentalId)
        {
            var rental = await _rentalService.GetRentalByIdAsync(rentalId);
            if (rental == null)
                return NotFound("Rental not found.");
            return Ok(rental);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRentals()
        {
            return Ok(await _rentalService.GetAllRentalsAsync());
        }

        [HttpDelete("{rentalId}")]
        public async Task<IActionResult> DeleteRental(int rentalId)
        {
            if (await _rentalService.DeleteRentalAsync(rentalId))
                return Ok("Rental deleted successfully.");
            return NotFound("Rental not found.");
        }

        [HttpGet("overdue")]
        public async Task<IActionResult> GetOverdueRentals()
        {
            return Ok(await _rentalService.GetOverdueRentalsAsync());
        }
    }
}




