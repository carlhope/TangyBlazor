﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using Stripe.FinancialConnections;
using Tangy_Business.Repository;
using Tangy_Business.Repository.IRepository;
using Tangy_Models;
using Stripe.Checkout;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace TangyWeb_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IEmailSender _emailSender;
        public OrderController(IOrderRepository orderRepository, IEmailSender emailSender)
        {
            _orderRepository = orderRepository;
            _emailSender = emailSender;

        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _orderRepository.GetAll());
        }
        [HttpGet("{orderHeaderId}")]
        public async Task<IActionResult> Get(int? orderHeaderId)
        {
            if (orderHeaderId == null || orderHeaderId == 0)
            {
                return BadRequest(new ErrorModelDTO()
                {

                    ErrorMessage = "Invalid Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            var orderHeader = await _orderRepository.Get(orderHeaderId.Value);
            if (orderHeader == null)
            {
                return BadRequest(new ErrorModelDTO()
                {

                    ErrorMessage = "Invalid Id",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }
            return Ok(orderHeader);
        }

        [HttpPost]
        [ActionName("Create")]
        public async Task<IActionResult> Create([FromBody] StripePaymentDTO paymentDTO)
        {
            paymentDTO.Order.OrderHeader.OrderDate = DateTime.Now;
            var result = await _orderRepository.Create(paymentDTO.Order);
            return Ok(result);
        }
        [HttpPost]
        [ActionName("paymentsuccessful")]
        public async Task<IActionResult> PaymentSuccessful([FromBody] OrderHeaderDTO orderHeaderDTO)
        {
            var service = new SessionService();
            var sessionDetails = service.Get(orderHeaderDTO.SessionId);
            if (sessionDetails.PaymentStatus == "paid")
            {
                var result = await _orderRepository.MarkPaymentSuccessful(orderHeaderDTO.Id, sessionDetails.PaymentIntentId);
                _emailSender.SendEmailAsync(orderHeaderDTO.Email,"Tangy Order Confirmation","New Order has been created : "+orderHeaderDTO.Id);
                if (result == null)
                {
                    return BadRequest(new ErrorModelDTO()
                    {
                        ErrorMessage = "Payment was not successful"

                    });
                }
                return Ok(result);
            }
            return BadRequest();




        }
    }
}
