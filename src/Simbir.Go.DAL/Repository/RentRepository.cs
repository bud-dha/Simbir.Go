using Microsoft.EntityFrameworkCore;
using Simbir.Go.DAL.Contracts;
using Simbir.Go.DAL.Data;
using Simbir.Go.DAL.Models;

namespace Simbir.Go.DAL.Repositories
{
    public class RentRepository : IRepository<Rent>
    {
        private readonly ApplicationContext _dbContext = null!;


        public RentRepository(ApplicationContext context)
        {
            _dbContext = context;
        }


        public void Create(Rent rent)
        {
            _dbContext.Rents.Add(rent);
            _dbContext.SaveChanges();
        }

        public async Task<IEnumerable<Rent>> GetAll()
        {
            return await _dbContext.Rents.ToListAsync();
        }

        public async Task<Rent> GetByIdAsync(long id)
        {
            return await _dbContext.Rents.FindAsync(id);
        }

        public Rent GetById(long id)
        {
            return _dbContext.Rents.FirstOrDefault(x => x.RentId == id);
        }

        public void Update(Rent rent)
        {
            _dbContext.Rents.Update(rent);
            _dbContext.SaveChanges();
        }

        public void Delete(Rent rent)
        {
            _dbContext.Rents.Remove(rent);
            _dbContext.SaveChanges();
        }
    }
}