using Microsoft.AspNetCore.Mvc;
using Simbir.Go.BLL.DTO;
using Simbir.Go.BLL.Services;
using Simbir.Go.DAL.Models;

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
        public async Task<ActionResult<Transport>> Get(int id)
        {
            try
            {
                return await _transportService.GetTransportById(id);
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] TransportDTO transport)
        {
            try
            {
                _transportService.CreateTransport(transport);
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
                _transportService.UpdateTransport(id, transport);
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
                _transportService.DeleteTransport(id);
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message);
            }
            return Ok();
        }
    }
}