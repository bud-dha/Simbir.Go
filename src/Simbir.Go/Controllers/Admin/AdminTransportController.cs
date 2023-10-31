using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simbir.Go.BLL.DTO;
using Simbir.Go.BLL.Services.Admin;
using Simbir.Go.DAL.Models;

namespace Simbir.Go.Controllers.AdminArea
{
    [ApiController, Route("api/Admin/Transport")]
    public class AdminTransportController : ControllerBase
    {
        private AdminTransportService _adminTransportService;


        public AdminTransportController(AdminTransportService transportService)
        {
            _adminTransportService = transportService;
        }


        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Transport>>> Get(int count, int start, string type = "All")
        {
            try
            {
                return await _adminTransportService.GetAllTransports(count, start, type);
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{id}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<Transport>> Get(int id)
        {
            try
            {
                return await _adminTransportService.GetTransportById(id);
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message);
            }
        }


        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<ActionResult> Post([FromBody] AdminTransportDTO transport)
        {
            try
            {
                await _adminTransportService.CreateTransport(transport);
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message);
            }
            return Ok();
        }

        [HttpPut("{id}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> Put(int id, [FromBody] AdminTransportDTO transport)
        {
            try
            {
                await _adminTransportService.UpdateTransport(id, transport);
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message);
            }
            return Ok();
        }

        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _adminTransportService.DeleteTransport(id);
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message);
            }
            return Ok();
        }
    }
}