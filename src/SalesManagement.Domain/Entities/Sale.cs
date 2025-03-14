﻿using SalesManagement.Domain.Entities;

public class Sale
{
    public Guid Id { get; private set; }
    public string SaleNumber { get; private set; }
    public DateTime Date { get; private set; }
    public Guid RegisteredUserId { get; private set; }
    public RegisteredUser RegisteredUser { get; private set; }
    public decimal TotalAmount { get; private set; }
    public bool IsCancelled { get; private set; }
    public Guid BranchId { get; private set; }
    public Branch Branch { get; private set; }

    private List<SaleItem> _items = new();
    public IReadOnlyCollection<SaleItem> Items => _items;

    private Sale() { }

    public Sale(string saleNumber, RegisteredUser registeredUser, Branch branch, DateTime date, List<SaleItem> items)
    {
        Id = Guid.NewGuid();
        SaleNumber = saleNumber ?? throw new ArgumentNullException(nameof(saleNumber));
        RegisteredUser = registeredUser ?? throw new ArgumentNullException(nameof(registeredUser));
        RegisteredUserId = registeredUser.Id;
        Branch = branch ?? throw new ArgumentNullException(nameof(branch));
        BranchId = branch.Id;
        Date = date;

        _items = items ?? new List<SaleItem>();
        CalculateTotalAmount();
    }
    public void CalculateTotalAmount()
    {
        TotalAmount = _items.Sum(item => (item.UnitPrice * item.Quantity) - item.Discount);
    }

    public void Cancel()
    {
        if (IsCancelled)
            throw new InvalidOperationException("A venda já está cancelada.");

        IsCancelled = true;
    }

    public void RemoveItem(SaleItem item)
    {
        if (!Items.Contains(item))
            throw new InvalidOperationException("O item não pertence a esta venda.");

        _items.Remove(item);
    }

    public static Sale CreateSale(string saleNumber, RegisteredUser registeredUser, Branch branch, DateTime date, List<SaleItem> items)
    {
        if (registeredUser == null)
            throw new ArgumentException("Usuário registrado é obrigatório.");

        if (branch == null)
            throw new ArgumentException("A filial (Branch) é obrigatória.");

        if (string.IsNullOrWhiteSpace(saleNumber))
            throw new ArgumentException("O número da venda (SaleNumber) é obrigatório.");

        if (items == null || items.Count == 0)
            throw new ArgumentException("A venda deve conter pelo menos um item.");

        foreach (var item in items)
        {
            if (item.Quantity > 20)
                throw new InvalidOperationException("Não é possível vender acima de 20 itens idênticos.");

            decimal totalSemDesconto = item.UnitPrice * item.Quantity;
            decimal totalComDesconto = totalSemDesconto; 

            if (item.Quantity >= 10 && item.Quantity <= 20)
            {
                item.Discount = totalSemDesconto * 0.2m;
                totalComDesconto -= item.Discount;
            }
            else if (item.Quantity >= 4)
            {
                item.Discount = totalSemDesconto * 0.1m;
                totalComDesconto -= item.Discount;
            }
            else
            {
                item.Discount = 0; 
            }

            Console.WriteLine($"Produto: {item.ProductId}");
            Console.WriteLine($"Quantidade: {item.Quantity}");
            Console.WriteLine($"Preço sem desconto: {totalSemDesconto:C}");
            Console.WriteLine($"Desconto aplicado: {item.Discount:C}");
            Console.WriteLine($"Preço final com desconto: {totalComDesconto:C}");
            Console.WriteLine("---------------------------------");
        }

        return new Sale(saleNumber, registeredUser, branch, date, items);
    }
}
