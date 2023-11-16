using SpaManagement.Service.DTOs;

namespace SpaManagement.Service.Abstracts
{
    public interface IProductService
    {
        Task<ResponseModel> CreateUpdate(ProductModel productModel);
        Task DeleteProduct(int productId);
        Task<IEnumerable<ProductDTO>> GetAllProduct(int pageIndex, int pageSize);
        Task<ProductDTO> GetProductById(int id);
    }
}