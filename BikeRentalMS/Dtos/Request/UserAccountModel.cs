using BikeRentalMS.Models;
using System.Data;

namespace BikeRentalMS.Dtos.Request
{
    public class UserAccountModel
    {
        public int? Id { get; set; }
        public string UserName { get; set; }
        public string NIC { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }


    }
}
