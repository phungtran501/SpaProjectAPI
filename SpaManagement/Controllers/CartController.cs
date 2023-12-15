using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpaManagement.Service.Abstracts;
using SpaManagement.Service.DTOs;
using SpaManagement.Service.DTOs.Cart;

namespace SpaManagement.Controllers
{

    public class CartController : BaseController
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("get-cart-item")]
        [AllowAnonymous]
        public async Task<IActionResult> GetListCart([FromBody] List<CartDTO> cartDTO)
        {
            var result = await _cartService.GetItemCart(cartDTO);
            return Ok(result);
        }
    }
}
