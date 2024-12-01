using BikeRentalMS.Dtos.Request;
using BikeRentalMS.Models;
using BikeRentalMS.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace BikeRentalMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalRequestController : ControllerBase
    {

            private readonly RentalRequestService _requestService;

            public RentalRequestController(RentalRequestService requestService)
            {
                _requestService = requestService;
            }

            [HttpPost]
            public async Task<IActionResult> AddRentalRequest([FromBody] RentalRequestDTO requestDto)
            {
                   var data = await _requestService.AddRentalRequestAsync(requestDto);
                    // return CreatedAtAction(nameof(GetRentalRequestById), new { id = requestDto.Id }, requestDto);
                    return Ok(data);
              
            }

            [HttpGet("{requestId}/update")]
            public async Task<IActionResult> UpdateRentalRequestStatus(int requestId)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid status update request.");
                }

                try
                {
                    bool isUpdated = await _requestService.ApproveRentalRequestAsync(requestId);

                    if (isUpdated)
                    {
                        return NoContent(); 
                    }

                    return NotFound("Rental request not found or status update failed.");
                }
                catch (Exception ex)
                {
                    // Log the exception
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }

        ///      ----------approve user============================

        [HttpGet("Approvals/{userId}")]
        public async Task<IActionResult> GetUserApprovals(int userId)
        {
            try
            {
                var approvals = await _requestService.GetUserApprovalsAsync(userId);
                if (approvals == null)
                {
                    throw new Exception("no data for this person");
                }
                return Ok(approvals);
            }
            catch (Exception ex)
            {
                {
                    return BadRequest(ex.Message);
                }
            }
        }



        [HttpGet("{requestId}")]
            public async Task<IActionResult> GetRentalRequestById(int requestId)
            {
                try
                {
                    var request = await _requestService.GetRentalRequestByIdAsync(requestId);
                    if (request == null)
                    {
                        return NotFound("Rental request not found.");
                    }

                    return Ok(request);
                }
                catch (Exception ex)
                {
                    // Log the exception
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }

            [HttpGet]
            public async Task<IActionResult> GetAllRentalRequests()
            {
                try
                {
                    var requests = await _requestService.GetAllRentalRequestsAsync();
                    return Ok(requests);
                }
                catch (Exception ex)
                {
                    // Log the exception
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }
        }
    }


