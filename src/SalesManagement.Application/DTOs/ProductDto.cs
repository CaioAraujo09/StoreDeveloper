﻿namespace SalesManagement.Application.DTOs
{
    public class ProductDto
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }
        public RatingDto Rating { get; set; }
    }

    public class RatingDto
    {
        public decimal Rate { get; set; }
        public int Count { get; set; }
    }
}
