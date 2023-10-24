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


        [HttpGet]
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

        [HttpGet("{id}")]
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

        [HttpPost]
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

        [HttpPut("{id}")]
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

        [HttpDelete("{id}")]
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