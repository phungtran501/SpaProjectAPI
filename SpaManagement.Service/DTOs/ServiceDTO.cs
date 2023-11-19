﻿namespace SpaManagement.Service.DTOs
{
    public class ServiceDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }
        public string? Action { get; set; }

    }
}
