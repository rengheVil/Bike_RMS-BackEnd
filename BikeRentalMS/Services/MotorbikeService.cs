using BikeRentalMS.Models;
using BikeRentalMS.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace BikeRentalMS.Services
{
    public class MotorbikeService
    {

            private readonly MotorbikeRepository _motorbikeRepository;

            public MotorbikeService(MotorbikeRepository motorbikeRepository)
            {
                _motorbikeRepository = motorbikeRepository;
            }

            public async Task<bool> AddMotorbikeAsync(Motorbike motorbike)
            {
                return await _motorbikeRepository.AddMotorbikeAsync(motorbike);
            }

            public async Task<Motorbike> GetMotorbikeByIdAsync(int motorbikeId)
            {
                return await _motorbikeRepository.GetMotorbikeByIdAsync(motorbikeId);
            }

            public async Task<bool> UpdateMotorbikeAsync(Motorbike motorbike)
            {
                return await _motorbikeRepository.UpdateMotorbikeAsync(motorbike);
            }

            public async Task<bool> DeleteMotorbikeAsync(int motorbikeId)
            {
                return await _motorbikeRepository.DeleteMotorbikeAsync(motorbikeId);
            }

            public async Task<List<Motorbike>> GetAllMotorbikesAsync()
            {
                return await _motorbikeRepository.GetAllMotorbikesAsync();
            }
        }
    }


