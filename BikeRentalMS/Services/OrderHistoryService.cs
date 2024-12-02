using BikeRentalMS.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using BikeRentalMS.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using BikeRentalMS.Dtos.Response;


namespace BikeRentalMS.Services
{
    public class OrderHistoryService
    {
        
            private readonly OrderHistoryRepository _orderRepository;

            public OrderHistoryService(OrderHistoryRepository orderRepository)
            {
                _orderRepository = orderRepository;
            }

            // Add an order history
            public async Task<bool> AddOrderHistoryAsync(OrderHistory order)
            {
                return await _orderRepository.AddOrderHistoryAsync(order);
            }

            // Get all order histories
            public async Task<List<OrderHistoryResponseDTO>> GetAllOrderHistoriesAsync()
            {
                return await _orderRepository.GetAllOrderHistoriesAsync();
            }

        /// get Customer Order History
        /// 
        public async Task<IEnumerable<OrderHistory>> GetOrderHistoryByUserIdAsync(int userId)
        {
            return await _orderRepository.GetOrderHistoryByUserIdAsync(userId);
        }

        //public async Task<OrderHistory> CreateOrderHistoryAsync(OrderHistory orderHistory)
        //{
        //    var newOrderHistory = await _orderRepository.AddOrderHistoryAsync(orderHistory);
        //    await _orderRepository.SaveChangesAsync();
        //    return newOrderHistory;
        //}



    }
    }

