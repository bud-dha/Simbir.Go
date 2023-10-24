using Microsoft.AspNetCore.Mvc;

namespace Simbir.Go.Controllers.AdminArea
{
    [ApiController, Route("api/Admin/Transport")]
    public class AdminTransportController : ControllerBase
    {
        [HttpGet]
        public BadRequestResult Get()
        {
            return BadRequest();
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