using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.DTOs.Category;
using Shop.Application.Interfaces;
using Shop.Domain.Entities;

namespace Shop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly IGenericRepository<Category>
            _repository;

        private readonly IMapper _mapper;

        public CategoriesController(
            IGenericRepository<Category> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories =
                await _repository.GetAllAsync();

            var result =
                _mapper.Map<IEnumerable<CategoryDto>>(
                    categories);

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(
            CreateCategoryDto dto)
        {
            var category =
                _mapper.Map<Category>(dto);

            await _repository.CreateAsync(category);

            await _repository.SaveChangesAsync();

            return Ok(category);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            UpdateCategoryDto dto)
        {
            var category =
                await _repository.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            _mapper.Map(dto, category);

            _repository.Update(category);

            await _repository.SaveChangesAsync();

            return Ok(category);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            int id)
        {
            var category =
                await _repository.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            _repository.Delete(category);

            await _repository.SaveChangesAsync();

            return NoContent();
        }
    }
}