namespace BikeRentalMS.Dtos.Request
{
    public class RentalRequestDTO
    {
        public DateTime RequestDate { get; set; }
        public int UserId { get; set; }
        public int MotorbikeId { get; set; }
    }
}
