using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simbir.Go.BLL.Services;
using Simbir.Go.DAL.Models;
using Simbir.Go.DAL.Models.Common;

namespace Simbir.Go.Controllers
{
    [ApiController, Route("api/Rent")]
    public class RentController : ControllerBase
    {
        private RentService _rentService;


        public RentController(RentService rentService)
        {
            _rentService = rentService;
        }


        [HttpGet, Route("Transport")]
        public async Task<ActionResult<List<Transport>>> Get(double latitude, double longitude, double radius, TransportTypes type)
        {
            try
            {
                return await _rentService.GetAllTransports(latitude, longitude, radius, type);
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message);
            }
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