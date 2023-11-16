using SpaManagement.Domain.Entities;
using SpaManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaManagement.Service.DTOs
{
    public class AppointmentDTO
    {
        public int? Id { get; set; }
        public string? Note { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Status { get; set; }
        public string UserName { get; set; }
    }
}
