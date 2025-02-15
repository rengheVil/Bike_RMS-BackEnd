using BikeRentalMS.Dtos.Request;
using BikeRentalMS.Models;
using BikeRentalMS.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static BikeRentalMS.Repositories.RentalRequestRepository;


namespace BikeRentalMS.Services
{
    public class RentalRequestService
    {

        private readonly RentalRequestRepository _requestRepository;
        private readonly RentalRepository _rentalRepository;
        private readonly MotorbikeRepository _motorbikeRepository;

        public RentalRequestService(RentalRequestRepository rentalRequestRepository, RentalRepository rentalRepository, MotorbikeRepository motorbikeRepository)
        {
            _requestRepository = rentalRequestRepository;
            _rentalRepository = rentalRepository;
            _motorbikeRepository = motorbikeRepository;
        }

        public async Task<RentalRequest> AddRentalRequestAsync(RentalRequestDTO rentalRequest)
        {
            var rental = new RentalRequest
            {
                Status = "pending",
                MotorbikeId = rentalRequest.MotorbikeId,
                UserId = rentalRequest.UserId,
                RequestDate = DateTime.Now
            };
            var data = await _requestRepository.AddRentalRequestAsync(rental);
            return data;
        }

        public async Task<bool> ApproveRentalRequestAsync(int requestId)
        {
            var rentalRequest = await _requestRepository.GetRentalRequestByIdAsync(requestId);
            if (rentalRequest == null || rentalRequest.Status != "pending") return false;

            var result = await _requestRepository.ApproveRentalRequestAsync(requestId);
            var getRequest = await _requestRepository.GetRentalRequestByIdAsync(requestId);
            var bike = getRequest.Motorbike;
            bike.IsAvailable = false;
            var updated = await _motorbikeRepository.UpdateMotorbikeAsync(bike);


            var rental = new Rental
            {
                MotorbikeId = rentalRequest.MotorbikeId,
                UserId = rentalRequest.UserId,
                RentDate = DateTime.Now
            };
            var data = await _rentalRepository.AddRentalAsync(rental);
            return data;
        }

        ///-----------------------------------------rental--
        public async Task<List<RentalRequest>> GetUserApprovalsAsync(int userId)
        {
            var data = await _requestRepository.GetApprovedRequestsByUserIdAsync(userId);
            // var Motorbike = data[0].Motorbike;
            return data;
        }


        public async Task<bool> RejectRentalRequest(int requestId)
        {
            return await _requestRepository.RejectRentalRequest(requestId);
        }

        public async Task<List<RentalRequest>> GetAllRentalRequestsAsync(string? status)
        {
            return await _requestRepository.GetAllRentalRequestsAsync(status);
        }

        public async Task<RentalRequest> GetRentalRequestByIdAsync(int id)
        {
            return await _requestRepository.GetRentalRequestByIdAsync(id);
        }
    }
}


