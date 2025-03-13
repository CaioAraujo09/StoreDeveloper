using System;

namespace SalesManagement.Application.DTOs
{
    public class SaleItemDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
