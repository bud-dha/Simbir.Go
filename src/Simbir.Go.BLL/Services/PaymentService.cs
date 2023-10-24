using Simbir.Go.DAL.Repositories;

namespace Simbir.Go.BLL.Services
{
    public class PaymentService
    {
        private AccountRepository _accountRepository;

        private const double balance = 250000;


        public PaymentService(AccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }


        public void AddBalance(long id)
        {
            var account = _accountRepository.GetById(id) ?? throw new ArgumentException("Account wasn`t found in the database");
            account.Balance += balance;
            _accountRepository.Update(account);
        }

    }
}