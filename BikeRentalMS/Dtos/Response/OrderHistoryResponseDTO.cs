namespace BikeRentalMS.Dtos.Response
{
    public class OrderHistoryResponseDTO
    {
        public int Id { get; set; }
        public int MotorbikeId { get; set; }
        public int UserId { get; set; }
        public required string Brand { get; set; }
        public required string Modal { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
