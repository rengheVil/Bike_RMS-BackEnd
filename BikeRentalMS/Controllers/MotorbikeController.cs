using BikeRentalMS.Dtos.Request;
using BikeRentalMS.Models;
using BikeRentalMS.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BikeRentalMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotorbikeController : ControllerBase
    {

            private readonly MotorbikeService _motorbikeService;

            public MotorbikeController(MotorbikeService motorbikeService)
            {
                _motorbikeService = motorbikeService;
            }

            [HttpPost]
            public async Task<IActionResult> AddMotorbike(MotorbikeRequest motorbikeRequest)
            {
                if (await _motorbikeService.AddMotorbikeAsync(motorbikeRequest))
                {
                    return Ok(new { message = "Motorbike added successfully." });
                }
                return BadRequest(new { message = "Failed to add motorbike." });
            }

            [HttpGet("{motorbikeId}")]
            public async Task<IActionResult> GetMotorbikeById(int motorbikeId)
            {
                var motorbike =await _motorbikeService.GetMotorbikeByIdAsync(motorbikeId);
                if (motorbike == null)
                {
                    return NotFound(new { message = "Motorbike not found." });
                }
                return Ok(motorbike);
            }

            [HttpPut]
            public async Task<IActionResult> UpdateMotorbike([FromBody] Motorbike motorbike)
            {
                if (await _motorbikeService.UpdateMotorbikeAsync(motorbike))
                {
                    return Ok(new { message = "Motorbike updated successfully." });
                }
                return BadRequest(new { message = "Failed to update motorbike." });
            }

            [HttpDelete("{motorbikeId}")]
            public async Task<IActionResult> DeleteMotorbike(int motorbikeId)
            {
                if (await _motorbikeService.DeleteMotorbikeAsync(motorbikeId))
                {
                    return Ok(new { message = "Motorbike deleted successfully." });
                }
                return NotFound(new { message = "Motorbike not found." });
            }

            [HttpGet]
            public async Task<IActionResult> GetAllMotorbikes()
            {
                return Ok(await _motorbikeService.GetAllMotorbikesAsync());
            }

        [HttpGet("countbike")]
        public async Task<IActionResult> GetBikeCountAsync()
        {
            var count = await _motorbikeService.GetBikeCountAsync();
            return Ok(count);
        }

        


    }
    }




