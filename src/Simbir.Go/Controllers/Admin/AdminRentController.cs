using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simbir.Go.BLL.DTO;
using Simbir.Go.BLL.Services.Admin;
using Simbir.Go.DAL.Models;

namespace Simbir.Go.Controllers.AdminArea
{
    [ApiController, Route("api/Admin")]
    public class AdminRentController : ControllerBase
    {
        private AdminRentService _adminRentService;


        public AdminRentController(AdminRentService adminRentService)
        {
            _adminRentService = adminRentService;
        }


        [HttpGet("Rent/{id}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<Rent>> GetRentById(int id)
        {
            try
            {
                return await _adminRentService.RentById(id);
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet, Route("UserHistory/{id}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Rent>>> GetUserHistory(int id)
        {
            try
            {
                return await _adminRentService.UserHistory(id);
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet, Route("TransportHistory/{id}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Rent>>> GetTransportHistory(int id)
        {
            try
            {
                return await _adminRentService.TransportHistory(id);
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost, Route("Rent"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> Post([FromBody] AdminRentDTO dto)
        {
            try
            {
                await _adminRentService.CreateRent(dto);
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message);
            }
            return Ok();
        }

        [HttpPost("Rent/End/{id}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> Post(long id, double latitude, double longitude)
        {
            try
            {
                await _adminRentService.EndRent(id, latitude, longitude);
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message);
            }
            return Ok();
        }

        [HttpPut("Rent/{id}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> Put(int id, [FromBody] AdminRentDTO dto)
        {
            try
            {
                await _adminRentService.UpdateRent(id, dto);
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message);
            }
            return Ok();
        }

        [HttpDelete("Rent/{id}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _adminRentService.DeleteRent(id);
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message);
            }
            return Ok();
        }
    }
}