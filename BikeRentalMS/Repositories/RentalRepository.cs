using BikeRentalMS.Database;
using BikeRentalMS.Models;
using Microsoft.EntityFrameworkCore;

namespace BikeRentalMS.Repositories
{
    public class RentalRepository
    {

            private readonly AppDbContext _context;

            public RentalRepository(AppDbContext context)
            {
                _context = context;
            }

            public async Task<bool> AddRentalAsync(Rental rental)
            {
                await _context.Rentals.AddAsync(rental);
                int result = await _context.SaveChangesAsync();
                return result > 0;
            }

            public async Task<Rental> GetRentalByIdAsync(int rentalId)
            {
                return await _context.Rentals.FirstOrDefaultAsync(r => r.Id == rentalId);
            }

            public async Task<bool> DeleteRentalAsync(int rentalId)
            {
                var rental = await _context.Rentals.FindAsync(rentalId);
                if (rental == null) return false;

                _context.Rentals.Remove(rental);
                int result = await _context.SaveChangesAsync();
                return result > 0;
            }

            public async Task<List<Rental>> GetAllRentalsAsync()
            {
                return await _context.Rentals.Include(rental => rental.User).ToListAsync();
            }

        public async Task<List<OrderHistory>> GetOverdueRentalsAsync()
        {
            return await _context.OrderHistorys
                .Where(r => r.ReturnDate == null && EF.Functions.DateDiffMinute(r.RentDate, DateTime.Now) > 1)
                .ToListAsync();
        }
    }
    }


