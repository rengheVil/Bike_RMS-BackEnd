using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BikeRentalMS.Models
{
    public class Rental
    {
        [Key]
        public int Id { get; set; }
       
        public Motorbike? Motorbike {  get; set; } 
        public int MotorbikeId { get; set; }
         
        public User? User { get; set; }
        public int UserId { get; set; }
        public DateTime RentDate { get; set; }
       // public object ReturnDate { get; internal set; }
    }
}
