using ECommerceShop.DataAccess.Implementation;
using ECommerceShop.Entities.Models.Domain;
using ECommerceShop.Entities.Models.DTO;
using ECommerceShop.Entities.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceShop.Controllers
{
    [Route("[controller]")] // /api
    [ApiController]
    public class CartController : ControllerBase
    {
        private IUnitofWork _unitofWork;
        public CartController(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;

        }
        [HttpPost]
        public IActionResult Create([FromBody] CartCreateDto cartDto) //Category category
        {

            if (cartDto == null)
            {
                return BadRequest("Category data is required.");
            }

            try
            {
                var cart = new Cart
                {
                    UserId = cartDto.UserId

                };

                _unitofWork.Cart.Add(cart);
                _unitofWork.Complete();

                return Ok(cartDto.UserId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpGet("{id}")]
        public IActionResult GetCartById(int id)
        {
            var cart = _unitofWork.Cart.GetFirstOrDefault(c => c.CartId == id, IncludeWord: "CartItems");
            if (cart == null)
            {
                return NotFound();
            }

            var cartDto = new CartReadDto
            {
                CartId = cart.CartId,
                UserId = cart.UserId,
                CartItems = cart.CartItems.Select(ci => new CartItemReadDto
                {
                    CartItemId = ci.CartItemId,
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity
                }).ToList()
            };

            return Ok(cartDto);
        }



        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartReadDto>>> GetCarts()
        {
            var carts = await _unitofWork.Cart.GetAllAsync(IncludeWord: "CartItems");

            var cartDtos = carts.Select(cart => new CartReadDto
            {
                CartId = cart.CartId,
                UserId = cart.UserId,
                CartItems = cart.CartItems.Select(ci => new CartItemReadDto
                {
                    CartItemId = ci.CartItemId,
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity
                    
                }).ToList()
            }).ToList();

            return Ok(cartDtos);
        }

        [HttpPut]
        public IActionResult UpdateCart(int id, CartUpdateDto cart)
        {
            if (id != cart.CartId)
            {
                return BadRequest("Cart ID mismatch.");
            }

            var existingCart = _unitofWork.Cart.GetFirstOrDefault(x => x.CartId == id, IncludeWord: "CartItems");
            if (existingCart == null)
            {
                return NotFound("Cart not found.");
            }

            existingCart.UserId = cart.UserId;

            // Update or add cart items
            foreach (var itemDto in cart.CartItems)
            {
                var cartItem = existingCart.CartItems.FirstOrDefault(ci => ci.CartItemId == itemDto.CartItemId);
                if (cartItem != null)
                {
                    // Update existing cart item
                    cartItem.ProductId = itemDto.ProductId;
                    cartItem.Quantity = itemDto.Quantity;
                }
                else
                {
                    // Add new cart item
                    existingCart.CartItems.Add(new CartItem
                    {
                        CartItemId = itemDto.CartItemId,
                        ProductId = itemDto.ProductId,
                        Quantity = itemDto.Quantity,
                        CartId = existingCart.CartId
                    });
                }
            }

            _unitofWork.Cart.Update(existingCart);
            _unitofWork.Complete();

            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteCart(CartDeleteDto cartDeleteDto)
        {
            try
            {
                var cart = _unitofWork.Cart.GetFirstOrDefault(x => x.CartId == cartDeleteDto.Id, IncludeWord: "CartItems");
                if (cart == null)
                {
                    NotFound();
                }
                foreach (var cartItem in cart.CartItems.ToList())
                {
                    _unitofWork.CartItem.Remove(cartItem);
                }

                // Remove the cart itself
                _unitofWork.Cart.Remove(cart);
                _unitofWork.Complete();

                return Ok();
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }

        }


        [HttpPost("checkout/{cartId}")]
        public IActionResult Checkout(int cartId)
        {
            // Retrieve the cart with its items
            var cart = _unitofWork.Cart.GetFirstOrDefault(c => c.CartId == cartId, IncludeWord: "CartItems.Product");
            if (cart == null || !cart.CartItems.Any())
            {
                return BadRequest("Cart is empty or does not exist.");
            }

            // Create the order
            var order = new Order
            {
                UserId = cart.UserId,
                OrderDate = DateTime.Now,
                TotalAmount = cart.CartItems.Sum(ci => ci.Product.Price * ci.Quantity),
                OrderStatus = "Pending",
                OrderItems = cart.CartItems.Select(ci => new OrderItem
                {
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity,
                    UnitPrice = ci.Product.Price
                }).ToList()
            };

            _unitofWork.Order.Add(order);

            // Optionally, clear the cart or its items after checkout
            _unitofWork.Cart.Remove(cart);
            // Or clear items: _unitOfWork.CartItem.RemoveRange(cart.CartItems);

            _unitofWork.Complete();

            return Ok(new { orderId = order.OrderId, message = "Checkout completed successfully." });
        }
    }
}
