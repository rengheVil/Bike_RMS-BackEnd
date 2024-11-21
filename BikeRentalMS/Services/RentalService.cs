using BikeRentalMS.Models;
using BikeRentalMS.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace BikeRentalMS.Services
{
    public class RentalService
    {

            private readonly RentalRepository _rentalRepository;

            public RentalService(RentalRepository rentalRepository)
            {
                _rentalRepository = rentalRepository;
            }

            // Add a rental
            public async Task<bool> AddRentalAsync(Rental rental)
            {
                return await _rentalRepository.AddRentalAsync(rental);
            }

            // Get rental by ID
            public async Task<Rental> GetRentalByIdAsync(int rentalId)
            {
                return await _rentalRepository.GetRentalByIdAsync(rentalId);
            }

            // Delete a rental
            public async Task<bool> DeleteRentalAsync(int rentalId)
            {
                return await _rentalRepository.DeleteRentalAsync(rentalId);
            }

            // Get all rentals
            public async Task<List<Rental>> GetAllRentalsAsync()
            {
                return await _rentalRepository.GetAllRentalsAsync();
            }

            // Get overdue rentals
            public async Task<List<OrderHistory>> GetOverdueRentalsAsync()
            {
                return await _rentalRepository.GetOverdueRentalsAsync();
            }
        }
    }

