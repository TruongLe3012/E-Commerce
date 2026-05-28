namespace Shop.Application.DTOs.Order
{
    public class UpdateOrderStatusDto
    {
        public int OrderId { get; set; }

        public string Status { get; set; }
            = string.Empty;
    }
}