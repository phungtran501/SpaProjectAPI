using SpaManagement.Data.Abstract;
using SpaManagement.Domain.Entities;
using SpaManagement.Domain.Enums;
using SpaManagement.Service.Abstracts;
using SpaManagement.Service.DTOs;
using SpaManagement.Service.DTOs.Cart;
using SpaManagement.Service.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaManagement.Service
{
    public class OrderService: IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseModel> Save(CheckoutCartDTO checkoutCartDTO, int addressId)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                if (checkoutCartDTO is not null)
                {
                    var order = new Appointment
                    {
                        Code = $"OR_{DateTime.Now.ToString("ddMMyyyyhhmmssff")}",
                        CreatedOn = DateTime.Now,
                        AddressId = addressId,
                        AppointmentDate = checkoutCartDTO.AppointmentDate,
                        Note = checkoutCartDTO.Note,
                        Status = StatusAppointment.New
                    };

                    await _unitOfWork.AppointmentRepository.Insert(order);
                    await _unitOfWork.CommitAsync();

                    foreach (var item in checkoutCartDTO.Items)
                    {
                        var productDetail = new AppointmentProductDetail
                        {

                            AppointmentId = order.Id,
                            Price = item.Price,
                            ProductId = item.Id,
                            Quantity = item.Quantity,

                        };

                        await _unitOfWork.AppointmentProductDetailRepository.Insert(productDetail);

                    }
                    await _unitOfWork.CommitAsync();

                    await _unitOfWork.CommitTransactionAsync();

                    return new ResponseModel
                    {
                        Status = true,
                        Message = "Your order is successful",

                    };
                }

                return new ResponseModel
                {
                    Status = false,
                    Message = "No data updated",
                };

            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }


        }


    }
}
