using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainService.Adds.Entities
{
    [Table("suppliers")]
    public class Supplier
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        [Column("Name")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Column("ContactInfo")]
        [MaxLength(255)]
        public string ContactInfo { get; set; }
    }
}