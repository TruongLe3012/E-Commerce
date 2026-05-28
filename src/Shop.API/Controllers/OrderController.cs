using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Application.DTOs.Order;
using Shop.Domain.Entities;
using Shop.Infrastructure.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Shop.Application.DTOs.Order;

namespace Shop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _context;

        private readonly IMapper _mapper;

        public OrdersController(
            AppDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout(
            CheckoutDto dto)
        {
            var userId = GetUserId();

            var cartItems =
                await _context.CartItems
                    .Include(x => x.Product)
                    .Where(x => x.UserId == userId)
                    .ToListAsync();

            if (!cartItems.Any())
            {
                return BadRequest(
                    "Cart is empty");
            }

            await using var transaction =
                await _context.Database
                    .BeginTransactionAsync();

            try
            {
                var totalPrice =
                    cartItems.Sum(x =>
                        x.Product.Price * x.Quantity);

                var order = new Order
                {
                    UserId = userId,
                    TotalPrice = totalPrice,
                    ShippingAddress =
                        dto.ShippingAddress,
                    CreatedAt = DateTime.UtcNow,
                    Status = "Pending"
                };

                await _context.Orders
                    .AddAsync(order);

                await _context.SaveChangesAsync();

                var orderItems =
                    cartItems.Select(x =>
                        new OrderItem
                        {
                            OrderId = order.Id,
                            ProductId = x.ProductId,
                            Quantity = x.Quantity,
                            Price = x.Product.Price
                        }).ToList();

                await _context.OrderItems
                    .AddRangeAsync(orderItems);

                _context.CartItems
                    .RemoveRange(cartItems);

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                var result =
                    _mapper.Map<OrderDto>(order);

                return Ok(result);
            }
            catch
            {
                await transaction.RollbackAsync();

                return StatusCode(
                    500,
                    "Checkout failed");
            }
        }

        private int GetUserId()
        {
            return int.Parse(
                User.FindFirstValue(
                    ClaimTypes.NameIdentifier)!);
        }

        [HttpGet("my-orders")]
        public async Task<IActionResult> GetMyOrders()
        {
            var userId = GetUserId();

            var orders =
                await _context.Orders
                    .Include(x => x.OrderItems)
                    .ThenInclude(x => x.Product)
                    .Where(x => x.UserId == userId)
                    .OrderByDescending(x => x.CreatedAt)
                    .ToListAsync();

            var result =
                _mapper.Map<List<OrderDto>>(orders);

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders =
                await _context.Orders
                    .Include(x => x.OrderItems)
                    .ThenInclude(x => x.Product)
                    .OrderByDescending(x => x.CreatedAt)
                    .ToListAsync();

            var result =
                _mapper.Map<List<OrderDto>>(orders);

            return Ok(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("status")]
        public async Task<IActionResult> UpdateStatus(UpdateOrderStatusDto dto)
        {
            var order =
                await _context.Orders
                    .FirstOrDefaultAsync(x =>
                        x.Id == dto.OrderId);

            if (order == null)
            {
                return NotFound();
            }

            order.Status = dto.Status;

            await _context.SaveChangesAsync();

            return Ok("Order status updated");
        }
    }
}