using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.DTOs.Product;
using Shop.Application.Interfaces;
using Shop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Shop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IGenericRepository<Product> _repository;

        private readonly IMapper _mapper; 
        
        private readonly IMemoryCache _cache;

        private readonly ILogger<ProductsController> _logger;

        public ProductsController(
        IGenericRepository<Product> repository,
        IMapper mapper,
        IMemoryCache cache,
        ILogger<ProductsController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _cache = cache;
            _logger = logger;
        }

        [HttpGet]
        [HttpGet]
        public async Task<IActionResult> GetAll(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? search = null,
        [FromQuery] int? categoryId = null)
        {
            var cacheKey =
                $"products_{page}_{pageSize}_{search}_{categoryId}";

            if (_cache.TryGetValue(
                cacheKey,
                out object? cachedProducts))
            {
                return Ok(cachedProducts);
            }

            var query =
                _repository.GetQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(x =>
                    x.Name.ToLower()
                        .Contains(search.ToLower()));
            }

            if (categoryId.HasValue)
            {
                query = query.Where(x =>
                    x.CategoryId ==
                    categoryId.Value);
            }

            var totalItems =
                await query.CountAsync();

            var products =
                await query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

            var result =
                _mapper.Map<IEnumerable<ProductDto>>(
                    products);

            var response = new
            {
                Page = page,
                PageSize = pageSize,
                TotalItems = totalItems,
                Data = result
            };

            var cacheOptions =
                new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(
                        TimeSpan.FromMinutes(5));

            _cache.Set(
                cacheKey,
                response,
                cacheOptions);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(
            int id)
        {
            var product =
                await _repository.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            var result =
                _mapper.Map<ProductDto>(product);

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(
            CreateProductDto dto)
        {
            var product =
                _mapper.Map<Product>(dto);

            await _repository.CreateAsync(product);

            await _repository.SaveChangesAsync();

            return Ok(product);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            UpdateProductDto dto)
        {
            var product =
                await _repository.GetByIdAsync(id);

            if (product == null)
            {
                _logger.LogWarning(
                "Not found product");
                return NotFound();
            }

            _mapper.Map(dto, product);

            _repository.Update(product);

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
                _logger.LogWarning(
                "Not found product");
                return NotFound();
            }

            _repository.Delete(product);

            await _repository.SaveChangesAsync();

            return NoContent();
        }
    }
}