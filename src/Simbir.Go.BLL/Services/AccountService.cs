using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Simbir.Go.BLL.DTO;
using Simbir.Go.DAL.Models;
using Simbir.Go.DAL.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Simbir.Go.BLL.Services
{
    public class AccountService
    {
        public IConfiguration _configuration;
        private AccountRepository _accountRepository;


        public AccountService(IConfiguration config, AccountRepository accountRepository)
        {
            _configuration = config;
            _accountRepository = accountRepository;
        }


        public async Task<Account> GetAccountInfo(string username)
        {
            var account = await _accountRepository.FindAsync(username);
            return account ?? throw new ArgumentException("Account wasn`t found in the database");
        }

        public async Task<string> GetJWT(AccountDTO dto)
        {
            var account = await _accountRepository.FindAsync(dto.Username) ?? throw new ArgumentException("Account wasn`t found in the database");
            if (dto.Password != account.Password)
                throw new ArgumentException("Incorrect login or password");

            var JWT = CreateJWT(account);
            return JWT ?? throw new ArgumentException("Failed to create access token");
        }

        public async Task CreateAccount(AccountDTO dto)
        {
            if (await _accountRepository.FindAsync(dto.Username) != null)
                throw new ArgumentException("Username is already exist");

            var newAccount = new Account
            {
                Username = dto.Username,
                Password = dto.Password
            };
            _accountRepository.Create(newAccount);
        }

        public async Task UpdateAccount(string username, AccountDTO dto)
        {
            var user = await _accountRepository.FindAsync(username);

            if (dto.Username != user.Username && await _accountRepository.FindAsync(dto.Username) != null)
                throw new ArgumentException("Username is already exist");

            var account = await _accountRepository.FindAsync(username) ?? throw new ArgumentException("Account wasn`t found in the database");

            ReplaceAccountData(account, dto);
            _accountRepository.Update(account);
        }

        public void SignOut(string username) { }


        private static void ReplaceAccountData(Account account, AccountDTO dto)
        {
            account.Username = dto.Username ?? account.Username;
            account.Password = dto.Password ?? account.Password;
        }

        private string CreateJWT(Account account)
        {
            var nowUtc = DateTime.UtcNow;
            var expirationDuration = TimeSpan.FromMinutes(60);
            var expirationUtc = nowUtc.Add(expirationDuration);

            var claims = new List<Claim> { new Claim("Username", account.Username), new Claim(ClaimTypes.Role, account.IsAdmin ? "Admin" : "User") };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurity:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSecurity:Issuer"],
                audience: _configuration["JwtSecurity:Audience"],
                claims: claims,
                expires: expirationUtc,
                signingCredentials: signIn);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}