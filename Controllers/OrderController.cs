using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using REST_API_Shashin.Classes;
using REST_API_Shashin.Models;

namespace REST_API_Shashin.Controllers
{
    [Route("/order")]
    public class OrderController : Controller
    {
        DBManager dbManager = new DBManager();

        [HttpPost]
        public ActionResult CreateOrder([FromQuery] string token, [FromBody] CreateOrderDto orderDto)
        {
            try
            {
                if (string.IsNullOrEmpty(token))
                {
                    return Unauthorized("Token is required");
                }

                if (token.StartsWith("Bearer "))
                {
                    token = token.Substring("Bearer ".Length);
                }

                var userId = JwtToken.GetUserIdFromToken(token);
                if (userId == null)
                {
                    return Unauthorized("Invalid token");
                }

                if (orderDto == null)
                {
                    return BadRequest("Order data is required");
                }

                Order newOrder = new Order
                {
                    address = orderDto.address,
                    date = orderDto.date
                };

                dbManager.Orders.Add(newOrder);
                dbManager.SaveChanges();

                foreach (var item in orderDto.dishes)
                {
                    OrderItem orderItem = new OrderItem
                    {
                        orderId = newOrder.orderId,
                        dishId = item.dishId,
                        count = item.count
                    };
                    dbManager.OrderItem.Add(orderItem);
                }
                dbManager.SaveChanges();

                return Ok(newOrder);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet]
        [Route("/history")]
        public ActionResult GetAllOrders()
        {
            try
            {
                
                var orders = dbManager.Orders.ToList();

                var result = new List<object>();

                foreach (var order in orders)
                {
                    var orderItems = dbManager.OrderItem
                        .Where(oi => oi.orderId == order.orderId)
                        .ToList();

                    var dishes = new List<object>();

                    foreach (var item in orderItems)
                    {
                        var dish = dbManager.Dishes
                            .FirstOrDefault(d => d.dishId == item.dishId);

                        dishes.Add(new
                        {
                            dishId = item.dishId,
                            orderId = item.orderId,
                            nameDish = dish?.nameDish,
                            count = item.count,
                            price = dish?.price
                        });
                    }

                    result.Add(new
                    {
                        order.orderId,
                        order.address,
                        order.date,
                        dishes = dishes
                    });
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
