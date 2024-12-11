
using BikeRentalMS.Database;
using BikeRentalMS.Models;
using BikeRentalMS.Repositories;
using BikeRentalMS.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

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

            // Register EmailConfig
            builder.Services.Configure<EmailConfig>(builder.Configuration.GetSection("EmailConfig"));

            // Register services
            builder.Services.AddScoped<sendmailService>();
            builder.Services.AddScoped<SendMailRepository>();
            builder.Services.AddScoped<EmailServiceProvider>();


            // Add JWT Authentication
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]));

            builder.Services.AddAuthentication()
                .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = key,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                });

            
         

            // Add Authorization
            builder.Services.AddAuthorization();

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

            // Ensure EmailConfig is available as a singleton if needed
            builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<EmailConfig>>().Value);

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
