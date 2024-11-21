﻿using BikeRentalMS.Models;
using BikeRentalMS.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace BikeRentalMS.Services
{
    public class UserService
    {

            private readonly UserRepository _userRepository;

            public UserService(UserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            // Login a user
            public async Task<User> LoginAsync(string username, string password, string role)
            {
                return await _userRepository.GetUserAsync(username, password, role);
            }

            // Register a user
            public async Task<bool> RegisterAsync(User user)
            {
                var existingUser = await _userRepository.GetUserAsync(user.UserName, user.Password, user.Role);
                if (existingUser != null)
                {
                    // User with the same username already exists
                    return false;
                }
                return await _userRepository.RegisterUserAsync(user);
            }

            // Get user by ID
            public async Task<User> GetUserByIdAsync(int userId)
            {
                return await _userRepository.GetUserByIdAsync(userId);
            }

            // Update a user
            public async Task<bool> UpdateUserAsync(User user)
            {
                return await _userRepository.UpdateUserAsync(user);
            }

            // Delete a user
            public async Task<bool> DeleteUserAsync(int userId)
            {
                return await _userRepository.DeleteUserAsync(userId);
            }

            // Get all users
            public async Task<List<User>> GetAllUsersAsync()
            {
                return await _userRepository.GetAllUsersAsync();
            }
        }
    }


