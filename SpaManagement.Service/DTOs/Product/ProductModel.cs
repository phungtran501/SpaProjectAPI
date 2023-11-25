using Microsoft.AspNetCore.Http;

namespace SpaManagement.Service.DTOs.Product
{
    public class ProductModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public double Price { get; set; }
        public DateTime CreateOn { get; set; }
        public int ServiceId { get; set; }
        public IFormFile? ProductImage { get; set; }
    }
}
