namespace DomainService.Objects.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SKU { get; set; }
        public int WarehouseId { get; set; }
        public int Quantity { get; set; }
    }
}