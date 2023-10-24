using Simbir.Go.DAL.Models;
using Simbir.Go.DAL.Models.Common;
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


        public async Task<List<Transport>> GetAllTransports(double latitude, double longitude, double radius, TransportTypes type)
        {
            var transports = await _transportRepository.GetAll() ?? throw new ArgumentException("Transports wasn`t found in the database");

            /// Добавить логику
            ReturnTransport(latitude, longitude, radius);

            return transports.ToList();
        }

        public async Task<List<Rent>> GetRentsHistory(long id)
        {
            var rent = await _rentRepository.GetAll();
            return rent.Where(r => r.UserId == id).ToList() ?? throw new ArgumentException("History wasn`t found in the database");
        }

        public async Task<List<Rent>> GetTransportHistory(long id)
        {
            var rent = await _rentRepository.GetAll();
            return rent.Where(r => r.TransportId == id).ToList() ?? throw new ArgumentException("History wasn`t found in the database");
        }

        public void NewRent(long id)
        {

        }

        public void EndRent(long id)
        {

        }


        private void ReturnTransport(double latitude, double longitude, double radius)
        {

        }
    }
}