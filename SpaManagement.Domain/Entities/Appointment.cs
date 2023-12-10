using System.ComponentModel.DataAnnotations.Schema;

namespace SpaManagement.Domain.Entities
{
    public class Appointment: BaseEntity
    {
        public string Code { get; set; }
        public string? Note { get; set; }
        public DateTime CreatedOn { get; set; }

        public DateTime AppointmentDate { get; set; }
        public short Status { get; set; }
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
