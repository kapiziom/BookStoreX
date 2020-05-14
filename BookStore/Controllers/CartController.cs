using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using BookStore.Domain;
using BookStore.Services;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("AddElement")]
        [Authorize]
        public async Task<IActionResult> AddToCart(CartElement addcart)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var result = await _cartService.AddToCart(addcart, userId);
            return Ok(result.Value);
        }

        [HttpGet]
        [Authorize]
        public async Task<List<CartElement>> GetCart()
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var cart = await _cartService.GetUsersCart(userId);
            return cart;
        }

        

        [HttpDelete("ClearCart")]
        [Authorize]
        public async Task<IActionResult> DeleteCart()
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            await _cartService.DeleteCart(userId);
            return NoContent();
        }

        [HttpDelete("DeleteElement/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteCartElement(int id)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            await _cartService.DeleteCartElement(id, userId);
            return NoContent();
        }

        [HttpPut("EditBooksNumber/setNumber/{bookID}/{numberOfBooks}")]
        [Authorize]
        public async Task<IActionResult> EditCartElement(int bookID, int numberOfBooks)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var result = await _cartService.ChangeNumberOfBooksInCart(bookID, numberOfBooks, userId);
            return Ok(result.Value);
        }

        
    }
}