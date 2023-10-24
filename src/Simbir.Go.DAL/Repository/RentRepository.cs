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
            _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Rent>> GetAll()
        {
            var obj = await _dbContext.Rents.ToListAsync();
            if (obj != null)
                return obj;
            else
                return null;
        }

        public async Task<Rent> GetByIdAsync(long id)
        {
            if (id != null)
            {
                var Obj = _dbContext.Rents.FirstOrDefault(x => x.RentId == id);
                if (Obj != null)
                    return Obj;
                else
                    return null;
            }
            else
                return null;
        }

        public Rent GetById(long id)
        {
            return _dbContext.Rents.FirstOrDefault(x => x.RentId == id);
        }

        public void Update(Rent rent)
        {
            if (rent != null)
            {
                var obj = _dbContext.Update(rent);
                if (obj != null)
                    _dbContext.SaveChanges();
            }
        }

        public void Delete(Rent rent)
        {
            if (rent != null)
            {
                var obj = _dbContext.Remove(rent);
                if (obj != null)
                    _dbContext.SaveChangesAsync();
            }
        }
    }
}