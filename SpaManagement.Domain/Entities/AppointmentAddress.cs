using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaManagement.Domain.Entities
{
    public class AppointmentAddress: BaseEntity
    {
        public int AppointmentId { get; set; }
        [ForeignKey(nameof(AppointmentId))]

        public Appointment Appointment { get; set; }

        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Fullname { get; set; }
    }
}
