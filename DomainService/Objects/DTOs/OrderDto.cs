using System;
using System.Collections.Generic;

namespace DomainService.Objects.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderItemDto> Items { get; set; }
    }
}