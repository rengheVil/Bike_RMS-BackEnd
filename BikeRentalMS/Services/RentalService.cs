using Azure.Core;
using BikeRentalMS.Dtos.Request;
using BikeRentalMS.Models;
using BikeRentalMS.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;


namespace BikeRentalMS.Services
{
    public class RentalService
    {

        private readonly RentalRepository _rentalRepository;
        private readonly OrderHistoryRepository _orderHistoryRepository;
        private readonly MotorbikeRepository _motorbikeRepository;

        public RentalService(RentalRepository rentalRepository, OrderHistoryRepository orderHistoryRepository, MotorbikeRepository motorbikeRepository)
        {
            _rentalRepository = rentalRepository;
            _orderHistoryRepository = orderHistoryRepository;
            _motorbikeRepository = motorbikeRepository;
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
            await _motorbikeRepository.UpdatReturnStatus(rentalId);
            return await _rentalRepository.DeleteRentalAsync(rentalId);
        }

        // Get all rentals
        //public async Task<List<Rental>> GetAllRentalsAsync()
        //{
        //    return await _rentalRepository.GetAllRentalsAsync();
        //}

        // Get overdue rentals
        public async Task<List<Rental>> GetOverdueRentalsAsync()
        {
            var data = await _rentalRepository.GetAllRentalsAsync();
            var now = DateTime.Now;
            var responseList = new List<Rental>();
            foreach (var rental in data)
            {
                if (now.Subtract(rental.RentDate).Days > 1 && rental.ReturnDate == null)
                {
                    responseList.Add(rental);
                }
            }
            return responseList;
        }

        ////////////
        /// 11/30- 10.23

        public async Task<IEnumerable<Rental>> GetAllActiveRentalsAsync()
        {
            var data = await _rentalRepository.GetAllRentalsAsync();
            var now = DateTime.Now;
            var responseList = new List<Rental>();
            foreach (var rental in data)
            {
                if (now.Subtract(rental.RentDate).Days <= 1 && rental.ReturnDate == null)
                {
                    responseList.Add(rental);
                }
            }
            return responseList;
        }

        public async Task<bool> ReturnRentalAsync(int rentalId)
        {

            var rental = await _rentalRepository.GetRentalByIdAsync(rentalId);
            if (rental == null) return false;

            // await _motorbikeRepository.UpdatReturnStatus(rental.MotorbikeId);
            var getRequest = await _rentalRepository.GetRentalByIdAsync(rentalId);
            var bike = getRequest.Motorbike;
            bike.IsAvailable = true;
            var updated = await _motorbikeRepository.UpdateMotorbikeAsync(bike);
            var orderHistory = new OrderHistory
            {
                MotorbikeId = rental.MotorbikeId,
                UserId = rental.UserId,
                RentDate = rental.RentDate,
                ReturnDate = DateTime.Now
            };

            // Add to OrderHistory and remove rental
            bool addedToHistory = await _orderHistoryRepository.AddOrderHistoryAsync(orderHistory);
            //if (!addedToHistory) return false;

            return await _rentalRepository.DeleteRentalAsync(rentalId);
        }


        //////////
        ///



        public async Task<IEnumerable<RentalDto>> GetAllRentalsAsync()
        {
            var rentals = await _rentalRepository.GetAllRentalsAsync();

            // Map entity to DTO
            return rentals.Select(r => new RentalDto
            {
                id = r.Id,
                UserId = r.UserId,
                Motorbike = new MotorbikeDto
                {

                    RegNumber = r.Motorbike.RegNumber,
                    Brand = r.Motorbike.Brand,
                    Model = r.Motorbike.Model
                },
                RentDate = r.RentDate,
                Status = r.ReturnDate >= DateTime.Now

            });
        }





    }
}

