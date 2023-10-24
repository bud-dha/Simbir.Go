using Simbir.Go.DAL.Models.Common;

namespace Simbir.Go.BLL.DTO
{
    public class AdminRentDTO
    {
        public long TransportId { get; set; }

        public long UserId { get; set; }

        public string TimeStart { get; set; } = null!;

        public string? TimeEnd { get; set; }

        public double PriceOfUnit { get; set; }

        public PriceTypes PriceType { get; set; }

        public double? FinalPrice { get; set; }
    }
}