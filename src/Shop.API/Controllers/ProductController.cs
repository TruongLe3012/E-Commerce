using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.DTOs.Product;
using Shop.Application.Interfaces;
using Shop.Domain.Entities;

namespace Shop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IGenericRepository<Product>
            _repository;

        public ProductController(
            IGenericRepository<Product> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products =
                await _repository.GetAllAsync();

            return Ok(products);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(
            CreateProductDto dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Stock = dto.Stock,
                CategoryId = dto.CategoryId
            };

            await _repository.CreateAsync(product);

            await _repository.SaveChangesAsync();

            return Ok(product);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            int id)
        {
            var product =
                await _repository.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            _repository.Delete(product);

            await _repository.SaveChangesAsync();

            return NoContent();
        }
    }
}