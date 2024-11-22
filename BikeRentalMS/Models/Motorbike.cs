using System.ComponentModel.DataAnnotations;

namespace BikeRentalMS.Models
{
    public class Motorbike
    {
        [Key]
        public int Id { get; set; }
        public string RegNumber { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Category { get; set; }
        public byte[]? ImageData { get; set; }
    }
}
