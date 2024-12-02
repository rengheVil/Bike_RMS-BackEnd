using BikeRentalMS.Database;
using BikeRentalMS.Dtos.Request;
using BikeRentalMS.Dtos.Response;
using BikeRentalMS.Models;
using Microsoft.EntityFrameworkCore;

namespace BikeRentalMS.Repositories
{
    public class OrderHistoryRepository
    {
       
            private readonly AppDbContext _context;

            public OrderHistoryRepository(AppDbContext context)
            {
                _context = context;
            }

            public async Task<bool> AddOrderHistoryAsync(OrderHistory order)
            {
                await _context.OrderHistorys.AddAsync(order);
                int result = await _context.SaveChangesAsync();
                return result > 0;
            }

        public async Task<List<OrderHistoryResponseDTO>> GetAllOrderHistoriesAsync()
        {
            return await _context.OrderHistorys
                .Include(b => b.Motorbike) 
                .Select(order => new OrderHistoryResponseDTO
                {
                    Id = order.Id,
                    MotorbikeId = order.Motorbike.Id,
                    UserId = order.UserId,
                    Brand = order.Motorbike.Brand,
                    Modal = order.Motorbike.Model,
                    RentDate = order.RentDate,
                    ReturnDate = order.ReturnDate
                })
                .ToListAsync();
        }

        /// Order History get UserId

        public async Task<IEnumerable<OrderHistory>> GetOrderHistoryByUserIdAsync(int userId)
        {
            return await _context.OrderHistorys
                .Include(o => o.Motorbike)
                .Where(o => o.UserId == userId)
                .ToListAsync();
        }

        //public async Task<OrderHistory> AddOrderHistoryAsync(OrderHistory orderHistory)
        //{
        //    var result = await _context.OrderHistorys.AddAsync(orderHistory);
        //    return result.Entity;
        //}

        //public async Task SaveChangesAsync()
        //{
        //    await _context.SaveChangesAsync();
        //}

    }
}

