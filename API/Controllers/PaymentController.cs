using System.IO;
using System.Threading.Tasks;
using API.Errors;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Order = Core.Entities.OrderAggregate.Order;

namespace API.Controllers
{
    public class PaymentController(
        IPaymentService paymentService,
        ILogger<PaymentController> logger,
        IConfiguration config)
        : BaseApiController
    {
        private readonly string _whSecret = config.GetSection("StripeSettings:WhSecret").Value;

        [Authorize]
        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var basket = await paymentService.CreateOrUpdatePaymentIntent(basketId);

            if (basket == null) return BadRequest(new ApiResponse(400, "Problem with your basket"));

            return basket;
        }

        [HttpPost("webhook")]
        public async Task<ActionResult> StripeWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], _whSecret);

            PaymentIntent intent;
            Order order;

            switch (stripeEvent.Type)
            {
                case "payment_intent.succeeded":
                    intent = (PaymentIntent)stripeEvent.Data.Object;
                    logger.LogInformation("Payment Succeeded: ", intent.Id);
                    order = await paymentService.UpdateOrderPaymentSucceeded(intent.Id);
                    logger.LogInformation("Order updated to payment received: ", order.Id);
                    break;

                case "payment_intent.payment_failed":
                    intent = (PaymentIntent)stripeEvent.Data.Object;
                    logger.LogInformation("Payment Failed: ", intent.Id);
                    order = await paymentService.UpdateOrderPaymentFailed(intent.Id);
                    logger.LogInformation("Payment Failed: ", order.Id);
                    break;
            }

            return new EmptyResult();
        }
    }
}