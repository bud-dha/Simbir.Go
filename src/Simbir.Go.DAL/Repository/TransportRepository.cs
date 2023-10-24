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
            _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Transport>> GetAll()
        {
            var obj = await _dbContext.Transports.ToListAsync();
            if (obj != null)
                return obj;
            else
                return null;
        }

        public async Task<Transport> GetByIdAsync(long id)
        {
            return await _dbContext.Transports.FirstOrDefaultAsync(x => x.TransportId == id);
        }

        public void Update(Transport transport)
        {
            if (transport != null)
            {
                var obj = _dbContext.Update(transport);
                if (obj != null)
                    _dbContext.SaveChanges();
            }
        }

        public Transport GetById(long id)
        {
            return _dbContext.Transports.FirstOrDefault(x => x.TransportId == id);
        }

        public void Delete(Transport transport)
        {
            if (transport != null)
            {
                var obj = _dbContext.Remove(transport);
                if (obj != null)
                    _dbContext.SaveChangesAsync();
            }
        }
    }
}