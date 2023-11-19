using SpaManagement.Service.DTOs;
using SpaManagement.Service.DTOs.Product;

namespace SpaManagement.Service.Abstracts
{
    public interface IProductService
    {
        Task<ProductResponse> AllProductPagination(int pageIndex = 1, int pageSize = 10);
        Task<ResponseModel> CreateUpdate(ProductModel productModel);
        Task DeleteProduct(int productId);
        Task<IEnumerable<ProductDTO>> GetAllProduct(int pageIndex, int pageSize);
        Task<ProductDTO> GetProductById(int id);
        Task<IEnumerable<ProductModel>> GetProductByService(int id);
        Task<IEnumerable<ProductDTO>> GetRandomProduct();
    }
}