using Microsoft.AspNetCore.Mvc;
using Simbir.Go.BLL.Services;
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

        [HttpGet("{id}")]
        public BadRequestResult Get(int id)
        {
            return BadRequest();
        }

        [HttpPost]
        public BadRequestResult Post()
        {
            return BadRequest();
        }

        [HttpPut("{id}")]
        public BadRequestResult Put(int id)
        {
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public BadRequestResult Delete(int id)
        {
            return BadRequest();
        }
    }
}