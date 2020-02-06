using System;
using System.Collections.Generic;
using System.Linq;
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
                return NotFound();
            }
            return Ok(cart);
        }

        [HttpPost("PlaceOrder")]
        [Authorize]
        public IActionResult PlaceOrder()
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            _cartRepository.PlaceOrder(userId);
            return Ok();
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
            return Ok();
        }

        [HttpGet("ShoppingHistory")]
        [Authorize]
        public IActionResult ShoppingHistory()
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            if(_cartRepository.CheckUserOrders(userId) == false)
            {
                return NotFound();
            }
            var history = _cartRepository.History(userId);
            return Ok(history);
        }

    }
}