using Simbir.Go.BLL.DTO;
using Simbir.Go.DAL.Repositories;
using Simbir.Go.DAL.Models;

namespace Simbir.Go.BLL.Services
{
    public class AdminAccountService
    {
        private AccountRepository _accountRepository;


        public AdminAccountService(AccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }


        public async Task<List<Account>> GetAllAccounts(int count, int start = 1)
        {
            var accounts = await _accountRepository.GetAll() ?? throw new ArgumentException("Accounts wasn`t found in the database");

            if (count == 0)
                return accounts.Skip(start - 1).ToList();

            return accounts.Skip(start - 1).Take(count).ToList();
        }

        public async Task<Account> GetAccountById(long id)
        {
            var account = await _accountRepository.GetByIdAsync(id);
            return account ?? throw new ArgumentException("Account wasn`t found in the database");
        }

        public void CreateAccount(AdminAccountDTO dto)
        {
            var newAccount = new Account(dto.Username, dto.Password, dto.IsAdmin, dto.Balance);
            _accountRepository.Create(newAccount);
        }

        public void UpdateAccount(long id, AdminAccountDTO dto)
        {
            if (_accountRepository.Find(dto.Username) != null)
                throw new ArgumentException("Username is already exist");

            var account = _accountRepository.GetById(id) ?? throw new ArgumentException("Account wasn`t found in the database");

            ReplaceAccountData(account, dto);
            _accountRepository.Update(account);
        }

        public void DeleteAccount(long id)
        {
            var account = _accountRepository.GetById(id) ?? throw new ArgumentException("Account wasn`t found in the database");
            _accountRepository.Delete(account);
        }


        private static void ReplaceAccountData(Account account, AdminAccountDTO dto)
        {
            account.Username = dto.Username ?? account.Username;
            account.Password = dto.Password ?? account.Password;
            account.IsAdmin = dto.IsAdmin;
            account.Balance = dto.Balance;
        }
    }
}