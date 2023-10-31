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

        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<Rent>> GetRentById(long id)
        {
            try
            {
                return await _rentService.RentById(id, HttpContext.GetClaimsValueHttp("Username"));
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
                return await _rentService.MyHistory(HttpContext.GetClaimsValueHttp("Username"));
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet, Route("TransportHistory/{id}"), Authorize]
        public async Task<ActionResult<List<Rent>>> GetTransportHistory(long id)
        {
            try
            {
                return await _rentService.TransportHistory(id, HttpContext.GetClaimsValueHttp("Username"));
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost, Route("New/{id}"), Authorize]
        public async Task<ActionResult> CreateNewRent(int id, string rentType)
        {
            try
            {
                await _rentService.NewRent(id, rentType, HttpContext.GetClaimsValueHttp("Username"));
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message);
            }
            return Ok();
        }

        [HttpPost, Route("End/{id}")]
        public async Task<ActionResult> EndRent(int id, double latitude, double longitude)
        {
            try
            {
                await _rentService.EndRent(id, latitude, longitude, HttpContext.GetClaimsValueHttp("Username"));
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message);
            }
            return Ok();
        }
    }
}