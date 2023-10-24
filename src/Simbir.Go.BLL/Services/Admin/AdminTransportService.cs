using Simbir.Go.BLL.DTO;
using Simbir.Go.DAL.Models;
using Simbir.Go.DAL.Models.Common;
using Simbir.Go.DAL.Repositories;

namespace Simbir.Go.BLL.Services.Admin
{
    public class AdminTransportService
    {
        private TransportRepository _transportRepository;


        public AdminTransportService(TransportRepository transportRepository)
        {
            _transportRepository = transportRepository;
        }


        public async Task<List<Transport>> GetAllTransports(int count, int start = 1, TransportTypes type = TransportTypes.All)
        {
            var transports = await _transportRepository.GetAll() ?? throw new ArgumentException("Transports wasn`t found in the database");

            if (count == 0)
                return transports.Skip(start - 1).Where(t => t.Type == type).ToList();

            return transports.Skip(start - 1).Take(count).Where(t => t.Type == type).ToList();
        }

        public async Task<Transport> GetTransportById(long id)
        {
            var transport = await _transportRepository.GetByIdAsync(id);
            return transport ?? throw new ArgumentException("Transport wasn`t found in the database");
        }

        public void CreateTransport(AdminTransportDTO dto)
        {
            var newTransport = new Transport(dto.OwnerId, dto.CanBeRented, dto.Type, dto.Model, dto.Color, dto.Identefier, dto.Description, dto.Latitude, dto.Longitude, dto.MinutePrice, dto.DayPrice);
            _transportRepository.Create(newTransport);
        }

        public void UpdateTransport(long id, AdminTransportDTO dto)
        {
            var transport = _transportRepository.GetById(id) ?? throw new ArgumentException("Transport wasn`t found in the database");
            ReplaceTransportData(transport, dto);
            _transportRepository.Update(transport);
        }

        public void DeleteTransport(long id)
        {
            var transport = _transportRepository.GetById(id) ?? throw new ArgumentException("Transport wasn`t found in the database");
            _transportRepository.Delete(transport);
        }


        private static void ReplaceTransportData(Transport transport, AdminTransportDTO dto)
        {
            transport.OwnerId = dto.OwnerId;
            transport.CanBeRented = dto.CanBeRented;
            transport.Model = dto.Model ?? transport.Model;
            transport.Color = dto.Color ?? transport.Color;
            transport.Identifier = dto.Identefier ?? transport.Identifier;
            transport.Description = dto.Description;
            transport.Latitude = dto.Latitude;
            transport.Longitude = dto.Longitude;
            transport.MinutePrice = dto.MinutePrice;
            transport.DayPrice = dto.DayPrice;
        }
    }
}