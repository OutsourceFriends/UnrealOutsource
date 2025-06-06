using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainService.Adds.Entities
{
    [Table("products")]
    public class Product
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        [Column("Name")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [Column("SKU")]
        [MaxLength(50)]
        public string SKU { get; set; }

        [Required]
        [Column("WarehouseId")]
        public int WarehouseId { get; set; }

        [Required]
        [Column("Quantity")]
        public int Quantity { get; set; }

        // Навигационные свойства
        [ForeignKey("WarehouseId")]
        public virtual Warehouse Warehouse { get; set; }
    }
}