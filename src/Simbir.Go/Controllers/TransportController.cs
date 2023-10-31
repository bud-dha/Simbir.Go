using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simbir.Go.BLL.DTO;
using Simbir.Go.BLL.Services;
using Simbir.Go.DAL.Models;
using Simbir.Go.Helpers;

namespace Simbir.Go.Controllers
{
    [ApiController, Route("api/Transport")]
    public class TransportController : ControllerBase
    {
        private TransportService _transportService;


        public TransportController(TransportService transportService)
        {
            _transportService = transportService;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Transport>> GetTransportById(int id)
        {
            try
            {
                return await _transportService.TransportById(id);
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost, Authorize]
        public async Task<ActionResult> Post([FromBody] TransportDTO transport)
        {
            try
            {
                await _transportService.CreateTransport(HttpContext.GetClaimsValueHttp("Username"), transport);
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message);
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(long id, [FromBody] TransportDTO transport)
        {
            try
            {
                await _transportService.UpdateTransport(id, transport, HttpContext.GetClaimsValueHttp("Username"));
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message);
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            try
            {
                await _transportService.DeleteTransport(id, HttpContext.GetClaimsValueHttp("Username"));
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message);
            }
            return Ok();
        }
    }
}