using BikeRentalMS.Models;
using BikeRentalMS.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BikeRentalMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderHistoryController : ControllerBase
    {

            private readonly OrderHistoryService _orderService;

            public OrderHistoryController(OrderHistoryService orderService)
            {
                _orderService = orderService;
            }

            [HttpPost]
            public async Task<IActionResult> AddOrderHistory([FromBody] OrderHistory order)
            {
                if (await _orderService.AddOrderHistoryAsync(order))
                {
                    return Ok("Order history entry added.");
                }

                return BadRequest("Failed to add order history entry.");
            }

            [HttpGet]
            public async Task<IActionResult> GetAllOrderHistories()
            {
                return Ok(await _orderService.GetAllOrderHistoriesAsync());
            }

            // get  orderhistory/user/{userId}
            [HttpGet("user/{userId}")]
            public async Task<IActionResult> GetOrderHistoryByUserId(int userId)
            {
                var orderHistories = await _orderService.GetOrderHistoryByUserIdAsync(userId);

                if (orderHistories == null || !orderHistories.Any())
                {
                    return NotFound($"No order history found for user ID {userId}.");
                }

                return Ok(orderHistories.Select(o => new
                {
                    o.Id,
                    Motorbike = new
                    {
                        o.Motorbike.RegNumber,
                        o.Motorbike.Brand,
                        o.Motorbike.Model,
                        o.Motorbike.Category
                    },
                    o.RentDate,
                    o.ReturnDate
                }));
            } 

    }
 }


