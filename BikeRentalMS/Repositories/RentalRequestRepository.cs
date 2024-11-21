using BikeRentalMS.Database;
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
            public async Task<bool> AddRentalRequestAsync(RentalRequest request)
            {
                await _context.RentalRequests.AddAsync(request);
                int result = await _context.SaveChangesAsync();
                return result > 0;
            }

            // Update the status of a rental request
            public async Task<bool> UpdateRentalRequestStatusAsync(int rentalRequestId, string status, DateTime? approvalDate = null)
            {
                var request = await _context.RentalRequests.FindAsync(rentalRequestId);
                if (request == null) return false;

                request.Status = status;
                request.ApprovalDate = (DateTime)approvalDate;

                int result = await _context.SaveChangesAsync();
                return result > 0;
            }

            // Retrieve all rental requests
            public async Task<List<RentalRequest>> GetAllRentalRequestsAsync()
            {
                return await _context.RentalRequests.ToListAsync();
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

            // Get rental request by ID
            public async Task<RentalRequest> GetRentalRequestByIdAsync(int id)
            {
                return await _context.RentalRequests.FirstOrDefaultAsync(r => r.Id == id);
            }
        }
    }


