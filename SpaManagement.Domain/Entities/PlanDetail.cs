using System.ComponentModel.DataAnnotations.Schema;

namespace SpaManagement.Domain.Entities
{
    public class PlanDetail: BaseEntity
    {
        public string? Note { get; set; }
        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }
        public int PlanId { get; set; }
        [ForeignKey(nameof(PlanId))]
        public Plan Plan { get; set; }
    }
}
