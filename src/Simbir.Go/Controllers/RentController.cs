using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simbir.Go.BLL.Services;
using Simbir.Go.DAL.Models;
using Simbir.Go.Helpers;

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
        public async Task<ActionResult<List<Transport>>> GetTransports(double latitude, double longitude, double radius, string type = "All")
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

        [HttpGet("{rentId}"), Authorize]
        public async Task<ActionResult<Rent>> GetRentById(long rentId)
        {
            try
            {
                return await _rentService.RentById(rentId, HttpContext.GetUsernameHttp());
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet, Route("MyHistory"), Authorize]
        public async Task<ActionResult<List<Rent>>> GetRentHistory()
        {
            try
            {
                return await _rentService.MyHistory(HttpContext.GetUsernameHttp());
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet, Route("TransportHistory/{transportId}"), Authorize]
        public async Task<ActionResult<List<Rent>>> GetTransportHistory(long transportId)
        {
            try
            {
                return await _rentService.TransportHistory(transportId, HttpContext.GetUsernameHttp());
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost, Route("New/{transportId}"), Authorize]
        public async Task<ActionResult> CreateNewRent(int transportId, string rentType)
        {
            try
            {
                await _rentService.NewRent(transportId, rentType, HttpContext.GetUsernameHttp());
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message);
            }
            return Ok();
        }

        [HttpPost, Route("End/{rentId}")]
        public async Task<ActionResult> EndRent(int rentId, double latitude, double longitude)
        {
            try
            {
                await _rentService.EndRent(rentId, latitude, longitude, HttpContext.GetUsernameHttp());
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message);
            }
            return Ok();
        }
    }
}