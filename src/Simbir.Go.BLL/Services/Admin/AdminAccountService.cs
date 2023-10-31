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

        public async Task<Account> GetAccountById(long accountId)
        {
            var account = await _accountRepository.GetByIdAsync(accountId);
            return account ?? throw new ArgumentException("Account wasn`t found in the database");
        }

        public async Task CreateAccount(AdminAccountDTO dto)
        {
            if (await _accountRepository.FindAsync(dto.Username) != null)
                throw new ArgumentException("Username is already exist");

            var newAccount = new Account{
                Username = dto.Username,
                Password = dto.Password,
                IsAdmin = dto.IsAdmin,
                Balance = dto.Balance
            };
            _accountRepository.Create(newAccount);
        }

        public async Task UpdateAccount(long accountId, AdminAccountDTO dto)
        {
            var user = await _accountRepository.GetByIdAsync(accountId);

            if (dto.Username != user.Username && await _accountRepository.FindAsync(dto.Username) != null)
                throw new ArgumentException("Username is already exist");

            var account = await _accountRepository.GetByIdAsync(accountId) ?? throw new ArgumentException("Account wasn`t found in the database");

            ReplaceAccountData(account, dto);
            _accountRepository.Update(account);
        }

        public async Task DeleteAccount(long accountId)
        {
            var account = await _accountRepository.GetByIdAsync(accountId) ?? throw new ArgumentException("Account wasn`t found in the database");
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