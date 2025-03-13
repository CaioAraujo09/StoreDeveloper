using SalesManagement.Application.DTOs;
using SalesManagement.Domain.Entities;
using SalesManagement.Domain.Events;
using SalesManagement.Domain.Interfaces;

namespace SalesManagement.Application.Services;

public class SaleService
{
    private readonly ISaleRepository _saleRepository;
    private readonly IRegisteredUserRepository _userRepository;
    private readonly IBranchRepository _branchRepository;
    private readonly IEventPublisher _eventPublisher;


    public SaleService(ISaleRepository saleRepository, IRegisteredUserRepository userRepository, 
        IBranchRepository branchRepository, IEventPublisher eventPublisher)
    {
        _saleRepository = saleRepository;
        _eventPublisher = eventPublisher;
        _userRepository = userRepository;
        _branchRepository = branchRepository;

    }
    public async Task<Guid> CreateSaleAsync(Guid registeredUserId, string saleNumber, DateTime date, Guid branchId, List<SaleItemDto> itemDtos)
    {
        var registeredUser = await _userRepository.GetByIdAsync(registeredUserId);
        if (registeredUser == null)
            throw new KeyNotFoundException("Usuário registrado não encontrado.");

        var branch = await _branchRepository.GetByIdAsync(branchId);
        if (branch == null)
            throw new KeyNotFoundException("Filial (Branch) não encontrada.");

        var existingSale = await _saleRepository.GetBySaleNumberAsync(saleNumber);
        if (existingSale != null)
            throw new InvalidOperationException($"Já existe uma venda com o número '{saleNumber}'.");

        var items = itemDtos?.Select(dto => new SaleItem(dto.ProductId, dto.Quantity, dto.UnitPrice)).ToList() ?? new List<SaleItem>();
        var sale = Sale.CreateSale(saleNumber, registeredUser, branch, date, items); 

        try
        {
            await _saleRepository.AddAsync(sale);
            _eventPublisher.Publish(new SaleCreatedEvent(sale.Id, sale.TotalAmount, sale.Items.ToList()));
            return sale.Id;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao salvar venda: {ex.InnerException?.Message ?? ex.Message}");
            throw new Exception($"Erro ao salvar venda: {ex.InnerException?.Message ?? ex.Message}");
        }
    }

    public async Task CancelSaleItemAsync(Guid saleId, Guid itemId)
    {
        var sale = await _saleRepository.GetByIdAsync(saleId);
        if (sale == null)
            throw new KeyNotFoundException("Venda não encontrada.");

        if (sale.IsCancelled)
            throw new InvalidOperationException("Não é possível cancelar um item de uma venda já cancelada.");

        var item = sale.Items.FirstOrDefault(i => i.Id == itemId);
        if (item == null)
            throw new KeyNotFoundException("Item não encontrado na venda.");

        sale.RemoveItem(item); 

        await _saleRepository.UpdateAsync(sale);
        _eventPublisher.Publish(new ItemCancelledEvent(saleId, itemId));
    }

    public async Task CancelSaleAsync(Guid saleId)
    {
        var sale = await _saleRepository.GetByIdAsync(saleId);
        if (sale == null)
            throw new KeyNotFoundException("Venda não encontrada.");

        if (sale.IsCancelled)
            throw new InvalidOperationException("A venda já está cancelada.");

        sale.Cancel(); 

        await _saleRepository.UpdateAsync(sale);
        _eventPublisher.Publish(new SaleCancelledEvent(sale.Id));
    }


    public async Task<Sale?> GetSaleByIdAsync(Guid saleId)
    {
        return await _saleRepository.GetByIdAsync(saleId);
    }
}
