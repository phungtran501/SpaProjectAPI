using System.ComponentModel.DataAnnotations.Schema;

namespace SpaManagement.Domain.Entities
{
    public class AppointmentProductDetail: BaseEntity
    {
        public double Price { get; set; }
        public int AppointmentId { get; set; }
        [ForeignKey(nameof(AppointmentId))]
        public Appointment Appointment { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
