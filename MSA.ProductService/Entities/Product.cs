using MSA.Common.Contracts.Domain;

namespace MSA.ProductService.Entities
{
    public class Product : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}