using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaManagement.Service.DTOs
{
    public class PlanDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public double Price { get; set; }
        public DateTime CreateOn { get; set; }
    }
}
