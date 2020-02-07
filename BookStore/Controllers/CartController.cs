using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BookStore.Repository;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartOrderRepository _cartRepository;
        private readonly IBooksRepository _bookRepository;
        public CartController(ICartOrderRepository cartRepository, IBooksRepository booksRepository)
        {
            _cartRepository = cartRepository;
            _bookRepository = booksRepository;
        }

        [HttpPost("AddElement")]
        [Authorize]
        public IActionResult AddToCart(AddCartElementVM addcart)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var book =_bookRepository.GetBook(addcart.BookID);
            if(book.InStock < addcart.NumberOfBooks || book == null)
            {
                return Conflict();
            }
            else
            {
                _cartRepository.AddToCart(addcart, userId);
                return Ok();
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetCart()
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            List<CartVM> cart = _cartRepository.GetUsersCart(userId);
            if (cart == null)
            {
                return NoContent();
            }
            return Ok(cart);
        }

        [HttpPost("PlaceOrder")]
        [Authorize]
        public IActionResult PlaceOrder()
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var cart = _cartRepository.GetUsersCart(userId);
            if(cart == null || cart.Count() < 1)
            {
                var error = new { succeeded = false };
                return BadRequest(error);
            }

            _cartRepository.PlaceOrder(userId);
            
            var message = new { succeeded = true };
            return Ok(message);
        }

        [HttpDelete("ClearCart")]
        [Authorize]
        public IActionResult DeleteCart()
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var result = _cartRepository.DeleteCart(userId);
            if(result == false)
            {
                var error = new { successed = false };
                return BadRequest(error);
            }
            var message = new { successed = true };
            return Ok(message);
        }

        [HttpGet("ShoppingHistory")]
        [Authorize]
        public IActionResult ShoppingHistory()
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            if(_cartRepository.CheckUserOrders(userId) == false)
            {
                return NoContent();
            }
            var history = _cartRepository.History(userId);
            return Ok(history);
        }

        [HttpGet("OrderDetails/{id}")]
        [Authorize]
        public IActionResult OrderDetails(int id)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var orderDetails = _cartRepository.GetOrderDetail(id);

            if (orderDetails.UserId != userId)
            {
                return Forbid();
            }
            var history = _cartRepository.History(userId);
            return Ok(history);
        }
    }
}