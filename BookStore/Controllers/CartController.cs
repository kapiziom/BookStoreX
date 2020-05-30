using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using BookStore.Services;
using BookStore.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BookStore.ViewModels;
using AutoMapper;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly IMapper _mapper;
        public CartController(ICartService cartService, IMapper mapper)
        {
            _cartService = cartService;
            _mapper = mapper;
        }

        [HttpPost("AddElement")]
        [Authorize]
        public async Task<IActionResult> AddToCart(AddCartElementVM addcart)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var cartItem = _mapper.Map<CartElement>(addcart);
            await _cartService.AddToCart(cartItem, userId);
            return Ok(new { meesage = "Item successfully added to your cart" });
        }

        /// <summary>
        /// Get current user's cart
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<List<CartVM>> GetCart()
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            return _mapper.Map<List<CartVM>>(await _cartService.GetUsersCart(userId));
        }

        
        /// <summary>
        /// Delete all items from current user's cart
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// update number of books in current user's cart
        /// </summary>
        /// <param name="bookID"></param>
        /// <param name="numberOfBooks"></param>
        /// <returns></returns>
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