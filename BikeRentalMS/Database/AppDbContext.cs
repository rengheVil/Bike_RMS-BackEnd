using BikeRentalMS.Models;
using Microsoft.EntityFrameworkCore;

namespace BikeRentalMS.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Motorbike> Motorbikes { get; set; }
        public DbSet<LoginRequest> LoginRequests { get; set; }
        public DbSet<OrderHistory> OrderHistorys { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<RentalRequest> RentalRequests { get; set; }
        public DbSet<UpdateStatusRequest> UpdateStatusRequests { get; set; }
        public DbSet<User> Users { get; set; }




        }


}
