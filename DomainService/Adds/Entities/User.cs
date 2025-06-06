using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainService.Adds.Entities
{
    [Table("users")]
    public class User
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        [Column("UserName")]
        [MaxLength(50)]
        public string UserName { get; set; }

        [Required]
        [Column("PasswordHash")]
        [MaxLength(255)]
        public string PasswordHash { get; set; }

        [Required]
        [Column("FullName")]
        [MaxLength(100)]
        public string FullName { get; set; }

        [Required]
        [Column("Role")]
        [MaxLength(20)]
        public string Role { get; set; }
    }
}