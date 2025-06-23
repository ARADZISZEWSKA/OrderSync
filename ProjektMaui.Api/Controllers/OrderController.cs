using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjektMaui.Api.Data;
using ProjektMaui.Api.Models;
using ProjektMaui.Api.Models.Dto;
using System.Security.Claims;

namespace ProjektMaui.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto dto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var order = new Order
            {
                ProductId = dto.ProductId,
                UserId = userId,
                Notes = dto.Notes,
                ImageUrl = dto.ImageUrl  
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return Ok(order);
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetMyOrders()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return await _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.Product)
                .ToListAsync();
        }

        [HttpGet("admin")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<Order>>> GetAllOrders()
        {
            return await _context.Orders
                .Include(o => o.Product)
                .Include(o => o.User)
                .ToListAsync();
        }

        

      

        [HttpPut("{id}/status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] UpdateStatusDto dto)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return NotFound();

            if (string.IsNullOrWhiteSpace(dto.Status))
                return BadRequest("Status is required.");

            if (!Enum.TryParse<OrderStatus>(dto.Status, true, out var parsedStatus))
                return BadRequest("Invalid status value.");

            order.Status = parsedStatus;
            await _context.SaveChangesAsync();

            return NoContent();
        }





    }

}
