using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Simbir.Go.Controllers
{
    [ApiController, Route("api/Rent")]
    public class RentController : ControllerBase
    {
        [HttpGet, Route("Transport")]
        public BadRequestResult Get()
        {
            return BadRequest();
        }

        [HttpGet("{rentId}")]
        public BadRequestResult Get(int rentId)
        {
            return BadRequest();
        }

        [HttpGet, Route("MyHistory"), Authorize]
        public BadRequestResult MyHistory()
        {
            return BadRequest();
        }

        [HttpGet, Route("TransportHistory/{transportId}")]
        public BadRequestResult TransportHistory(int transportId)
        {
            return BadRequest();
        }

        [HttpPost, Route("New/{transportId}"), Authorize]
        public BadRequestResult Update(int transportId)
        {
            return BadRequest();
        }

        [HttpPost, Route("End/{rentId}")]
        public BadRequestResult End(int rentId)
        {
            return BadRequest();
        }
    }
}