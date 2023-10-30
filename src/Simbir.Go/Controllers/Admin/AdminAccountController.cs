using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simbir.Go.BLL.DTO;
using Simbir.Go.BLL.Services;
using Simbir.Go.DAL.Models;

namespace Simbir.Go.Controllers.AdminArea
{
    [ApiController, Route("api/Admin/Account")]
    public class AdminAccountController : ControllerBase
    {
        private AdminAccountService _adminAccountService;


        public AdminAccountController(AdminAccountService adminAccountService) { _adminAccountService = adminAccountService; }


        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Account>>> Get(int count, int start)
        {
            try
            {
                return await _adminAccountService.GetAllAccounts(count, start);
            }
            catch(ArgumentException ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{id}"), Authorize (Roles = "Admin")]
        public async Task<ActionResult<Account>> Get(long id)
        {
            try
            {
                return await _adminAccountService.GetAccountById(id);
            }
            catch(ArgumentException ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost, Authorize(Roles = "Admin")]
        public ActionResult Post([FromBody] AdminAccountDTO account)
        {
            try
            {
                _adminAccountService.CreateAccount(account);
            }
            catch(ArgumentException ex)
            {
                return Problem(ex.Message);
            }
            return Ok();
        }

        [HttpPut("{id}"), Authorize(Roles = "Admin")]
        public ActionResult Put(long id, [FromBody] AdminAccountDTO account)
        {
            try
            {
                _adminAccountService.UpdateAccount(id, account);
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message);
            }
            return Ok();
        }

        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public ActionResult Delete(long id)
        {
            try
            {
                _adminAccountService.DeleteAccount(id);
            }
            catch(ArgumentException ex)
            {
                return Problem(ex.Message);
            }
            return Ok();
        }
    }
}