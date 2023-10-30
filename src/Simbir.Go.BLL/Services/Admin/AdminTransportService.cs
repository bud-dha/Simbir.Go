using Simbir.Go.BLL.DTO;
using Simbir.Go.DAL.Models;
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


        public async Task<List<Transport>> GetAllTransports(int count, int start = 1, string type = "All")
        {
            var transports = await _transportRepository.GetAll() ?? throw new ArgumentException("Transports wasn`t found in the database");

            transports = transports.Skip(start - 1);

            if (type != "All")
                transports = transports.Where(t => t.TransportType == type);

            if (count != 0)
                transports = transports.Take(count);

            return transports.ToList();
        }

        public async Task<Transport> GetTransportById(long id)
        {
            var transport = await _transportRepository.GetByIdAsync(id);
            return transport ?? throw new ArgumentException("Transport wasn`t found in the database");
        }

        public async Task CreateTransport(AdminTransportDTO dto)
        {
            var newTransport = new Transport
            {
                OwnerId = dto.OwnerId,
                CanBeRented = dto.CanBeRented,
                TransportType = dto.TransportType,
                Model = dto.Model,
                Color = dto.Color,
                Identifier = dto.Identefier,
                Description = dto.Description,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                MinutePrice = dto.MinutePrice,
                DayPrice = dto.DayPrice
            };
            _transportRepository.Create(newTransport);
        }

        public async Task UpdateTransport(long id, AdminTransportDTO dto)
        {
            var transport = await _transportRepository.GetByIdAsync(id) ?? throw new ArgumentException("Transport wasn`t found in the database");
            ReplaceTransportData(transport, dto);
            _transportRepository.Update(transport);
        }

        public async Task DeleteTransport(long id)
        {
            var transport = await _transportRepository.GetByIdAsync(id) ?? throw new ArgumentException("Transport wasn`t found in the database");
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