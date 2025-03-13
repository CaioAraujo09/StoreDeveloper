using Microsoft.AspNetCore.Mvc;
using SalesManagement.Application.DTOs;
using SalesManagement.Application.Services;
using SalesManagement.Domain.Entities;
using SalesManagement.API.Helpers;


namespace SalesManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly SaleService _saleService;

        public SalesController(SaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSale([FromBody] SaleDto saleDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var saleId = await _saleService.CreateSaleAsync(
                    saleDto.RegisteredUserId,
                    saleDto.SaleNumber,
                    saleDto.Date,
                    saleDto.BranchId,
                    saleDto.Items
                );

                return CreatedAtAction(nameof(GetSaleById), new { saleId }, new { message = "Venda criada com sucesso.", saleId });
            }
            catch (Exception ex)
            {
                return ExceptionHandler.HandleException(ex, "Erro ao criar a venda.");
            }
        }

        [HttpPut("{saleId}/cancel")]
        public async Task<IActionResult> CancelSale(Guid saleId)
        {
            try
            {
                await _saleService.CancelSaleAsync(saleId);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Erro interno ao cancelar a venda.", details = ex.Message });
            }
        }

        [HttpPut("{saleId}/items/{itemId}/cancel")]
        public async Task<IActionResult> CancelSaleItem(Guid saleId, Guid itemId)
        {
            try
            {
                await _saleService.CancelSaleItemAsync(saleId, itemId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return ExceptionHandler.HandleException(ex, "Erro ao cancelar o item de uma venda.");
            }
        }

        [HttpGet("{saleId}")]
        public async Task<IActionResult> GetSaleById(Guid saleId)
        {
            var sale = await _saleService.GetSaleByIdAsync(saleId);
            return sale == null ? NotFound("Venda não encontrada.") : Ok(sale);
        }
    }
}
