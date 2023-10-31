namespace Simbir.Go.BLL.DTO
{
    public class AdminRentDTO
    {
        public long TransportId { get; set; }
        public long UserId { get; set; }
        public string TimeStart { get; set; }
        public string? TimeEnd { get; set; }
        public double PriceOfUnit { get; set; }
        public string RentType { get; set; }
        public double? FinalPrice { get; set; }
    }
}