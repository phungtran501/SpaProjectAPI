using Microsoft.EntityFrameworkCore;
using SpaManagement.Data.Abstract;
using SpaManagement.Domain.Entities;
using SpaManagement.Domain.Enums;
using SpaManagement.Service.Abstracts;
using SpaManagement.Service.DTOs;
using SpaManagement.Service.DTOs.Product;
using PlanDetail = SpaManagement.Service.DTOs.PlanDetail;

namespace SpaManagement.Service
{
    public class PlanService : IPlanService
    {
        IUnitOfWork _unitOfWork;
        public PlanService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<PlanDTO>> GetListPlan(int pageIndex, int pageSize)
        {
            var plans = await _unitOfWork.PlanRepository.GetData(x => x.IsActive);

            var result = plans.Skip((pageIndex - 1) * pageSize).Take(pageIndex * pageSize).ToList();

            int total = plans.Count();

            var data = result.Select(x => new PlanDTO
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                Description = x.Decription.Length > 100 ? x.Decription.Substring(0, 100) : x.Decription,
            });

            return data;
        }

        public async Task<ResponseModel> CreateUpdate(PlanDTO planDTO)
        {
            var plan = await _unitOfWork.PlanRepository.GetSingleByConditionAsync(x => x.Name.ToLower() == planDTO.Name.ToLower()
                                                                                   && x.Id != planDTO.Id);

            if (plan is not null)
            {
                return new ResponseModel
                {
                    Status = false,
                    Message = "Plan name is exist",
                    StatusType = StatusType.Fail,
                };
            }

            if (planDTO.Id == 0)
            {
                var newPlan = new Plan
                {
                    Name = planDTO.Name,
                    Decription = planDTO.Description,
                    Price = planDTO.Price,
                    CreatedOn = DateTime.Now,
                    IsActive = true
                };

                await _unitOfWork.PlanRepository.Insert(newPlan);
                await _unitOfWork.PlanRepository.Commit();

                foreach (var item in planDTO.Detail)
                {
                    if(item.Id == 0)
                    {
                        var detail = new Domain.Entities.PlanDetail
                        {
                            PlanId = newPlan.Id,
                            Note = item.Note,
                            ProductId = item.ProductId,
                        };

                        await _unitOfWork.PlanDetailRepository.Insert(detail);
                    }
                }

                await _unitOfWork.PlanDetailRepository.Commit();
            }
            //update
            else
            {
                var getPlan = await _unitOfWork.PlanRepository.GetById(planDTO.Id);

                getPlan.Name = planDTO.Name;
                getPlan.Decription = planDTO.Description;
                getPlan.Price = planDTO.Price;
                getPlan.CreatedOn = DateTime.Now;

                _unitOfWork.PlanRepository.Update(getPlan);

                foreach (var item in planDTO.Detail)
                {
                    if (item.Id == 0)
                    {
                        var detail = new Domain.Entities.PlanDetail
                        {
                            PlanId = getPlan.Id,
                            Note = item.Note,
                            ProductId = item.ProductId,
                        };

                        await _unitOfWork.PlanDetailRepository.Insert(detail);

                        continue;
                    }

                    var getDetail = await _unitOfWork.PlanDetailRepository.GetById(item.Id);

                    getDetail.PlanId = getPlan.Id;
                    getDetail.Note = item.Note;
                    getDetail.ProductId = item.ProductId;

                    _unitOfWork.PlanDetailRepository.Update(getDetail);
                }

                await _unitOfWork.PlanDetailRepository.Commit();

            }
            await _unitOfWork.PlanRepository.Commit();

            return new ResponseModel
            {
                Status = true,
                Message = planDTO.Id is null ? "Insert successful" : "Update successful",
                StatusType = StatusType.Success,
            };
        }

        public async Task<PlanDTO> GetPlanById(int id)
        {

            var plan = await _unitOfWork.PlanRepository.GetById(id);

            var detail = await _unitOfWork.PlanDetailRepository.GetData(x => x.PlanId == id);

            var result = new PlanDTO
            {
                Id = plan.Id,
                Name = plan.Name,
                Description = plan.Decription,
                Price = plan.Price,
                IsActive = plan.IsActive,
                Detail = detail.Select(x => new PlanDetail
                {
                    Id = x.Id,
                    Note = x.Note,
                    ProductId = x.ProductId,
                }).ToList()
            };

            return result;
        }

        public async Task DeletePlan(int key)
        {
            var plan = await _unitOfWork.PlanRepository.GetById(key);
            plan.IsActive = false;
            _unitOfWork.PlanRepository.Update(plan);
            await _unitOfWork.PlanRepository.Commit();
        }

        public async Task<IEnumerable<ProductByPlanDTO>> GetPlanDetail()
        {
            List<ProductByPlanDTO> productByPlanDTOs = new ();

            var lsPlan = (await _unitOfWork.PlanRepository.GetData(x => x.IsActive)).Select(x => new
            {
                PlanId = x.Id,
                Name = x.Name,
                Price = x.Price,
            }).ToList();

            var lsProduct = await _unitOfWork.PlanDetailRepository.Table.Join(_unitOfWork.ProductRepository.Table, x => x.ProductId,
                                                                                                                    y => y.Id,
                                                                                                                    (detail, product) => new
                                                                                                                    {
                                                                                                                        PlanId = detail.PlanId,
                                                                                                                        ProductName = product.Name,

                                                                                                                    }).ToListAsync();
            foreach (var item in lsPlan)
            {
                ProductByPlanDTO productByPlanDTO = new();

                productByPlanDTO.Id = item.PlanId;
                productByPlanDTO.PlanName = item.Name;
                productByPlanDTO.Price = item.Price;
                

                var product = lsProduct.Where(x => x.PlanId == item.PlanId).Select(x => new PlanProduct
                {
                    ProductName = x.ProductName
                }).ToList();

                productByPlanDTO.Product = product;

                productByPlanDTOs.Add(productByPlanDTO);
            }

            return productByPlanDTOs;
        }
    }
}
