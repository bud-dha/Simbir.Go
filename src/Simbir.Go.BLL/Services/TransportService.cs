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


        public async Task<Transport> TransportById(long id)
        {
            var transport = await _transportRepository.GetByIdAsync(id);
            return transport ?? throw new ArgumentException("Transport wasn`t found in the database");
        }

        public void CreateTransport(string username, TransportDTO dto)
        {
            var user = _accountRepository.Find(username);
            var newTransport = new Transport(user.AccountId, dto.CanBeRented, dto.TransportType, dto.Model, dto.Color, dto.Identefier, dto.Description, dto.Latitude, dto.Longitude, dto.MinutePrice, dto.DayPrice);
            _transportRepository.Create(newTransport);
        }

        public void UpdateTransport(long id, TransportDTO dto, string username)
        {
            var user = _accountRepository.Find(username);
            var transport = _transportRepository.GetById(id) ?? throw new ArgumentException("Transport wasn`t found in the database");

            if (user.AccountId != transport.OwnerId)
                throw new ArgumentException("Transport information can be changed only by the owner");

            ReplaceTransportData(transport, dto);
            _transportRepository.Update(transport);
        }

        public void DeleteTransport(long id, string username)
        {
            var user = _accountRepository.Find(username);
            var transport = _transportRepository.GetById(id) ?? throw new ArgumentException("Transport wasn`t found in the database");

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