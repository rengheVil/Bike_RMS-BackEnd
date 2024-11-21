using BikeRentalMS.Models;
using BikeRentalMS.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace BikeRentalMS.Services
{
    public class RentalRequestService
    {

            private readonly RentalRequestRepository _requestRepository;
            private readonly RentalRepository _rentalRepository;

            public RentalRequestService(RentalRequestRepository rentalRequestRepository, RentalRepository rentalRepository)
            {
                _requestRepository = rentalRequestRepository;
                _rentalRepository = rentalRepository;
            }

            public async Task<bool> AddRentalRequestAsync(RentalRequest request)
            {
                return await _requestRepository.AddRentalRequestAsync(request);
            }

            public async Task<bool> ApproveRentalRequestAsync(int requestId)
            {
                var rentalRequest = await _requestRepository.GetRentalRequestByIdAsync(requestId);
                if (rentalRequest == null || rentalRequest.Status != "pending") return false;

                await _requestRepository.ApproveRentalRequestAsync(requestId, DateTime.UtcNow);

                var rental = new Rental
                {
                    MotorbikeId = rentalRequest.MotorbikeId,
                    UserId = rentalRequest.UserId,
                    RentDate = DateTime.UtcNow
                };

                return await _rentalRepository.AddRentalAsync(rental);
            }

            public async Task<bool> UpdateRentalRequestStatusAsync(int requestId, string status, DateTime? approvalDate = null)
            {
                return await _requestRepository.UpdateRentalRequestStatusAsync(requestId, status, approvalDate);
            }

            public async Task<List<RentalRequest>> GetAllRentalRequestsAsync()
            {
                return await _requestRepository.GetAllRentalRequestsAsync();
            }

            public async Task<RentalRequest> GetRentalRequestByIdAsync(int id)
            {
                return await _requestRepository.GetRentalRequestByIdAsync(id);
            }
        }
    }


