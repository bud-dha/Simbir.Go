﻿using Microsoft.EntityFrameworkCore;
using Simbir.Go.DAL.Contracts;
using Simbir.Go.DAL.Data;
using Simbir.Go.DAL.Models;

namespace Simbir.Go.DAL.Repositories
{
    public class AccountRepository : IRepository<Account>
    {
        private readonly ApplicationContext _dbContext = null!;


        public AccountRepository(ApplicationContext context)
        {
            _dbContext = context;
        }


        public void Create(Account account)
        {
            _dbContext.Accounts.Add(account);
            _dbContext.SaveChanges();
        }

        public async Task<IEnumerable<Account>> GetAll()
        {
            return await _dbContext.Accounts.ToListAsync();
        }

        public async Task<Account> GetByIdAsync(long id)
        {
            return await _dbContext.Accounts.FindAsync(id);
        }

        public Account GetById(long id)
        {
            return _dbContext.Accounts.FirstOrDefault(x => x.AccountId == id);
        }

        public void Update(Account account)
        {
            _dbContext.Accounts.Update(account);
            _dbContext.SaveChanges();
        }

        public void Delete(Account account)
        {
            _dbContext.Accounts.Remove(account);
            _dbContext.SaveChanges();
        }

        public Account Find(string username)
        {
            return _dbContext.Accounts.FirstOrDefault(a => a.Username == username);
        }

        public async Task<Account> FindAsync(string username)
        {
            return await _dbContext.Accounts.FirstOrDefaultAsync(a => a.Username == username);
        }
    }
}