using System.ComponentModel.DataAnnotations.Schema;

namespace BikeRentalMS.Models
{
    public class OrderHistory
    {
        public int OrderId { get; set; }
        [ForeignKey("Motorbikes")]
        public int MotorbikeId { get; set; }
        [ForeignKey("Users")]
        public int UserId { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
