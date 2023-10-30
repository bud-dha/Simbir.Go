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


        public async Task<Account> GetAccountInfo(long id)
        {
            var account = await _accountRepository.GetByIdAsync(id);
            return account ?? throw new ArgumentException("Account wasn`t found in the database");
        }

        public string GetJWT(AccountDTO dto)
        {
            var account = _accountRepository.Find(dto.Username) ?? throw new ArgumentException("Account wasn`t found in the database");

            var JWT = CreateJWT(account);
            return JWT ?? throw new ArgumentException("Failed to create access token");
        }

        public void CreateAccount(AccountDTO dto)
        {
            if (_accountRepository.Find(dto.Username) != null)
                throw new ArgumentException("Username is already exist");

            var newAccount = new Account(dto.Username, dto.Password);
            _accountRepository.Create(newAccount);
        }

        public void UpdateAccount(long id, AccountDTO dto)
        {
            if (_accountRepository.Find(dto.Username) != null)
                throw new ArgumentException("Username is already exist");

            var account = _accountRepository.GetById(id) ?? throw new ArgumentException("Account wasn`t found in the database");

            ReplaceAccountData(account, dto);
            _accountRepository.Update(account);
        }


        private static void ReplaceAccountData(Account account, AccountDTO dto)
        {
            account.Username = dto.Username ?? account.Username;
            account.Password = dto.Password ?? account.Password;
        }

        private string CreateJWT(Account account)
        {
            var nowUtc = DateTime.UtcNow;
            var expirationDuration = TimeSpan.FromMinutes(10); // Adjust as needed
            var expirationUtc = nowUtc.Add(expirationDuration);

            var claims = new List<Claim> {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["JwtSecurity:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, EpochTime.GetIntDate(nowUtc).ToString(), ClaimValueTypes.Integer64),
                        new Claim(JwtRegisteredClaimNames.Exp, EpochTime.GetIntDate(expirationUtc).ToString(), ClaimValueTypes.Integer64),
                        new Claim(JwtRegisteredClaimNames.Iss, _configuration["JwtSecurity:Issuer"]),
                        new Claim(JwtRegisteredClaimNames.Aud, _configuration["JwtSecurity:Audience"]),
                        new Claim("Id", account.AccountId.ToString()),
                        new Claim(ClaimTypes.Role, account.IsAdmin ? "Admin" : "User")
            };

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