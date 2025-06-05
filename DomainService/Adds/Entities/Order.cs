using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.DomainService.Entities
{
    [Table("orders")]
    public class Order
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        [Column("SupplierId")]
        public int SupplierId { get; set; }

        [Required]
        [Column("Date")]
        public DateTime Date { get; set; }

        [Required]
        [Column("TotalAmount")]
        public decimal TotalAmount { get; set; }

        // Навигационные свойства
        [ForeignKey("SupplierId")]
        public virtual Supplier Supplier { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}