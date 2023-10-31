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
                return await _accountService.GetAccountInfo(HttpContext.GetClaimsValueHttp("Username"));
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost, Route("SignIn")]
        public async Task<ActionResult<string>> SignIn([FromBody] AccountDTO dto)
        {
            try
            {
                return await _accountService.GetJWT(dto);
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost, Route("SignUp")]
        public async Task<ActionResult> SigUp([FromBody] AccountDTO dto)
        {
            try
            {
                await _accountService.CreateAccount(dto);
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
                _accountService.SignOut(HttpContext.GetClaimsValueHttp("Username"));
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message);
            }
            return Ok();
        }

        [HttpPut, Route("Update"), Authorize]
        public async Task<ActionResult> Update([FromBody] AccountDTO dto)
        {
            try
            {
                await _accountService.UpdateAccount(HttpContext.GetClaimsValueHttp("Username"), dto);
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message);
            }
            return Ok();
        }
    }
}