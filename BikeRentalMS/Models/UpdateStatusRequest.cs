using System.ComponentModel.DataAnnotations;

namespace BikeRentalMS.Models
{
    public class UpdateStatusRequest
    {
        [Key]
        public int Id {  get; set; }
        public string Status { get; set; }
        public DateTime? ApprovalDate { get; set; }

    }

}
