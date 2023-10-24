using Simbir.Go.BLL.DTO;
using Simbir.Go.DAL.Models;
using Simbir.Go.DAL.Repositories;

namespace Simbir.Go.BLL.Services.Admin
{
    public class AdminRentService
    {
        private RentRepository _rentRepository;

        private TransportRepository _transportRepository;


        public AdminRentService(RentRepository rentRepository, TransportRepository transportRepository)
        {
            _rentRepository = rentRepository;
            _transportRepository = transportRepository;
        }


        public async Task<Rent> GetRentById(long id)
        {
            return await _rentRepository.GetByIdAsync(id) ?? throw new ArgumentException("Rent wasn`t found in the database");
        }

        public async Task<List<Rent>> GetUserHistory(long id)
        {
            var rent = await _rentRepository.GetAll();
            return rent.Where(r => r.UserId == id).ToList() ?? throw new ArgumentException("This user history wasn`t found in the database");
        }

        public async Task<List<Rent>> GetTransportHistory(long id)
        {
            var rent = await _rentRepository.GetAll();
            return rent.Where(r => r.TransportId == id).ToList() ?? throw new ArgumentException("This transport history wasn`t found in the database");
        }

        public void CreateNewRent(AdminRentDTO dto)
        {
            
        }

        public void EndRent(double latitude, double longitude)
        {

        }

        public void UpdateRent(long id, AdminRentDTO dto)
        {

        }




        private void ReturnTransport(double latitude, double longitude, double radius)
        {

        }

        private static void ReplaceRentData(Rent rent, AdminRentDTO dto)
        {
            rent.TransportId = dto.TransportId;
            rent.UserId = dto.UserId;
            rent.TimeStart = dto.TimeStart ?? rent.TimeStart;
            rent.TimeEnd = dto.TimeEnd;
            rent.PriceOfUnit = dto.PriceOfUnit;
            rent.Type = dto.PriceType;
            rent.FinalPrice = dto.FinalPrice;
        }
    }
}