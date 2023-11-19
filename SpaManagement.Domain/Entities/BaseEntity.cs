using System.ComponentModel.DataAnnotations.Schema;

namespace SpaManagement.Domain.Entities
{
    public class BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [System.ComponentModel.DataAnnotations.Required]
        public int Id { get; set; }
    }
}
