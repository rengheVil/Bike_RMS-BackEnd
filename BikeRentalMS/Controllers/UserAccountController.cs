using BikeRentalMS.Database;
using BikeRentalMS.Dtos.Request;
using BikeRentalMS.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BikeRentalMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {

        private  AppDbContext _appDbContext;
        private readonly IConfiguration _configuration;

            public UserAccountController(AppDbContext appDbContext, IConfiguration configuration)
            {
                _appDbContext = appDbContext;
                _configuration = configuration;
            }

     


        [HttpPost("Register-User")]

            public async Task<IActionResult> RegisterUser(UserAccountModel userAccount)
            {
                try
                {
                    var user = new User
                    {
                        UserName = userAccount.UserName,
                        NIC = userAccount.NIC,
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword(userAccount.Password),
                        Role = (string)userAccount.Role,
                        Email = userAccount.Email,
                        PhoneNumber = userAccount.PhoneNumber,
                    };

                    var data = await _appDbContext.Users.AddAsync(user);
                    await _appDbContext.SaveChangesAsync();

                    var token = CreateToken(user);
                    return Ok(token);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }

            [HttpPost("Log-In")]
            public async Task<IActionResult> LogIn(LogInData logInData)
            {
                try
                {
                    var user = _appDbContext.Users.SingleOrDefault(u => u.UserName == logInData.UserName) ?? throw new Exception("User Not Found");
                    var hash = BCrypt.Net.BCrypt.Verify(logInData.Password, user.PasswordHash);
                    if (hash)
                    {
                        var token = CreateToken(user);
                        return Ok(token);
                    }
                    else
                    {
                        throw new Exception("Invalid Password");
                    }

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message+"Api connected");
                }
            }


            private TokenModel CreateToken(User user)
            {
                var claimList = new List<Claim>();
                claimList.Add(new Claim("Id", user.Id.ToString()));
                claimList.Add(new Claim("UserName", user.UserName));
                claimList.Add(new Claim("NIC", user.NIC));
                claimList.Add(new Claim("Email", user.Email));
                claimList.Add(new Claim("PhoneNumber", user.PhoneNumber));
                claimList.Add(new Claim("Role", user.Role.ToString()));
                claimList.Add(new Claim(ClaimTypes.Role, user.Role.ToString()));

                var key = _configuration["JWT:Key"];
                var secKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
                var credentials = new SigningCredentials(secKey, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:Issuer"],
                    audience: _configuration["JWT:Audience"],
                    claims: claimList,
                    expires: DateTime.Now.AddMinutes(1),
                    signingCredentials: credentials
                    );
                var res = new TokenModel();
                res.Token = new JwtSecurityTokenHandler().WriteToken(token);
                return res;
            }
        

    }
}
