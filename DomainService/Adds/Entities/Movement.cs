using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainService.Adds.Entities
{
    [Table("movements")]
    public class Movement
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        [Column("ProductId")]
        public int ProductId { get; set; }

        [Column("FromWarehouseId")]
        public int? FromWarehouseId { get; set; }

        [Column("ToWarehouseId")]
        public int? ToWarehouseId { get; set; }

        [Required]
        [Column("Quantity")]
        public int Quantity { get; set; }

        [Required]
        [Column("Date")]
        public DateTime Date { get; set; }

        // Навигационные свойства
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [ForeignKey("FromWarehouseId")]
        public virtual Warehouse FromWarehouse { get; set; }

        [ForeignKey("ToWarehouseId")]
        public virtual Warehouse ToWarehouse { get; set; }
    }
}