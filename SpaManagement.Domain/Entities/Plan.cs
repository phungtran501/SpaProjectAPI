using System.ComponentModel.DataAnnotations;

namespace SpaManagement.Domain.Entities
{
    public class Plan: BaseEntity
    {
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(1000)]
        public string Decription { get; set; }
        public bool IsActive { get; set; }

        public double Price { get; set; }
        public DateTime CreatedOn { get; set; }

        [StringLength(2000)]
        public string URLName { get; set; }
    }
}
