using Simbir.Go.DAL.Models;
using Simbir.Go.DAL.Repositories;

namespace Simbir.Go.BLL.Services
{
    public class RentService
    {
        private AccountRepository _accountRepository;
        private RentRepository _rentRepository;
        private TransportRepository _transportRepository;


        public RentService(AccountRepository accountRepository, RentRepository rentRepository, TransportRepository transportRepository)
        {
            _accountRepository = accountRepository;
            _rentRepository = rentRepository;
            _transportRepository = transportRepository;
        }


        public async Task<List<Transport>> Transports(double latitude, double longitude, double radius, string type)
        {
            var transports = await _transportRepository.GetAll() ?? throw new ArgumentException("Transports wasn`t found in the database.");

            if (type != "All")
                transports = transports.Where(t => t.TransportType == type);

            return ReturnTransportInMyArea(latitude, longitude, radius, transports).ToList();
        }

        public async Task<Rent> RentById(long rentId, string username)
        {
            var rent = await _rentRepository.GetByIdAsync(rentId);
            var user = await _accountRepository.FindAsync(username);
            var transport = await _transportRepository.GetByIdAsync(rent.TransportId);

            if (user.AccountId != rent.UserId && user.AccountId != transport.OwnerId)
                throw new ArgumentException("This information available only to the transport owner and the tenant.");

            return rent ?? throw new ArgumentException("Rent wasn`t found in the database.");
        }

        public async Task<List<Rent>> MyHistory(string username)
        {
            var user = await _accountRepository.FindAsync(username);
            var rents = await _rentRepository.GetAll();

            return rents.Where(r => r.UserId == user.AccountId).ToList() ?? throw new ArgumentException("Rents history wasn`t found in the database.");
        }

        public async Task<List<Rent>> TransportHistory(long transportId, string username)
        {
            var user = await _accountRepository.FindAsync(username);
            var transport = await _transportRepository.GetByIdAsync(transportId);

            if (user.AccountId != transport.OwnerId)
                throw new ArgumentException("Transport rent hirtory available only to the owner.");

            var rents = await _rentRepository.GetAll();
            return rents.Where(r => r.TransportId == transportId).ToList() ?? throw new ArgumentException("Transport history wasn`t found in the database.");
        }

        public async Task NewRent(long transportId, string rentType, string username)
        {
            var user = await _accountRepository.FindAsync(username);
            var transport = await _transportRepository.GetByIdAsync(transportId) ?? throw new ArgumentException("Transport wasn`t found in the database.");

            if (user.AccountId == transport.OwnerId)
                throw new ArgumentException("You can`t rent your own transport.");

            var newRent = new Rent
            {
                TransportId = transportId,
                UserId = user.AccountId,
                TimeStart = DateTime.UtcNow.ToString(),
                RentType = rentType,
                PriceOfUnit = transport.MinutePrice
            };
            _rentRepository.Create(newRent);

            transport.CanBeRented = false;
            _transportRepository.Update(transport);
        }

        public async Task EndRent(long transportId, double latitude, double longitude, string username)
        {
            var rents = await _rentRepository.GetAll();
            var rent = rents.Where(r => r.TransportId == transportId).FirstOrDefault(r => r.TimeEnd == null);
            var user = await _accountRepository.FindAsync(username);
            var transport = await _transportRepository.GetByIdAsync(transportId) ?? throw new ArgumentException("Transport wasn`t found in the database.");

            rent.TimeEnd = DateTime.UtcNow.ToString();
            _rentRepository.Update(rent);

            transport.Latitude = latitude;
            transport.Longitude = longitude;
            transport.CanBeRented = true;
            _transportRepository.Update(transport);
        }


        private static List<Transport> ReturnTransportInMyArea(double latitudeCenter, double longitudeCenter, double radius, IEnumerable<Transport> transports)
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