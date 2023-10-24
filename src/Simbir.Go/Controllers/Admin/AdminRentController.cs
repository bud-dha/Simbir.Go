using Microsoft.AspNetCore.Mvc;

namespace Simbir.Go.Controllers.AdminArea
{
    [ApiController, Route("api/Admin")]
    public class AdminRentController : ControllerBase
    {
        [HttpGet("Rent/{rentId}")]
        public BadRequestResult Get(int rentId)
        {
            return BadRequest();
        }

        [HttpGet]
        [Route("UserHistory/{userId}")]
        public BadRequestResult GetUserHistory(int userId)
        {
            return BadRequest();
        }

        [HttpGet]
        [Route("TransportHistory/{transportId}")]
        public BadRequestResult GetTransportHistory(int transportId)
        {
            return BadRequest();
        }

        [HttpPost, Route("Rent")]
        public BadRequestResult Post()
        {
            return BadRequest();
        }

        [HttpPost("Rent/End/{rentId}")]
        public BadRequestResult Post(int rentId)
        {
            return BadRequest();
        }

        [HttpPut("Rent/{id}")]
        public BadRequestResult Put(int id)
        {
            return BadRequest();
        }

        [HttpDelete("Rent/{rentId}")]
        public BadRequestResult Delete(int rentId)
        {
            return BadRequest();
        }
    }
}