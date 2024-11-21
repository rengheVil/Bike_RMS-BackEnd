using BikeRentalMS.Database;
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

            public async Task<List<OrderHistory>> GetAllOrderHistoriesAsync()
            {
                return await _context.OrderHistorys.ToListAsync();
            }
        }
    }

