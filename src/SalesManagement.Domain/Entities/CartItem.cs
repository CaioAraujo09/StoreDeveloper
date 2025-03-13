﻿namespace SalesManagement.Domain.Entities;

public class CartItem
{
    public Guid Id { get; private set; }
    public Guid CartId { get; private set; }
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }
    public Product Product { get; private set; }
    public Cart Cart { get; private set; }

    private CartItem() { }

    public CartItem(Guid productId, int quantity)
    {
        Id = Guid.NewGuid();
        ProductId = productId; 
        Quantity = quantity;
    }
}