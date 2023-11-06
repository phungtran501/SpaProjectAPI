using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaManagement.Domain.Entities
{
    public class Customer: BaseEntity
    {
        [Required]
        [StringLength(250)]
        public string UserName { get; set; }

        [Required]
        [StringLength(150)]
        public string Password { get; set; }
        [StringLength(150)]
        public string Email { get; set; }
        [StringLength(500)]
        public string? Address { get; set; }
        [StringLength(15)]
        public string Phone { get; set; }
        public bool IsSystem { get; set; }
        [StringLength(500)]
        public string FullName { get; set; }
        public bool IsActive { get; set; }
    }
}
