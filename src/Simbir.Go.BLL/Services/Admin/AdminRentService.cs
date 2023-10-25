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


        public async Task<Rent> RentById(long id)
        {
            var rent = await _rentRepository.GetByIdAsync(id);
            return rent ?? throw new ArgumentException("Rent wasn`t found in the database");
        }

        public async Task<List<Rent>> UserHistory(long id)
        {
            var rent = await _rentRepository.GetAll();
            return rent.Where(r => r.UserId == id).ToList() ?? throw new ArgumentException("This user history wasn`t found in the database");
        }

        public async Task<List<Rent>> TransportHistory(long id)
        {
            var rent = await _rentRepository.GetAll();
            return rent.Where(r => r.TransportId == id).ToList() ?? throw new ArgumentException("This transport history wasn`t found in the database");
        }

        public void CreateRent(AdminRentDTO dto)
        {
            var rent = new Rent(dto.TransportId, dto.UserId, dto.TimeStart, dto.TimeEnd, dto.PriceOfUnit, dto.PriceType, dto.FinalPrice);
            _rentRepository.Create(rent);
        }

        public async void EndRent(long id, double latitude, double longitude)
        {
            var rent = await _rentRepository.GetByIdAsync(id);
            var transport = await _transportRepository.GetByIdAsync(rent.TransportId);

            transport.Latitude = latitude;
            transport.Longitude = longitude;
        }

        public void UpdateRent(long id, AdminRentDTO dto)
        {
            var rent = _rentRepository.GetById(id) ?? throw new ArgumentException("Rent wasn`t found in the database");

            ReplaceRentData(rent, dto);
            _rentRepository.Update(rent);
        }

        public void DeleteRent(long id)
        {
            var rent = _rentRepository.GetById(id) ?? throw new ArgumentException("Rent wasn`t found in the database");
            _rentRepository.Delete(rent);
        }


        private static void ReplaceRentData(Rent rent, AdminRentDTO dto)
        {
            rent.TransportId = dto.TransportId;
            rent.UserId = dto.UserId;
            rent.TimeStart = dto.TimeStart ?? rent.TimeStart;
            rent.TimeEnd = dto.TimeEnd;
            rent.PriceOfUnit = dto.PriceOfUnit;
            rent.PriceType = dto.PriceType;
            rent.FinalPrice = dto.FinalPrice;
        }
    }
}