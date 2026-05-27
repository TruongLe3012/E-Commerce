using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Application.DTOs.Cart;
using Shop.Infrastructure.Data;
using System.Security.Claims;

namespace Shop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly AppDbContext _context;

        private readonly IMapper _mapper;

        public CartController(
            AppDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetMyCart()
        {
            var userId = GetUserId();

            var cartItems =
                await _context.CartItems
                    .Include(x => x.Product)
                    .Where(x => x.UserId == userId)
                    .ToListAsync();

            var result =
                _mapper.Map<List<CartItemDto>>(
                    cartItems);

            return Ok(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart(
            AddCartDto dto)
        {
            var userId = GetUserId();

            var existingCartItem =
                await _context.CartItems
                    .FirstOrDefaultAsync(x =>
                        x.UserId == userId &&
                        x.ProductId == dto.ProductId);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity += dto.Quantity;
            }
            else
            {
                var cartItem =
                    new Domain.Entities.CartItem
                    {
                        UserId = userId,
                        ProductId = dto.ProductId,
                        Quantity = dto.Quantity
                    };

                await _context.CartItems
                    .AddAsync(cartItem);
            }

            await _context.SaveChangesAsync();

            return Ok("Added to cart");
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateCart(
            UpdateCartDto dto)
        {
            var userId = GetUserId();

            var cartItem =
                await _context.CartItems
                    .FirstOrDefaultAsync(x =>
                        x.Id == dto.CartItemId &&
                        x.UserId == userId);

            if (cartItem == null)
            {
                return NotFound();
            }

            cartItem.Quantity = dto.Quantity;

            await _context.SaveChangesAsync();

            return Ok("Cart updated");
        }

        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> Remove(
            int id)
        {
            var userId = GetUserId();

            var cartItem =
                await _context.CartItems
                    .FirstOrDefaultAsync(x =>
                        x.Id == id &&
                        x.UserId == userId);

            if (cartItem == null)
            {
                return NotFound();
            }

            _context.CartItems.Remove(cartItem);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private int GetUserId()
        {
            return int.Parse(
                User.FindFirstValue(
                    ClaimTypes.NameIdentifier)!);
        }
    }
}