using MSA.Common.Contracts.Domain;

namespace MSA.OrderService.Domain
{
    public class Order : IEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string OrderStatus { get; set; } = string.Empty;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}