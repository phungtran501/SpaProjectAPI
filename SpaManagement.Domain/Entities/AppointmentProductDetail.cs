using System.ComponentModel.DataAnnotations.Schema;

namespace SpaManagement.Domain.Entities
{
    public class AppointmentProductDetail: BaseEntity
    {
        public double Price { get; set; }
        public bool Available { get; set; }
        public string? Note { get; set; }
        public int AppointmentId { get; set; }
        [ForeignKey(nameof(AppointmentId))]
        public Appointment Appointment { get; set; }
        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }
    }
}
