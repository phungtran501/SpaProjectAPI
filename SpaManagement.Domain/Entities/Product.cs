using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SpaManagement.Domain.Entities
{
    public class Product: BaseEntity
    {
        [StringLength(200)]
        public string Name { get; set; }
        [StringLength(1000)]
        public string Decription { get; set; }
        public bool IsActive { get; set; }

        public double Price { get; set; }
        public DateTime CreateOn { get; set; }
        public int ServicesId { get; set; }
        [ForeignKey(nameof(ServicesId))]
        public Services Services { get; set; }

        public string Code { get; set; }
    }
}
