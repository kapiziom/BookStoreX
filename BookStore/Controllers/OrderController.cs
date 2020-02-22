using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;
        private readonly IUserRepository _userRepository;
        private readonly IOrderRepository _orderRepository;
        public OrderController(ICartRepository cartRepository, IUserRepository userRepository, IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _cartRepository = cartRepository;
        }

        [HttpPost("PlaceOrder")]
        [Authorize]
        public IActionResult PlaceOrder()
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            if (_userRepository.CheckAddressExist(userId) == false)
            {
                return BadRequest();
            }
            var CheckAddress = _userRepository.CheckAddressExist(userId);
            var cart = _cartRepository.GetUsersCart(userId);
            if (cart == null || cart.Count() < 1)
            {
                var error = new { succeeded = false };
                return BadRequest(error);
            }

            _orderRepository.PlaceOrder(userId);

            var message = new { succeeded = true };
            return Ok(message);
        }

        [HttpGet("ShoppingHistory")]
        [Authorize]
        public IActionResult ShoppingHistory()
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            if (_orderRepository.CheckUserOrders(userId) == false)
            {
                return NoContent();
            }
            var history = _orderRepository.History(userId);
            return Ok(history);
        }

        [HttpGet("OrderDetails/{id}")]
        [Authorize]
        public IActionResult OrderDetails(int id)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var orderDetails = _orderRepository.GetOrderDetails(id);
            var role = _userRepository.GetRole(userId);

            if (orderDetails.UserId == userId || role != "NormalUser")
            {
                return Ok(orderDetails);

            }
            return Forbid();
        }

        [HttpGet("Unshipped")]
        [Authorize(Roles = "Administrator,Worker")]
        public IActionResult Unshipped()
        {
            var orders = _orderRepository.Unshipped();
            return Ok(orders);
        }

        [HttpPut("Ship/{id}")]
        [Authorize(Roles = "Administrator,Worker")]
        public IActionResult Ship(int id)
        {
            var result = _orderRepository.Ship(id);
            if (result == true)
            {
                return Ok();
            }
            else return BadRequest();
            
        }

    }
}