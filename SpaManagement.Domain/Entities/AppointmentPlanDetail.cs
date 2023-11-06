using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SpaManagement.Domain.Entities
{
    public class AppointmentPlanDetail: BaseEntity
    {
        public string? Note { get; set; }
        public int PlanId { get; set; }
        [ForeignKey(nameof(PlanId))]
        public Plan Plan { get; set; }
        public int AppointmentId { get; set; }
        [ForeignKey(nameof(AppointmentId))]
        public Appointment Appointment { get; set; }
    }
}
