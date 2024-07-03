using ECommerceShop.DataAccess.Implementation;
using ECommerceShop.Entities.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {

        private IUnitofWork _unitofWork;
        public OrderItemController(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;

        }
        [HttpDelete("order/{orderId}/orderItem/{orderItemId}")]
        public async Task<IActionResult> DeleteOrderItem(int orderId, int orderItemId)
        {
            var order = await _unitofWork.Order.GetFirstOrDefaultAsync(o => o.OrderId == orderId, "OrderItems");
            if (order == null)
            {
                return NotFound("Order not found.");
            }

            var orderItem = order.OrderItems.FirstOrDefault(oi => oi.OrderItemId == orderItemId);
            if (orderItem == null)
            {
                return NotFound("Order item not found.");
            }

            _unitofWork.OrderItem.Remove(orderItem);
            _unitofWork.Complete();

            return Ok();
        }
    }
}
