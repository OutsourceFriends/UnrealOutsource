using System;

namespace DomainService.Objects.DTOs
{
    public class MovementDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int? FromWarehouseId { get; set; }
        public int? ToWarehouseId { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
    }
}