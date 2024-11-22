﻿using BikeRentalMS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeRentalMS.Database;

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
            public async Task<User> GetUserAsync(string username, string password, string role)
            {
                return await _context.Users
                    .FirstOrDefaultAsync(u => u.UserName == username && u.Password == password && u.Role == role);
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
                existingUser.Password = user.Password;
                existingUser.NIC = user.NIC;
                existingUser.Role = user.Role;

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
