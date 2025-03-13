namespace SalesManagement.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public decimal Price { get; private set; }
        public string Description { get; private set; }
        public string Category { get; private set; }
        public string Image { get; private set; }

        public Rating Rating { get; private set; }  

        private Product() { }

        public Product(string title, decimal price, string description, string category, string image)
        {
            Id = Guid.NewGuid();
            Title = title;
            Price = price;
            Description = description;
            Category = category;
            Image = image;
        }

        public void SetRating(Rating rating)
        {
            Rating = rating;
        }

        public void Update(string title, decimal price, string description, string category, string image)
        {
            Title = title;
            Price = price;
            Description = description;
            Category = category;
            Image = image;
        }
    }
}
