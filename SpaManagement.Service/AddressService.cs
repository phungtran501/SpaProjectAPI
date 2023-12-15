using Microsoft.AspNetCore.Identity;
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
    public class AddressService : IAddressService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public AddressService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<int> Save(CheckoutCartDTO checkoutCartDTO)
        {
            var user = await _userManager.FindByNameAsync(checkoutCartDTO.Username);

            if(user is null)
            {
                throw new Exception("User is not exist");
            }

            if (checkoutCartDTO is not null)
            {
                var userAddress = new AppointmentAddress
                {
                    
                    Fullname = checkoutCartDTO.Fullname,
                    Email = checkoutCartDTO.Email,
                    PhoneNumber = checkoutCartDTO.Phone,
                    UserId = user.Id,
                    
                };

                await _unitOfWork.AppointmentAddressRepository.Insert(userAddress);

                await _unitOfWork.CommitAsync();

                return userAddress.Id;
            }

            return 0;

        }

    }
}
