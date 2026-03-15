using DAL.Models;
using DAL.Repositories.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PortalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRepository _paymentRepo;

        public PaymentController(IPaymentRepository paymentRepo)
        {
            _paymentRepo = paymentRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Payment>>> GetAll()
        {
            var payments = await _paymentRepo.GetAll();
            return Ok(payments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Payment>> GetById(int id)
        {
            var pay = await _paymentRepo.GetById(id);
            if (pay == null)
                return NotFound();
            return Ok(pay);
        }

        [HttpPost]
        public async Task<ActionResult<Payment>> Insert([FromBody] Payment payment)
        {
            payment.Id = 0;
            try
            {
                var pay = await _paymentRepo.Insert(payment);
                return Ok(pay);
            }
            catch (Exception ex)
            {
                return BadRequest(error: ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Payment>> Update(int id, [FromBody] Payment payment)
        {
            var pay = await _paymentRepo.GetById(id);
            try
            {
                if (pay == null)
                {
                    return NotFound();
                }
                pay = payment;
                pay.Id = id;
                var result = await _paymentRepo.Update(pay);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(error: ex.Message);
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Payment>> Delete(int id)
        {
            var pay = await _paymentRepo.GetById(id);
            try
            {
                if (pay == null)
                    return NotFound();

                await _paymentRepo.Delete(pay);
                return Ok(pay);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
