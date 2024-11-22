using BikeRentalMS.Dtos.Request;
using BikeRentalMS.Models;
using BikeRentalMS.Repositories;
using System.Collections.Generic;
using System.Security.Cryptography;
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

            public async Task<bool> AddMotorbikeAsync(MotorbikeRequest motorbikeRequest)
            {
            var motorbike = new Motorbike
            {
                RegNumber = motorbikeRequest.RegNumber,
                Brand = motorbikeRequest.Brand,
                Model = motorbikeRequest.Model,
                Category = motorbikeRequest.Category,
                         
               };

            if (motorbikeRequest.ImageData != null)
            {
                using (var memoryStream = new MemoryStream()) 
                {
                    await motorbikeRequest.ImageData.CopyToAsync(memoryStream);
                    motorbike.ImageData = memoryStream.ToArray();


                }
            }

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


