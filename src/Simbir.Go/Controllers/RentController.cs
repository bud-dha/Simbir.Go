using Microsoft.AspNetCore.Mvc;
using Simbir.Go.BLL.Services;
using Simbir.Go.DAL.Models;

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
        public async Task<ActionResult<List<Transport>>> GetTransports(double latitude, double longitude, double radius, string type)
        {
            try
            {
                return await _rentService.Transports(latitude, longitude, radius, type);
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{rentId}")]
        public async Task<ActionResult<Rent>> GetRentById(long rentId)
        {
            try
            {
                return await _rentService.RentById(rentId);
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet, Route("MyHistory")]
        public async Task<ActionResult<List<Rent>>> GetRentHistory()
        {
            try
            {
                return await _rentService.RentHistory(10); // id текущего аккаунта.
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet, Route("TransportHistory/{transportId}")]
        public async Task<ActionResult<List<Rent>>> GetTransportHistory(long transportId)
        {
            try
            {
                return await _rentService.TransportHistory(transportId);
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost, Route("New/{transportId}")]
        public ActionResult CreateNewRent(int transportId, string transportType)
        {
            try
            {
                _rentService.NewRent(transportId, transportType);
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message);
            }
            return Ok();
        }

        [HttpPost, Route("End/{rentId}")]
        public ActionResult EndRent(int rentId, double latitude, double longitude)
        {
            try
            {
                _rentService.EndRent(rentId, latitude, longitude);
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message);
            }
            return Ok();
        }
    }
}