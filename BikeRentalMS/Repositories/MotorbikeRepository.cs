using BikeRentalMS.Database;
using BikeRentalMS.Models;
using Microsoft.EntityFrameworkCore;

namespace BikeRentalMS.Repositories
{
    public class MotorbikeRepository
    {
      
            private readonly AppDbContext _context;

            public MotorbikeRepository(AppDbContext context)
            {
                _context = context;
            }

            public async Task<bool> AddMotorbikeAsync(Motorbike motorbike)
            {
                _context.Motorbikes.Add(motorbike);
                return await _context.SaveChangesAsync() > 0;
            }

            public async Task<Motorbike> GetMotorbikeByIdAsync(int motorbikeId)
            {
                return await _context.Motorbikes.FindAsync(motorbikeId);
            }

            public async Task<bool> UpdateMotorbikeAsync(Motorbike motorbike)
            {
                _context.Motorbikes.Update(motorbike);
                return await _context.SaveChangesAsync() > 0;
            }

            public async Task<bool> DeleteMotorbikeAsync(int motorbikeId)
            {
                var motorbike = await _context.Motorbikes.FindAsync(motorbikeId);
                if (motorbike == null) return false;

                _context.Motorbikes.Remove(motorbike);
                return await _context.SaveChangesAsync() > 0;
            }

            public async Task<List<Motorbike>> GetAllMotorbikesAsync()
            {
                return await _context.Motorbikes.ToListAsync();
            }
        }
    }



    