using Microsoft.AspNetCore.Mvc;
using SalesManagement.Application.DTOs;
using SalesManagement.Application.Services;

namespace SalesManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisteredUsersController : ControllerBase
    {
        private readonly RegisteredUserService _service;

        public RegisteredUsersController(RegisteredUserService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RegisteredUserDto dto)
        {
            await _service.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll( [FromQuery] int _page = 1,[FromQuery] int _size = 10,[FromQuery] string _order = "username asc",[FromQuery] Dictionary<string, string> filters = null)
        {
            var users = await _service.GetAllAsync(_page, _size, _order);
            return Ok(new
            {
                data = users,
                totalItems = users.Count(),
                currentPage = _page,
                totalPages = (int)Math.Ceiling(users.Count() / (double)_size)
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _service.GetByIdAsync(id);
            return user == null ? NotFound() : Ok(user);
        }
     

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] RegisteredUserDto dto)
        {
            await _service.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
