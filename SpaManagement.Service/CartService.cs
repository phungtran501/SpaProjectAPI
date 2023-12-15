using SpaManagement.Data.Abstract;
using SpaManagement.Domain.Entities;
using SpaManagement.Service.Abstracts;
using SpaManagement.Service.DTOs.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaManagement.Service
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CartItemModel>> GetItemCart(List<CartDTO> cartDTOs)
        {
            List<CartItemModel> cartItemModels = new();

            var lsProductId = cartDTOs.Select(x => x.Id).ToList();

            var products = await _unitOfWork.ProductRepository.GetData(x => lsProductId.Contains(x.Id));


            //cartItemModels = products.Select(product =>
            //{
            //    var currentProduct = cartDTOs.FirstOrDefault(x => x.Id == product.Id);

            //    return new CartItemModel
            //    {
            //        Name = product.Name,
            //        Id = product.Id,
            //        Price = product.Price,
            //        Quantity = currentProduct.Quantity
            //    };
            //}).ToList();

            foreach (var item in products)
            {
                var currentProduct = cartDTOs.FirstOrDefault(x => x.Id == item.Id);

                cartItemModels.Add(new CartItemModel
                {
                    Name = item.Name,
                    Id = item.Id,
                    Price = item.Price,
                    Quantity = currentProduct.Quantity
                });
            }

            return cartItemModels;
        }
    }
}
