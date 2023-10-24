using Simbir.Go.BLL.DTO;
using Simbir.Go.DAL.Repositories;
using Simbir.Go.DAL.Models;

namespace Simbir.Go.BLL.Services
{
    public class TransportService
    {
        private TransportRepository _transportRepository;


        public TransportService(TransportRepository transportRepository)
        {
            _transportRepository = transportRepository;
        }


        public async Task<Transport> GetTransportById(long id)
        {
            var transport = await _transportRepository.GetByIdAsync(id);
            return transport ?? throw new ArgumentException("Transport wasn`t found in the database");
        }

        public void CreateTransport(TransportDTO dto)
        {
            var newTransport = new Transport(dto.CanBeRented, dto.Type, dto.Model, dto.Color, dto.Identefier, dto.Description, dto.Latitude, dto.Longitude, dto.MinutePrice, dto.DayPrice);
            _transportRepository.Create(newTransport);
        }

        public void UpdateTransport(long id, TransportDTO dto)
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


        private static void ReplaceTransportData(Transport transport, TransportDTO dto)
        {
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