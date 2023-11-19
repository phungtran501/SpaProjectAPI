using System.ComponentModel.DataAnnotations;

namespace SpaManagement.Domain.Entities
{
    public class Services : BaseEntity
    {
        [StringLength(200)]
        public string Name { get; set; }
        [StringLength(1000)]
        public string Decription { get; set; }
        public bool IsActive { get; set; }
    }
}
