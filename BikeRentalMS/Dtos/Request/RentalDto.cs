namespace BikeRentalMS.Dtos.Request
{
    public class RentalDto
    {
        public int id { get; set; }
        public int UserId { get; set; }
        public MotorbikeDto Motorbike { get; set; }
        public DateTime RentDate { get; set; }
        public bool Status { get; set; } // True = Active, False = Overdue


    }
}
