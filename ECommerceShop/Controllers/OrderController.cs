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
    public class OrderController : ControllerBase
    {
        private IUnitofWork _unitofWork;
        public OrderController(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;

        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody] OrderCreateDto orderDto)
        {
            if (orderDto == null)
            {
                return BadRequest("Order data is required.");
            }

            try
            {
                var orderItems = orderDto.OrderItems.Select(item => new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                }).ToList();

                var totalAmount = orderItems.Sum(item => item.Quantity * item.UnitPrice);

                var order = new Order
                {
                    UserId = orderDto.Id,
                    OrderDate = orderDto.OrderDate,
                    OrderStatus = orderDto.OrderStatus,
                    ShippingAddress = orderDto.ShippingAddress,
                    TotalAmount = totalAmount,
                    OrderItems = orderItems
                };

                _unitofWork.Order.Add(order);
                _unitofWork.Complete();

                return Ok(order.OrderId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    


        [HttpGet("{id}")]
        public async Task<ActionResult<OrderReadDto>> GetOrder(int id)
        {
            var order = await _unitofWork.Order.GetFirstOrDefaultAsync(o => o.OrderId == id, IncludeWord:"OrderItems");
            if (order == null)
            {
                return NotFound("Order not found.");
            }

            var orderDto = new OrderReadDto
            {
                OrderId = order.OrderId,
                Id = order.UserId,
                OrderDate = order.OrderDate,
                OrderItems = order.OrderItems.Select(item => new OrderItemReadDto
                {
                    OrderItemId = item.OrderItemId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                }).ToList()
            };

            return Ok(orderDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] OrderUpdateDto orderDto)
        {
            if (id != orderDto.OrderId)
            {
                return BadRequest("Order ID mismatch.");
            }

            var existingOrder = await _unitofWork.Order.GetFirstOrDefaultAsync(o => o.OrderId == id, "OrderItems");
            if (existingOrder == null)
            {
                return NotFound("Order not found.");
            }

            existingOrder.UserId = orderDto.Id;
            existingOrder.OrderDate = orderDto.OrderDate;

            // Update or add order items
            foreach (var itemDto in orderDto.OrderItems)
            {
                var orderItem = existingOrder.OrderItems.FirstOrDefault(oi => oi.OrderItemId == itemDto.OrderItemId);
                if (orderItem != null)
                {
                    // Update existing order item
                    orderItem.ProductId = itemDto.ProductId;
                    orderItem.Quantity = itemDto.Quantity;
                    orderItem.UnitPrice = itemDto.UnitPrice;
                }
                else
                {
                    // Add new order item
                    existingOrder.OrderItems.Add(new OrderItem
                    {
                        ProductId = itemDto.ProductId,
                        Quantity = itemDto.Quantity,
                        UnitPrice = itemDto.UnitPrice,
                        OrderId = existingOrder.OrderId
                    });
                }
            }

            _unitofWork.Order.Update(existingOrder);
            _unitofWork.Complete();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _unitofWork.Order.GetFirstOrDefaultAsync(o => o.OrderId == id, "OrderItems");
            if (order == null)
            {
                return NotFound("Order not found.");
            }

            // Remove all order items associated with this order
            foreach (var orderItem in order.OrderItems.ToList())
            {
                _unitofWork.OrderItem.Remove(orderItem);
            }

            // Remove the order itself
            _unitofWork.Order.Remove(order);
            _unitofWork.Complete();

            return Ok();
        }
    }
}
