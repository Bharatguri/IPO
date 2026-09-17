using IPO.Application.Interfaces.Services;
using IPO.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IPO.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        // GET: api/Payment
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var payments = await _paymentService.GetAllAsync();

            return Ok(payments);
        }

        // GET: api/Payment/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var payment = await _paymentService.GetByIdAsync(id);

            if (payment is null)
            {
                return NotFound(new
                {
                    message = "Payment not found"
                });
            }

            return Ok(payment);
        }

        // POST: api/Payment
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] Payment payment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _paymentService.AddAsync(payment);

            return Ok(new
            {
                message = "Payment created successfully",
                data = payment
            });
        }

        // PUT: api/Payment/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] Payment payment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingPayment =
                await _paymentService.GetByIdAsync(id);

            if (existingPayment is null)
            {
                return NotFound(new
                {
                    message = "Payment not found"
                });
            }

            payment.Id = id;

            await _paymentService.UpdateAsync(payment);

            return Ok(new
            {
                message = "Payment updated successfully",
                data = payment
            });
        }

        // DELETE: api/Payment/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingPayment =
                await _paymentService.GetByIdAsync(id);

            if (existingPayment is null)
            {
                return NotFound(new
                {
                    message = "Payment not found"
                });
            }

            await _paymentService.DeleteAsync(id);

            return Ok(new
            {
                message = "Payment deleted successfully"
            });
        }
    }
}
