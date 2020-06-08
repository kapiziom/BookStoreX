using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Services;
using BookStore.Domain;
using BookStore.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using BookStore.ViewModels;
using System.Security.Claims;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IAddressService _addressService;
        private readonly IMapper _mapper;
        public OrderController(IOrderService orderService, IAddressService addressService, IMapper mapper)
        {
            _orderService = orderService;
            _addressService = addressService;
            _mapper = mapper;
        }

        [HttpPost("PlaceOrder")]
        [Authorize]
        public async Task<IActionResult> PlaceOrder()
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var address = await _addressService.GetAddressByUserId(userId);
            var orderAddress = new Order()
            {
                UserId = userId,
                FirstName = address.FirstName,
                LastName = address.LastName,
                City = address.City,
                Country = address.Country,
                PostalCode = address.PostalCode,
                Street = address.Street,
                Number = address.Number,
            };
            await _orderService.PlaceOrder(userId, orderAddress);
            return Ok(new { succeeded = true });
        }

        [HttpGet("ShoppingHistory/{page}/{itemsPerPage}")]
        [Authorize]
        public async Task<PagedList<OrderVM>> ShoppingHistory(int page, int itemsPerPage)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var history = await _orderService.History(userId, page, itemsPerPage);
            var vm = new PagedList<OrderVM>()
            {
                TotalItems = history.TotalItems,
                PageCount = history.PageCount,
                Page = page,
                ItemsPerPage = itemsPerPage,
                Items = _mapper.Map<List<OrderVM>>(history.Items),
            };
            return vm;
        }

        [HttpGet("OrderDetails/{id}")]
        [Authorize]
        public async Task<IActionResult> OrderDetails(int id)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var role = User.Claims.First(c => c.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
            var order = await _orderService.GetOrderById(id, userId);

            if (userId == order.UserId || role == "Worker" || role == "Administrator")
            {
                var vm = _mapper.Map<OrderWithDetailsVM>(order);
                vm.OrderDetailsVM = _mapper.Map<List<OrderDetailsVM>>(order.OrderDetails);
                return Ok(vm);
            }
            else return Forbid();
            
        }

        [HttpGet("Unshipped/{page}/{itemsPerPage}")]
        [Authorize(Roles = "Administrator,Worker")]
        public async Task<PagedList<OrderVM>> Unshipped(int page, int itemsPerPage)
        {
            var orders = await _orderService.GetPagedOrders(m => m.IsShipped == false, x => x.OrderDate, page, itemsPerPage);
            var vm = new PagedList<OrderVM>()
            {
                TotalItems = orders.TotalItems,
                PageCount = orders.PageCount,
                Page = page,
                ItemsPerPage = itemsPerPage,
                Items = _mapper.Map<List<OrderVM>>(orders.Items),
            };
            return vm;
        }

        [HttpPut("Ship/{id}")]
        [Authorize(Roles = "Administrator,Worker")]
        public async Task<IActionResult> Ship(int id)
        {
            await _orderService.Ship(id);
            return Ok();
        }

    }
}