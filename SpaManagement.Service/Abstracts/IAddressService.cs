using SpaManagement.Service.DTOs.Cart;

namespace SpaManagement.Service.Abstracts
{
    public interface IAddressService
    {
        Task<int> Save(CheckoutCartDTO checkoutCartDTO);
    }
}