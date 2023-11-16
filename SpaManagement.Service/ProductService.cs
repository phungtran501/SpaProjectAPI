using Dapper;
using Microsoft.AspNetCore.Identity;
using SpaManagement.Data;
using SpaManagement.Data.Abstract;
using SpaManagement.Domain.Entities;
using SpaManagement.Domain.Enums;
using SpaManagement.Service.Abstracts;
using SpaManagement.Service.DTOs;
using System.Data;
using System.Diagnostics;


namespace SpaManagement.Service
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDapperHelper _dapperHelper;

        public ProductService(IUnitOfWork unitOfWork, IDapperHelper dapperHelper)
        {
            _unitOfWork = unitOfWork;
            _dapperHelper = dapperHelper;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProduct(int pageIndex, int pageSize)
        {
            var products = await _unitOfWork.ProductRepository.GetData(x => x.IsActive);

            DynamicParameters dynamicParameters = new DynamicParameters();

            dynamicParameters.Add("pageIndex", pageIndex, DbType.Int32, ParameterDirection.Input);
            dynamicParameters.Add("pageSize", pageSize, DbType.Int32, ParameterDirection.Input);
            dynamicParameters.Add("totalRecord", 0, DbType.Int32, ParameterDirection.Output);

            var result = await _dapperHelper.ExcuteStoreProcedureReturnList<ProductDTO>("GetAllProducts", dynamicParameters);

            var total = dynamicParameters.Get<int>("totalRecord");


            var data = result.Select(x => new ProductDTO
            {
                Id = x.Id,
                Name = x.Name,
                CreateOn = x.CreateOn,
                Description = x.Description,
                Price = x.Price,
                ServiceName = x.ServiceName,
                IsActive = x.IsActive
            }).ToArray();

            return data;
        }

        public async Task<ProductDTO> GetProductById(int id)
        {

            var product =  _unitOfWork.ProductRepository.Table.Where(x => x.Id == id).Join(_unitOfWork.ServicesRepository.Table,
                prod => prod.ServicesId,
                ser => ser.Id,
                (product, service) => new ProductDTO
                {
                    Id = product.Id,
                    ServiceId = service.Id,
                    Name = product.Name,
                    Description = product.Decription,
                    Price = product.Price,
                    CreateOn = product.CreateOn,
                    IsActive = product.IsActive
                }).First();


            return product;
        }

        public async Task<ResponseModel> CreateUpdate(ProductModel productModel)
        {
            var product = await _unitOfWork.ProductRepository.GetSingleByConditionAsync(x => x.Name.ToLower() == productModel.Name.ToLower()
                                                                                   && x.Id != productModel.Id);

            if (product is not null)
            {
                return new ResponseModel
                {
                    Status = false,
                    Message = "Product name is exist",
                    StatusType = StatusType.Fail,
                    Action = productModel.Id is null ? ActionType.Insert : ActionType.Update
                };
            }
            if (productModel.Id == 0)
            {
                var pro = new Product
                {
                    ServicesId = productModel.ServiceId,
                    Name = productModel.Name,
                    Decription = productModel.Description,
                    Price = productModel.Price,
                    CreateOn = DateTime.Now,
                    IsActive = true
                };

                var result = _unitOfWork.ProductRepository.Insert(pro);

            }
            //update
            else
            {
                var pro = await _unitOfWork.ProductRepository.GetById(productModel.Id);

                pro.ServicesId = productModel.ServiceId;
                pro.Name = productModel.Name;
                pro.Decription = productModel.Description;
                pro.Price = productModel.Price;
                pro.CreateOn = DateTime.Now;

                _unitOfWork.ProductRepository.Update(pro);

            }
            await _unitOfWork.ProductRepository.Commit();

            return new ResponseModel
            {
                Status = true,
                Message = productModel.Id is null ? "Insert successful" : "Update successful",
                StatusType = StatusType.Success,
                Action = productModel.Id is null ? ActionType.Insert : ActionType.Update
            };

        }

        public async Task DeleteProduct(int productId)
        {
            var product = await _unitOfWork.ProductRepository.GetById(productId);
            product.IsActive = false;
            _unitOfWork.ProductRepository.Update(product);
            await _unitOfWork.ProductRepository.Commit();
        }
    }
}
