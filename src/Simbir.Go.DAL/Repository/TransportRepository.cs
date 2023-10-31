using Microsoft.EntityFrameworkCore;
using Simbir.Go.DAL.Contracts;
using Simbir.Go.DAL.Data;
using Simbir.Go.DAL.Models;

namespace Simbir.Go.DAL.Repositories
{
    public class TransportRepository : IRepository<Transport>
    {
        private readonly ApplicationContext _dbContext = null!;


        public TransportRepository(ApplicationContext context)
        {
            _dbContext = context;
        }


        public void Create(Transport transport)
        {
            _dbContext.Transports.Add(transport);
            _dbContext.SaveChanges();
        }

        public async Task<IEnumerable<Transport>> GetAll()
        {
            return await _dbContext.Transports.ToListAsync();
        }

        public async Task<Transport> GetByIdAsync(long id)
        {
            return await _dbContext.Transports.FindAsync(id);
        }

        public Transport GetById(long id)
        {
            return _dbContext.Transports.FirstOrDefault(x => x.TransportId == id);
        }

        public void Update(Transport transport)
        {
            _dbContext.Transports.Update(transport);
            _dbContext.SaveChanges();
        }

        public void Delete(Transport transport)
        {
            _dbContext.Transports.Remove(transport);
            _dbContext.SaveChanges();
        }
    }
}