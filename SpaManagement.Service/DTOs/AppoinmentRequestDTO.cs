using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaManagement.Service.DTOs
{
    public class AppoinmentRequestDTO
    {
        public int? Id { get; set; }
        public string? Note { get; set; }
        public DateTime CreatedOn { get; set; }
        public short Status { get; set; }
        public string UserId { get; set; }
    }
}
