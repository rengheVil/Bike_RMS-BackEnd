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

        ////////////
        /// 11/30- 10.23

        public async Task<IEnumerable<Rental>> GetAllActiveRentalsAsync()
        {
            return await _rentalRepository.GetAllActiveRentalsAsync();
        }

        public async Task<bool> ReturnRentalAsync(int rentalId)
        {

            var rental = await _rentalRepository.GetRentalByIdAsync(rentalId);
            if (rental == null) return false;


            var orderHistory = new OrderHistory
            {
                MotorbikeId = rental.MotorbikeId,
                UserId = rental.UserId,
                RentDate = rental.RentDate,
                ReturnDate = DateTime.Now
            };

            // Add to OrderHistory and remove rental
            //bool addedToHistory = await _rentalRepository.AddOrderHistoryAsync(orderHistory);
            //if (!addedToHistory) return false;

            return await _rentalRepository.DeleteRentalAsync(rentalId);
        }




    }
    }

