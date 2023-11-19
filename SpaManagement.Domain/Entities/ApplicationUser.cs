﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SpaManagement.Domain.Entities
{
    public class ApplicationUser: IdentityUser
    {
        [StringLength(250)]
        public string? Fullname { get; set; }
        [StringLength(500)]
        public string? Address { get; set; }
        public bool IsSystem { get; set; }
        public bool IsActive { get; set; }
    }
}
