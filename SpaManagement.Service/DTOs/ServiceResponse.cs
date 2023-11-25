using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaManagement.Service.DTOs
{
    public class ServiceResponse
    {
        public int? Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }
        public string Image { get; set; }
    }
}
