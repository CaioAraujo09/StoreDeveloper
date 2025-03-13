using Microsoft.AspNetCore.Mvc;
using SalesManagement.Application.Services;
using SalesManagement.Application.DTOs;
using SalesManagement.API.Helpers;

namespace SalesManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BranchController : ControllerBase
    {
        private readonly BranchService _branchService;

        public BranchController(BranchService branchService)
        {
            _branchService = branchService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BranchDto>>> GetAll()
        {
            var branches = await _branchService.GetAllAsync();
            return Ok(branches);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BranchDto>> GetById(Guid id)
        {
            var branch = await _branchService.GetByIdAsync(id);
            return branch == null ? NotFound("Filial não encontrada.") : Ok(branch);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BranchDto branchDto)
        {
            await _branchService.AddAsync(branchDto);
            return CreatedAtAction(nameof(GetById), new { id = branchDto.Id }, branchDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] BranchDto branchDto)
        {
            try
            {
                await _branchService.UpdateAsync(id, branchDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return ExceptionHandler.HandleException(ex, "Erro ao atualizar Branch.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _branchService.DeleteAsync(id);
            return NoContent();
        }
    }
}
