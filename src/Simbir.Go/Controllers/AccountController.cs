using Microsoft.AspNetCore.Mvc;
using Simbir.Go.BLL.DTO;
using Simbir.Go.BLL.Services;
using Simbir.Go.DAL.Models;

namespace Simbir.GO.Controllers
{
    [ApiController, Route("api/Account")]
    public class AccountController : ControllerBase
    {
        private AccountService _accountService;


        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }


        [HttpGet, Route("Me")]
        public async Task<ActionResult<Account>> Me()
        {
            try
            {
                return await _accountService.GetAccountInfo(10); // �������� ������.
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost, Route("SignIn")]
        public object SignIn([FromBody] AccountDTO account)
        {
            try
            {
                return _accountService.GetJWT(account);
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost, Route("SignUp")]
        public ActionResult SigUp([FromBody] AccountDTO account)
        {
            try
            {
                _accountService.CreateAccount(account);
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message);
            }
            return Ok();
        }

        [HttpPost, Route("SignOut")]
        public ActionResult SigOut([FromBody] AccountDTO account)
        {
            try
            {
                _accountService.CreateAccount(account);
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message);
            }
            return Ok();
        }

        [HttpPut, Route("Update")]
        public ActionResult Update([FromBody] AccountDTO account)
        {
            try
            {
                _accountService.UpdateAccount(1, account); // �������� ������.
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message);
            }
            return Ok();
        }
    }
}