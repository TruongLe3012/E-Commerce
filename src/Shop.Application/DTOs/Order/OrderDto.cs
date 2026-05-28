namespace Shop.Application.DTOs.Order
{
    public class OrderDto
    {
        public int Id { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTime CreatedAt { get; set; }

        public string ShippingAddress { get; set; }
            = string.Empty;

        public string Status { get; set; }
            = string.Empty;

        public List<OrderItemDto> Items
        { get; set; }
            = new();
    }
}