using SpaManagement.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaManagement.Domain.Entities
{
    public class Appointment: BaseEntity
    {
        public string Code { get; set; }
        public string? Note { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime AppointmentDate { get; set; }
        public StatusAppointment Status { get; set; }
        public int AddressId { get; set; }
        [ForeignKey(nameof(AddressId))]
        public AppointmentAddress AppointmentAddress { get; set; }

    }
}
