using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BikeRentalMS.Models
{
    public class RentalRequest
    {
        [Key]
        public int Id { get; set; }
       public Motorbike Motorbike { get; set; }
        public int MotorbikeId { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public DateTime RequestDate { get; set; }
        public string Status { get; set; }
        public DateTime ApprovalDate { get; set; }
        //public object Id { get; internal set; }
    }
}
