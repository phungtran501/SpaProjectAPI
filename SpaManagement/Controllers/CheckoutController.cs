using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpaManagement.Domain.Entities;
using SpaManagement.Service.Abstracts;
using SpaManagement.Service.DTOs.Cart;

namespace SpaManagement.Controllers
{
    public class CheckoutController : BaseController
    {
        private readonly IAddressService _addressService;
        private readonly IOrderService _orderService;

        public CheckoutController(IAddressService addressService, IOrderService orderService)
        {
            _addressService = addressService;
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] CheckoutCartDTO checkoutCartDTO)
        {
            var addressId = await _addressService.Save(checkoutCartDTO);

            if (addressId != 0)
            {
                var result = await _orderService.Save(checkoutCartDTO, addressId);
                return Ok(result);

            }
            else
            {
                return BadRequest("Save cart failed");
            }

        }
    }
}
