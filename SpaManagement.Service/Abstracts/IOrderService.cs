using SpaManagement.Service.DTOs;
using SpaManagement.Service.DTOs.Cart;

namespace SpaManagement.Service.Abstracts
{
    public interface IOrderService
    {
        Task<ResponseModel> Save(CheckoutCartDTO checkoutCartDTO, int addressId);
    }
}