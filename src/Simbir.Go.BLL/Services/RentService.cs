using Simbir.Go.DAL.Models;
using Simbir.Go.DAL.Repositories;

namespace Simbir.Go.BLL.Services
{
    public class RentService
    {
        private RentRepository _rentRepository;

        private TransportRepository _transportRepository;


        public RentService(RentRepository rentRepository, TransportRepository transportRepository)
        {
            _rentRepository = rentRepository;
            _transportRepository = transportRepository;
        }


        public async Task<List<Transport>> Transports(double latitude, double longitude, double radius, string type)
        {
            var transports = await _transportRepository.GetAll() ?? throw new ArgumentException("Transports wasn`t found in the database");

            if (type != "All")
                transports = transports.Where(t => t.TransportType == type);

            return ReturnTransport(latitude, longitude, radius, transports).ToList();
        }

        public async Task<Rent> RentById(long id)
        {
            var rent = await _rentRepository.GetByIdAsync(id);
            return rent ?? throw new ArgumentException("Rent wasn`t found in the database");
        }

        public async Task<List<Rent>> RentHistory(long id)
        {
            var rent = await _rentRepository.GetAll();
            return rent.Where(r => r.UserId == id).ToList() ?? throw new ArgumentException("Rents history wasn`t found in the database");
        }

        public async Task<List<Rent>> TransportHistory(long id)
        {
            var rent = await _rentRepository.GetAll();
            return rent.Where(r => r.TransportId == id).ToList() ?? throw new ArgumentException("Transport history wasn`t found in the database");
        }

        public void NewRent(long id, string transportType)
        {
            var newRent = new Rent(); // получить id пользователя.
            _rentRepository.Create(newRent);
        }

        public async void EndRent(long id, double latitude, double longitude)
        {
            var rent = await _rentRepository.GetByIdAsync(id);
            var transport = await _transportRepository.GetByIdAsync(rent.TransportId);

            transport.Latitude = latitude;
            transport.Longitude = longitude;
        }


        private static List<Transport> ReturnTransport(double latitudeCenter, double longitudeCenter, double radius, IEnumerable<Transport> transports)
        {
            List<Transport> available = new();
            foreach (var transport in transports)
            {
                if (DistanceBetweenPoints(latitudeCenter, longitudeCenter, transport.Latitude, transport.Longitude) <= radius)
                    available.Add(transport);
            }
            return available;
        }

        static double DistanceBetweenPoints(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2)); ;
        }
    }
}