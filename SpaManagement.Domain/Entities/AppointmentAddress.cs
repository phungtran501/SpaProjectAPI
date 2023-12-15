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
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Fullname { get; set; }

        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
