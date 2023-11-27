﻿namespace SpaManagement.Service.DTOs.Product
{
    public class ProductDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Decription { get; set; }
        public bool IsActive { get; set; }
        public double Price { get; set; }
        public DateTime CreateOn { get; set; }
        public int ServiceId { get; set; }

        public string ServiceName { get; set; }

    }
}
