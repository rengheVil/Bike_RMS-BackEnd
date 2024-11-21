using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace BikeRentalMS.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string NIC { get; set; }
        public string Role { get; set; }
        //public object Id { get; internal set; }
    }
}
