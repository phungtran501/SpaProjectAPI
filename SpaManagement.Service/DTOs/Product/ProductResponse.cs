namespace SpaManagement.Service.DTOs.Product
{
    public class ProductResponse
    {
        public int TotalPage { get; set; }

        public IEnumerable<ProductDTO>  Products{ get; set; }
    }
}
