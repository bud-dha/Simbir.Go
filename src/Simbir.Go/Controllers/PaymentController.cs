using Microsoft.AspNetCore.Mvc;
using Simbir.Go.BLL.Services;

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


        [HttpPost, Route("Hesoyam/{accountId}")]
        public IActionResult Post(long accountId)
        {
            try
            {
                _paymentService.AddBalance(accountId);
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message);
            }
            return Ok();
        }
    }
}