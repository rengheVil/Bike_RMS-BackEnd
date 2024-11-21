﻿using BikeRentalMS.Models;
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
            public async Task<IActionResult> AddRentalRequest([FromBody] RentalRequest request)
            {
                if (request == null)
                {
                    return BadRequest("Invalid rental request.");
                }

                try
                {
                    bool isAdded = await _requestService.AddRentalRequestAsync(request);

                    if (isAdded)
                    {
                        return CreatedAtAction(nameof(GetRentalRequestById), new { id = request.Id }, request);
                    }

                    return StatusCode(500, "Error while adding rental request.");
                }
                catch (Exception ex)
                {
                    // Log the exception
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }

            [HttpPut("{requestId}/status")]
            public async Task<IActionResult> UpdateRentalRequestStatus(int requestId, [FromBody] UpdateStatusRequest statusRequest)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid status update request.");
                }

                try
                {
                    bool isUpdated = await _requestService.UpdateRentalRequestStatusAsync(requestId, statusRequest.Status, statusRequest.ApprovalDate);

                    if (isUpdated)
                    {
                        return NoContent(); // 204 No Content for successful updates
                    }

                    return NotFound("Rental request not found or status update failed.");
                }
                catch (Exception ex)
                {
                    // Log the exception
                    return StatusCode(500, $"Internal server error: {ex.Message}");
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


