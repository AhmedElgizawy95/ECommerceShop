using ECommerceShop.DataAccess.Implementation;
using ECommerceShop.Entities.Models.Domain;
using ECommerceShop.Entities.Models.DTO;
using ECommerceShop.Entities.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        private IUnitofWork _unitofWork;
        public CartItemController(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;

        }


        // POST: api/CartItem
        [HttpPost]
        public IActionResult CreateCartItem(CartItemCreateDto cartItemDto)
        {
            if (cartItemDto == null)
            {
                return BadRequest("Cart item data is required.");
            }

            try
            {
                var cart = _unitofWork.Cart.GetFirstOrDefault(c => c.CartId == cartItemDto.CartId);
                if (cart == null)
                {
                    return NotFound("Cart not found.");
                }

                var cartItem = new CartItem
                {
                    ProductId = cartItemDto.ProductId,
                    Quantity = cartItemDto.Quantity,
                    CartId = cartItemDto.CartId
                };

                _unitofWork.CartItem.Add(cartItem);
                 _unitofWork.Complete();

                return Ok(new { CartItemId = cartItem.CartItemId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

    }
}
