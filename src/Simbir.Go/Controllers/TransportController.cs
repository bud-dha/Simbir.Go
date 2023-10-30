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
        public ActionResult Post([FromBody] TransportDTO transport)
        {
            try
            {
                _transportService.CreateTransport(HttpContext.GetUsernameHttp(), transport);
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message);
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] TransportDTO transport)
        {
            try
            {
                _transportService.UpdateTransport(id, transport, HttpContext.GetUsernameHttp());
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message);
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _transportService.DeleteTransport(id, HttpContext.GetUsernameHttp());
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message);
            }
            return Ok();
        }
    }
}