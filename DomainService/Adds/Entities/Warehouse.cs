using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.DomainService.Entities
{
    [Table("warehouses")]
    public class Warehouse
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        [Column("Name")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Column("Location")]
        [MaxLength(100)]
        public string Location { get; set; }
    }
}