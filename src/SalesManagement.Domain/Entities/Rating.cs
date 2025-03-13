namespace SalesManagement.Domain.Entities
{
    public class Rating
    {
        public Guid Id { get; private set; }
        public Guid ProductId { get; private set; } 
        public decimal Rate { get; private set; }
        public int Count { get; private set; }

        private Rating() { }

        public Rating(Guid productId, decimal rate, int count)
        {
            Id = Guid.NewGuid();
            ProductId = productId;
            Rate = rate;
            Count = count;
        }

        public void Update(decimal rate, int count)
        {
            Rate = rate;
            Count = count;
        }
    }
}
