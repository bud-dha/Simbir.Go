using Simbir.Go.BLL.DTO;
using Simbir.Go.DAL.Repositories;
using Simbir.Go.DAL.Models;

namespace Simbir.Go.BLL.Services
{
    public class TransportService
    {
        private AccountRepository _accountRepository;
        private TransportRepository _transportRepository;


        public TransportService(AccountRepository accountRepository, TransportRepository transportRepository)
        {
            _accountRepository = accountRepository;
            _transportRepository = transportRepository;
        }


        public async Task<Transport> TransportById(long transportId)
        {
            var transport = await _transportRepository.GetByIdAsync(transportId);
            return transport ?? throw new ArgumentException("Transport wasn`t found in the database");
        }

        public async Task CreateTransport(string username, TransportDTO dto)
        {
            var user = await _accountRepository.FindAsync(username);

            var newTransport = new Transport
            {
                OwnerId = user.AccountId,
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

        public async Task UpdateTransport(long transportId, TransportDTO dto, string username)
        {
            var user = await _accountRepository.FindAsync(username);
            var transport = await _transportRepository.GetByIdAsync(transportId) ?? throw new ArgumentException("Transport wasn`t found in the database");

            if (user.AccountId != transport.OwnerId)
                throw new ArgumentException("Transport information can be changed only by the owner");

            ReplaceTransportData(transport, dto);
            _transportRepository.Update(transport);
        }

        public async Task DeleteTransport(long transportId, string username)
        {
            var user = await _accountRepository.FindAsync(username);
            var transport = await _transportRepository.GetByIdAsync(transportId) ?? throw new ArgumentException("Transport wasn`t found in the database");

            if (user.AccountId != transport.OwnerId)
                throw new ArgumentException("Transport can be deleted only by the owner");

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