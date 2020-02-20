using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Repository;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartOrderRepository _cartRepository;
        private readonly IBooksRepository _bookRepository;
        private readonly IUserRepository _userRepository;
        public CartController(ICartOrderRepository cartRepository, IBooksRepository booksRepository, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _cartRepository = cartRepository;
            _bookRepository = booksRepository;
        }

        [HttpPost("AddElement")]
        [Authorize]
        public IActionResult AddToCart(AddCartElementVM addcart)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var book =_bookRepository.GetBook(addcart.BookID);
            if(book.InStock < addcart.NumberOfBooks || book == null || addcart.NumberOfBooks <= 0)
            {
                var message = new { succeeded = false };
                return Conflict(message);
            }
            else
            {
                _cartRepository.AddToCart(addcart, userId);
                var message = new { succeeded = true };
                return Ok(message);
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
            if (_userRepository.CheckAddressExist(userId) == false)
            {
                return BadRequest();
            }
            var CheckAddress = _userRepository.CheckAddressExist(userId);
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
                return BadRequest();
            }
            var message = new { successed = true };
            return Ok(message);
        }

        [HttpDelete("DeleteElement/{id}")]
        [Authorize]
        public IActionResult DeleteCartElement(int id)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            string role = _userRepository.GetRole(userId);
            bool check = _cartRepository.CheckCartElement(userId, id);
            if (check == true || role != "NormalUser")
            {
                _cartRepository.DeleteCartElement(id);
                return Ok();
            }
            return Forbid();
        }

        [HttpPut("EditBooksNumber/{id}")]
        [Authorize]
        public IActionResult EditCartElement([FromBody] EditCartElement cartElement, int id)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            bool check = _cartRepository.CheckCartElement(userId, id);
            if(check == true)
            {
                _cartRepository.EditCartElement(cartElement, id);
                return Ok();
            }
            return Forbid();
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
            var orderDetails = _cartRepository.GetOrderDetails(id);
            var role = _userRepository.GetRole(userId);

            if (orderDetails.UserId == userId || role != "NormalUser")
            {
                return Ok(orderDetails);
                
            }
            return Forbid();
        }
    }
}