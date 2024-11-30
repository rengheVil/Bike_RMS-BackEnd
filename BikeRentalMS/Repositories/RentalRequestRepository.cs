using BikeRentalMS.Database;
using BikeRentalMS.Dtos.Request;
using BikeRentalMS.Models;
using Microsoft.EntityFrameworkCore;

namespace BikeRentalMS.Repositories
{
    public class RentalRequestRepository
    {
       
            private readonly AppDbContext _context;

            public RentalRequestRepository(AppDbContext context)
            {
                _context = context;
            }

            // Add a new rental request
            public async Task<RentalRequest> AddRentalRequestAsync(RentalRequest request)
            {
               var data = await _context.RentalRequests.AddAsync(request);
              await _context.SaveChangesAsync();
                return data.Entity;
            }

            // Update the status of a rental request
            public async Task<bool> UpdateRentalRequestStatusAsync(RentalRequest rentalRequest)
            {
                _context.RentalRequests.Update(rentalRequest);
                int result = await _context.SaveChangesAsync();
                return result > 0;
            }

            // Retrieve all rental requests
            public async Task<List<RentalRequest>> GetAllRentalRequestsAsync()
            {
                return await _context.RentalRequests.Include(b => b.Motorbike).ToListAsync();
            }

            // Approve a rental request
            public async Task<bool> ApproveRentalRequestAsync(int requestId, DateTime approvalDate)
            {
                var request = await _context.RentalRequests.FindAsync(requestId);
                if (request == null) return false;

                request.Status = "approved";
                request.ApprovalDate = approvalDate;

                int result = await _context.SaveChangesAsync();
                return result > 0;
            }


            public async Task<List<RentalRequest>> GetApprovedRequestsByUserIdAsync(int userId)
            {
                return await _context.RentalRequests
                    .Where(r => r.UserId == userId && r.Status.ToLower() == "approved").Include(r => r.Motorbike)
                    .ToListAsync();
            }


        // Get rental request by ID
        public async Task<RentalRequest> GetRentalRequestByIdAsync(int id)
            {
                return await _context.RentalRequests.FirstOrDefaultAsync(r => r.Id == id);
            }

       



    }
    }


