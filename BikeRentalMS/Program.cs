
using BikeRentalMS.Database;
using BikeRentalMS.Repositories;
using BikeRentalMS.Services;
using Microsoft.EntityFrameworkCore;
using System;

namespace BikeRentalMS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


          

            // Enable CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    policy =>
                    {
                        policy.AllowAnyOrigin()
                              .AllowAnyMethod()
                              .AllowAnyHeader();
                    });
            });

            // Get connection string
            builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefoultConnection")));

            //  Repositories
            builder.Services.AddScoped<UserRepository>();
            builder.Services.AddScoped<MotorbikeRepository>();
            builder.Services.AddScoped<RentalRepository>();
            builder.Services.AddScoped<RentalRequestRepository>();
            builder.Services.AddScoped<OrderHistoryRepository>();

            //  Services
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<MotorbikeService>();
            builder.Services.AddScoped<RentalService>();
            builder.Services.AddScoped<RentalRequestService>();
            builder.Services.AddScoped<OrderHistoryService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors("AllowAllOrigins");

            app.MapControllers();

            app.Run();
        }
    }
}
