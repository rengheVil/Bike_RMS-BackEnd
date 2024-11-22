using BikeRentalMS.Database;
using BikeRentalMS.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


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
                await _context.Motorbikes.AddAsync(motorbike);
                return await _context.SaveChangesAsync() > 0;
            }

            public async Task<Motorbike> GetMotorbikeByIdAsync(int motorbikeId)
            {
                return await _context.Motorbikes.FindAsync(motorbikeId);
            }

            public async Task<bool> UpdateMotorbikeAsync(Motorbike motorbike)
            {
                var existingMotorbike = await _context.Motorbikes.FindAsync(motorbike.Id);
                if (existingMotorbike == null) return false;

                existingMotorbike.RegNumber = motorbike.RegNumber;
                existingMotorbike.Brand = motorbike.Brand;
                existingMotorbike.Model = motorbike.Model;
                existingMotorbike.Category = motorbike.Category;
                existingMotorbike.ImageData = motorbike.ImageData;

                _context.Motorbikes.Update(existingMotorbike);
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

   



    