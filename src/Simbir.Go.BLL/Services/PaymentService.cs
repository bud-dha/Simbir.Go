﻿using Simbir.Go.DAL.Repositories;

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


        public void AddBalance(long id, string role, string username)
        {
            var user = _accountRepository.Find(username);

            if (role == "Admin" || user.AccountId == id)
            {
                var account = _accountRepository.GetById(id) ?? throw new ArgumentException("Account wasn`t found in the database");
                account.Balance += balance;
                _accountRepository.Update(account);
            }
            else
                throw new ArgumentException("Can`t increase balance of this user");
        }

    }
}