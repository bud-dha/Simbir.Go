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


        [HttpGet]
        public async Task<ActionResult<List<Transport>>> Get(int count, int start, string type)
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

        [HttpGet("{id}")]
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


        [HttpPost]
        public ActionResult Post([FromBody] AdminTransportDTO transport)
        {
            try
            {
                _adminTransportService.CreateTransport(transport);
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message);
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] AdminTransportDTO transport)
        {
            try
            {
                _adminTransportService.UpdateTransport(id, transport);
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
                _adminTransportService.DeleteTransport(id);
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message);
            }
            return Ok();
        }
    }
}