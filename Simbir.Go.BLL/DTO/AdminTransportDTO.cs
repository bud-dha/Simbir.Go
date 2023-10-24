using Simbir.Go.DAL.Models.Common;

namespace Simbir.Go.BLL.DTO
{
    public class AdminTransportDTO
    {
        public long OwnerId { get; set; }

        public bool CanBeRented { get; set; }

        public TransportTypes Type { get; set; }

        public string Model { get; set; } = null!;

        public string Color { get; set; } = null!;

        public string Identefier { get; set; } = null!;

        public string? Description { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public double? MinutePrice { get; set; }

        public double? DayPrice { get; set; }
    }
}