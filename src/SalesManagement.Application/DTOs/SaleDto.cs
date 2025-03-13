using System;
using System.Collections.Generic;

namespace SalesManagement.Application.DTOs
{
    public class SaleDto
    {
        public string SaleNumber { get; set; }
        public DateTime Date { get; set; }
        public Guid RegisteredUserId { get;  set; }
        public decimal TotalAmount { get; set; }
        public Guid BranchId { get; set; }
        public bool IsCancelled { get; set; }


        private List<SaleItemDto> _items = new();
        public List<SaleItemDto> Items
        {
            get => _items ??= new();
            set => _items = value ?? new();
        }
    }
}
