using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaManagement.Domain.Entities
{
    public class Plan: BaseEntity
    {
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(1000)]
        public string Decription { get; set; }
        public bool IsActive { get; set; }

        public double Price { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
