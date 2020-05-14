using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Domain;
using BookStore.Domain.Common;
using BookStore.Services;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("PlaceOrder")]
        [Authorize]
        public async Task<IActionResult> PlaceOrder([FromBody] Order order)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            await _orderService.PlaceOrder(userId, order);
            var message = new { succeeded = true };
            return Ok(message);
        }

        [HttpGet("ShoppingHistory/{page}/{itemsPerPage}")]
        [Authorize]
        public async Task<PagedList<Order>> ShoppingHistory(int page, int itemsPerPage)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var history = await _orderService.History(userId, page, itemsPerPage);
            return history;
        }

        [HttpGet("OrderDetails/{id}")]
        [Authorize]
        public async Task<IActionResult> OrderDetails(int id)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var order = await _orderService.GetOrderById(id, userId);
            return Ok(order);
        }

        [HttpGet("Unshipped")]
        [Authorize(Roles = "Administrator,Worker")]
        public async Task<PagedList<Order>> Unshipped(int page, int itemsPerPage)
        {
            var orders = await _orderService.GetPagedOrders(m => m.IsShipped == false, x => x.OrderDate, page, itemsPerPage);
            return orders;
        }

        [HttpPut("Ship/{id}")]
        [Authorize(Roles = "Administrator,Worker")]
        public async Task<IActionResult> Ship(int id)
        {
            await _orderService.Ship(id);
            return NoContent();
        }

    }
}