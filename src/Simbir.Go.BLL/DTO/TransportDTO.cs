namespace Simbir.Go.BLL.DTO
{
    public class TransportDTO
    {
        public bool CanBeRented { get; set; }
        public string TransportType { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string Identefier { get; set; }
        public string? Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double? MinutePrice { get; set; }
        public double? DayPrice { get; set; }
    }
}