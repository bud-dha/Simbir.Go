using Simbir.Go.BLL.DTO;
using Simbir.Go.DAL.Repositories;
using Simbir.Go.DAL.Models;

namespace Simbir.Go.BLL.Services
{
    public class AccountService
    {
        private AccountRepository _accountRepository;


        public AccountService(AccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }


        public async Task<Account> GetAccountInfo(long id)
        {
            var account = await _accountRepository.GetByIdAsync(id);
            return account ?? throw new ArgumentException("Account wasn`t found in the database");
        }

        public string GetJWT(AccountDTO dto)
        {
            var JWT = "";
            return JWT ?? throw new ArgumentException("Account wasn`t found in the database");
        }

        public void CreateAccount(AccountDTO dto)
        {
            var newAccount = new Account(dto.Username, dto.Password);
            _accountRepository.Create(newAccount);
        }

        public void UpdateAccount(long id, AccountDTO dto)
        {
            var account = _accountRepository.GetById(id) ?? throw new ArgumentException("Account wasn`t found in the database");

            ReplaceAccountData(account, dto);
            _accountRepository.Update(account);
        }


        private static void ReplaceAccountData(Account account, AccountDTO dto)
        {
            account.Username = dto.Username ?? account.Username;
            account.Password = dto.Password ?? account.Password;
        }
    }
}