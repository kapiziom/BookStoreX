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
        private readonly ICartRepository _cartRepository;
        private readonly IBooksRepository _bookRepository;
        private readonly IUserRepository _userRepository;
        public CartController(ICartRepository cartRepository, IBooksRepository booksRepository, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _cartRepository = cartRepository;
            _bookRepository = booksRepository;
        }

        [HttpPost("AddElement")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
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

        

        [HttpDelete("ClearCart")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        [HttpPut("EditBooksNumber/{id}/setNumber/{number}")]
        [Authorize]
        public IActionResult EditCartElement(int number, int id)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            bool check = _cartRepository.CheckCartElement(userId, id);
            if (check == true)
            {
                bool edit = _cartRepository.EditCartElement(number, id);
                if (edit == true)
                {
                    return Ok();
                }
                else return Conflict();
            }
            return Forbid();
        }

        
    }
}