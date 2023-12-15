using SpaManagement.Service.DTOs.Cart;

namespace SpaManagement.Service.Abstracts
{
    public interface ICartService
    {
        Task<IEnumerable<CartItemModel>> GetItemCart(List<CartDTO> cartDTOs);
    }
}