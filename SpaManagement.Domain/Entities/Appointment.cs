using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaManagement.Domain.Entities
{
    public class Appointment: BaseEntity
    {
        public string? Note { get; set; }
        public DateTime CreateOn { get; set; }
        public string? Status { get; set; }
        public int CustomerId { get; set; }
        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; }
    }
}
