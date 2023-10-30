using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simbir.Go.BLL.DTO;
using Simbir.Go.BLL.Services;
using Simbir.Go.DAL.Models;
using Simbir.Go.Helpers;

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


        [HttpGet, Route("Me"), Authorize]
        public async Task<ActionResult<Account>> Me()
        {
            try
            {
                return await _accountService.GetAccountInfo(HttpContext.GetUsernameHttp());
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost, Route("SignIn")]
        public ActionResult<string> SignIn([FromBody] AccountDTO dto)
        {
            try
            {
                return _accountService.GetJWT(dto);
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost, Route("SignUp")]
        public ActionResult SigUp([FromBody] AccountDTO dto)
        {
            try
            {
                _accountService.CreateAccount(dto);
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message);
            }
            return Ok();
        }

        [HttpPost, Route("SignOut"), Authorize]
        public ActionResult SigOut()
        {
            try
            {
                _accountService.SignOut(HttpContext.GetUsernameHttp());
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message);
            }
            return Ok();
        }

        [HttpPut, Route("Update"), Authorize]
        public ActionResult Update([FromBody] AccountDTO dto)
        {
            try
            {
                _accountService.UpdateAccount(HttpContext.GetUsernameHttp(), dto);
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message);
            }
            return Ok();
        }
    }
}