using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MSA.BankService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> ProcessedPaymentAsync(int orderId) { 
            return Ok(true);
        } 
    }
}
