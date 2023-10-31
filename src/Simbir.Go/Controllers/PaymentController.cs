using Microsoft.AspNetCore.Mvc;
using Simbir.Go.BLL.Services;
using Simbir.Go.Helpers;

namespace Simbir.Go.Controllers
{
    [ApiController, Route("api/Payment")]
    public class PaymentController : ControllerBase
    {
        private PaymentService _paymentService;


        public PaymentController(PaymentService paymentService)
        {
            _paymentService = paymentService;
        }


        [HttpPost, Route("Hesoyam/{id}")]
        public async Task<ActionResult> Post(long id)
        {
            try
            {
                await _paymentService.AddBalance(id, HttpContext.GetClaimsValueHttp("Role"), HttpContext.GetClaimsValueHttp("Username"));
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message);
            }
            return Ok();
        }
    }
}