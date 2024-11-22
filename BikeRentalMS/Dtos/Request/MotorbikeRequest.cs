namespace BikeRentalMS.Dtos.Request
{
    public class MotorbikeRequest
    {
        public string RegNumber { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Category { get; set; }
        public IFormFile ImageData { get; set; }

    }
}
