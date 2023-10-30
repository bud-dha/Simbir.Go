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


        public async Task<Rent> RentById(long rentId)
        {
            var rent = await _rentRepository.GetByIdAsync(rentId);
            return rent ?? throw new ArgumentException("Rent wasn`t found in the database");
        }

        public async Task<List<Rent>> UserHistory(long userId)
        {
            var rent = await _rentRepository.GetAll();
            return rent.Where(r => r.UserId == userId).ToList() ?? throw new ArgumentException("This user history wasn`t found in the database");
        }

        public async Task<List<Rent>> TransportHistory(long transportId)
        {
            var rent = await _rentRepository.GetAll();
            return rent.Where(r => r.TransportId == transportId).ToList() ?? throw new ArgumentException("This transport history wasn`t found in the database");
        }

        public async Task CreateRent(AdminRentDTO dto)
        {
            var transport = await _transportRepository.GetByIdAsync(dto.TransportId);
            if (!transport.CanBeRented)
                throw new ArgumentException("Еhis transport can`t be rented");

            var newRent = new Rent
            {
                TransportId = dto.TransportId,
                UserId = dto.UserId,
                TimeStart = dto.TimeStart,
                TimeEnd = dto.TimeEnd,
                PriceOfUnit = dto.PriceOfUnit,
                RentType = dto.RentType,
                FinalPrice = dto.FinalPrice
            };
            _rentRepository.Create(newRent);

            transport.CanBeRented = false;
            _transportRepository.Update(transport);
        }

        public async Task EndRent(long rentId, double latitude, double longitude)
        {
            var rents = await _rentRepository.GetAll();
            var rent = rents.FirstOrDefault(r => r.RentId == rentId) ?? throw new ArgumentException("Rent wasn`t found in the database");
            var transport = await _transportRepository.GetByIdAsync(rent.TransportId);

            rent.TimeEnd = DateTime.UtcNow.ToString();
            _rentRepository.Update(rent);

            transport.Latitude = latitude;
            transport.Longitude = longitude;
            transport.CanBeRented = true;
            _transportRepository.Update(transport);
        }

        public async Task UpdateRent(long id, AdminRentDTO dto)
        {
            var rent = await _rentRepository.GetByIdAsync(id) ?? throw new ArgumentException("Rent wasn`t found in the database");

            ReplaceRentData(rent, dto);
            _rentRepository.Update(rent);
        }

        public async Task DeleteRent(long id)
        {
            var rent = await _rentRepository.GetByIdAsync(id) ?? throw new ArgumentException("Rent wasn`t found in the database");
            _rentRepository.Delete(rent);
        }


        private static void ReplaceRentData(Rent rent, AdminRentDTO dto)
        {
            rent.TransportId = dto.TransportId;
            rent.UserId = dto.UserId;
            rent.TimeStart = dto.TimeStart ?? rent.TimeStart;
            rent.TimeEnd = dto.TimeEnd;
            rent.PriceOfUnit = dto.PriceOfUnit;
            rent.RentType = dto.RentType;
            rent.FinalPrice = dto.FinalPrice;
        }
    }
}