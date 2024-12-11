using BikeRentalMS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeRentalMS.Database;
using static BikeRentalMS.Repositories.UserRepository;

namespace BikeRentalMS.Repositories
{
    public class UserRepository
    {

            private readonly AppDbContext _context;

            public UserRepository(AppDbContext context)
            {
                _context = context;
            }

            // Get user by username, password, and role
            public async Task<User> GetUserByuserName(string userName)
            {
                return await _context.Users.FirstOrDefaultAsync(u=>u.UserName== userName);
            }

            // Register a new user
            public async Task<bool> RegisterUserAsync(User user)
            {
                await _context.Users.AddAsync(user);
                int result = await _context.SaveChangesAsync();
                return result > 0;
            }

            // Get user by ID
            public async Task<User> GetUserByIdAsync(int userId)
            {
                return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            }

            // Update user details
            public async Task<bool> UpdateUserAsync(User user)
            {
                var existingUser = await _context.Users.FindAsync(user.Id);
                if (existingUser == null) return false;

                existingUser.UserName = user.UserName;
                existingUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
                existingUser.NIC = user.NIC;
                existingUser.Role = user.Role;
                existingUser.Email = user.Email;
                existingUser.PhoneNumber = user.PhoneNumber;

                int result = await _context.SaveChangesAsync();
                return result > 0;
            }

            // Delete a user by ID
            public async Task<bool> DeleteUserAsync(int userId)
            {
                var user = await _context.Users.FindAsync(userId);
                if (user == null) return false;

                _context.Users.Remove(user);
                int result = await _context.SaveChangesAsync();
                return result > 0;
            }

            // Get all users
            public async Task<List<User>> GetAllUsersAsync()
            {
                return await _context.Users.ToListAsync();
        }





    
        

    }
}

