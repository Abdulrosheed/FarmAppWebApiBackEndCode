using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FirstProject.Dtos;
using FirstProject.Entities;
using FirstProject.Interfaces.Repositories;
using FirstProject.Interfaces.Services;

namespace FirstProject.Implementation.Services
{
    public class FarmProductService : IFarmProductService
    {
        private readonly IFarmProductRepository _farmProductRepository;
        private readonly IFarmReportRepository _farmReportRepository;
        private readonly IRequestRepository _requestRepository;

        public FarmProductService(IFarmProductRepository farmProductRepository , IFarmReportRepository farmReportRepository , IRequestRepository requestRepository)
        {
            _farmProductRepository = farmProductRepository;
            _farmReportRepository = farmReportRepository;
            _requestRepository = requestRepository;
        }

        // public async Task<BaseResponse<FarmProductDto>> CreateAsync(CreateFarmProductRequestModel model)
        // {
        //     var farmProduct = new FarmProduct
        //     {
        //         Country = model.Country,
        //         LocalGoverment = model.LocalGoverment,
        //         State = model.State,
        //         FarmProduce = model.FarmProduce,
        //         FarmProductStatus = FarmProductStatus.Available,


                
                
        //     };
        //     var farmProductInfo = await _farmProductRepository.CreateAsync(farmProduct);
        //     return new BaseResponse<FarmProductDto>
        //     {
        //         Message = "FarmProduct Sucessfully created",
        //         IsSucess = true,
        //         Data = farmProductInfo
        //     };

        // }

        public async Task<BaseResponse<FarmProductDto>> DeleteAsync(int id)
        {
            var farmProduct = await _farmProductRepository.GetFarmProductReturningFarmProductObjectAsync(id);
            if(farmProduct == null)
            {
                return new BaseResponse<FarmProductDto>
                {
                    IsSucess = false,
                    Message = "Farm Product Not Found"
                };
            }
              farmProduct.IsDeleted = true;
           await _farmProductRepository.UpdateAsync(farmProduct);
            return new BaseResponse<FarmProductDto>
            {
                Message = "Farm Product Sucessfully Deleted",
                IsSucess = true
            };

        }

        public async Task<BaseResponse<IEnumerable<FarmProductDto>>> GetAllFarmProductsAsync()
        {
            var farmProduct = await _farmProductRepository.GetAllFarmProductsAsync();
            if(farmProduct == null)
            {
                return new BaseResponse<IEnumerable<FarmProductDto>>
                {
                    Message = "No farm product has been created"
                };
            }
            return new BaseResponse<IEnumerable<FarmProductDto>>
            {
                Message = "Farm Product Sucessfully Retrieved",
                IsSucess = true,
                Data = await _farmProductRepository.GetAllFarmProductsAsync()
            };
        }

        public async Task<BaseResponse<IEnumerable<FarmProductDto>>> GetAllFarmProductsInspectedByFarmInspectorAsync(string email)
        {
               var farmProduct = await _farmProductRepository.GetAllFarmProductsInspectedByFarmInspectorAsync(email);
            if(farmProduct == null)
            {
                return new BaseResponse<IEnumerable<FarmProductDto>>
                {
                    Message = "No farm product has been created"
                };
            }
            return new BaseResponse<IEnumerable<FarmProductDto>>
            {
                Message = "Farm Product Sucessfully Retrieved",
                IsSucess = true,
                Data = await _farmProductRepository.GetAllFarmProductsAsync()
            };
        }

        public async Task<BaseResponse<IList<FarmProductDto>>> GetFarmProductByFarmerEmailAsync(string email)
        {
            var farmProduct = await _farmProductRepository.GetFarmProductByFarmerEmailReturningFarmProductObjectAsync(email);
            if(farmProduct == null)
            {
                return new BaseResponse<IList<FarmProductDto>>
                {
                    IsSucess = false,
                    Message = "Farm Product Not Found"
                };
            }
            return new BaseResponse<IList<FarmProductDto>>
            {
                Message = "Farm Product Sucessfully Retrieved",
                IsSucess = true,
                Data = farmProduct
            };
        }

        public async Task<BaseResponse<FarmProductDto>> GetFarmProductByIdAsync(int id)
        {
             var farmProduct = await _farmProductRepository.GetFarmProductByIdAsync(id);
            if(farmProduct == null)
            {
                return new BaseResponse<FarmProductDto>
                {
                    IsSucess = false,
                    Message = "Farm Product Not Found"
                };
            }
            return new BaseResponse<FarmProductDto>
            {
                Message = "Farm Product Sucessfully Retrieved",
                IsSucess = true,
                Data = farmProduct
            };
        }

        public async Task<BaseResponse<IEnumerable<FarmProductDto>>> GetFarmProductsByLocalGovernmentAndQuantityAndFarmProduceAsync(string localGovernment)
        {
            var farmProducts = await _farmProductRepository.GetFarmProductsByLocalGovernmentReturningFarmProductDtoObjectAsync(localGovernment);
            if(farmProducts == null)
            {
                return new BaseResponse<IEnumerable<FarmProductDto>>
                {
                    IsSucess = false,
                    Message = "Farm Product Not Found"
                }; 
            }
            return new BaseResponse<IEnumerable<FarmProductDto>>
            {
                IsSucess = true,
                Message = "Farm Product Retrieved Sucessfully",
                Data = farmProducts
            };
        }

        public async Task<BaseResponse<IEnumerable<FarmProductDto>>> GetFarmProductsByStateAndQuantityAndFarmProduceAsync(string state)
        {
            var farmProducts = await _farmProductRepository.GetFarmProductsByLocalGovernmentReturningFarmProductDtoObjectAsync(state);
            if(farmProducts == null)
            {
                return new BaseResponse<IEnumerable<FarmProductDto>>
                {
                    IsSucess = false,
                    Message = "Farm Product Not Found"
                }; 
            }
            return new BaseResponse<IEnumerable<FarmProductDto>>
            {
                IsSucess = true,
                Message = "Farm Product Retrieved Sucessfully",
                Data = farmProducts
            };
        }

        public async Task<BaseResponse<IList<FarmProductDto>>> GetNotSoldFarmProductByFarmerEmailAsync(string email)
        {
            var farmProducts = await _farmProductRepository.GetNotSoldFarmProductByFarmerAsync(email);
            if(farmProducts == null)
            {
                return new BaseResponse<IList<FarmProductDto>>
                {
                    IsSucess = false,
                    Message = "Farm Product Not Found"
                }; 
            }
            return new BaseResponse<IList<FarmProductDto>>
            {
                IsSucess = true,
                Message = "Farm Product Retrieved Sucessfully",
                Data = farmProducts
            };
        }

        public async Task<BaseResponse<IList<FarmProductDto>>> GetSoldFarmProductByFarmerEmailAsync(string email)
        {
            var farmProducts = await _farmProductRepository.GetSoldFarmProductByFarmerAsync(email);
            if(farmProducts == null)
            {
                return new BaseResponse<IList<FarmProductDto>>
                {
                    IsSucess = false,
                    Message = "Farm Product Not Found"
                }; 
            }
            return new BaseResponse<IList<FarmProductDto>>
            {
                IsSucess = true,
                Message = "Farm Product Retrieved Sucessfully",
                Data = farmProducts
            };
        }

        public async Task<BaseResponse<IList<FarmProductDto>>> GetToBeUpdatedFarmProductByFarmerEmailAsync(string email)
        {
             var farmProduct = await _farmProductRepository.GetToBeUpdatedFarmProductByFarmerEmailReturningFarmProductObjectAsync(email);
            if(farmProduct == null)
            {
                return new BaseResponse<IList<FarmProductDto>>
                {
                    IsSucess = false,
                    Message = "Farm Product Not Found"
                };
            }
            return new BaseResponse<IList<FarmProductDto>>
            {
                Message = "Farm Product Sucessfully Retrieved",
                IsSucess = true,
                Data = farmProduct
            };
        }

        public async Task<BaseResponse<IList<FarmProductDto>>> GetToBeUpdatedFarmProductByFarmerInspectorEmailAsync(string email)
        {
            var farmProduct = await _farmProductRepository.GetToBeUpdatedFarmProductByFarmerInspectorReturningFarmProductObjectAsync(email);
            if(farmProduct == null)
            {
                return new BaseResponse<IList<FarmProductDto>>
                {
                    IsSucess = false,
                    Message = "Farm Product Not Found"
                };
            }
            return new BaseResponse<IList<FarmProductDto>>
            {
                Message = "Farm Product Sucessfully Retrieved",
                IsSucess = true,
                Data = farmProduct
            };
        }

        public async Task<BaseResponse<IEnumerable<SearchRequestFarmProductDto>>> SearchRequest(int id)
        {
           var request = await _requestRepository.GetByIdAsync(id);
           var products = await _farmProductRepository.GetFarmProductByRequestAsync(new GetFarmProductByRequestModel(request.YearNeeded , request.MonthNeeded, request.Grade, request.FarmProduce.Name,request.Quantity));
           if(products == null)
           {
               return new BaseResponse<IEnumerable<SearchRequestFarmProductDto>>
               {
                   Message = "This request has no matched request",
                   IsSucess = false
               };
           }
           request.Status = RequestStatus.Merged;
           await _requestRepository.UpdateAsync(request);
           return new BaseResponse<IEnumerable<SearchRequestFarmProductDto>>
           {
               Message = "Farm products retrieved sucessfully",
               IsSucess = true,
               Data = products
           };
            
        }

        public async Task<BaseResponse<FarmProductDto>> UpdateAsync(UpdateFarmProductForAdminRequestModel model , int id)
        {
            var farmProduct = await _farmProductRepository.GetFarmProductReturningFarmProductObjectAsync(id);
            if(farmProduct == null)
            {
                return new BaseResponse<FarmProductDto>
                {
                    IsSucess = false,
                    Message = "Farm Product Not Found"
                };
            }
            var farmReport = new FarmReport
            {
                 FarmId = model.FarmId,
                Quantity = model.Quantity,
                FarmProduct = model.FarmProduct,
                HarvestedPeriod = DateTime.Now.AddMonths(model.HarvestedTime),
                Grade = model.Grade,
                FarmInspectorId = model.FarmInspectorId,
                FarmerId = model.FarmerId,
                FarmReportStatus = Enums.FarmReportStatus.ProcessingUpdateApproval
            };
            var farmReportInfo = await _farmReportRepository.CreateAsync(farmReport);
            if(farmProduct.OrderProducts.Count == 0)
            {
                farmProduct.Quantity = farmProduct.Quantity == 0 ? farmProduct.Quantity :  model.Quantity;
                farmProduct.Grade = model.Grade;
                farmProduct.HarvestedPeriod = DateTime.Now.AddMonths(model.HarvestedTime);
                
            }
            
            
            var farmProductInfo = await _farmProductRepository.UpdateAsync(farmProduct);
            return new BaseResponse<FarmProductDto>
            {
                IsSucess = true,
                Message = "Farm Product Updated Successfully",
                Data = farmProductInfo
            };
        }

        public async Task<BaseResponse<FarmProductDto>> UpdateAsync(UpdateFarmProductForFarmerRequestModel model, int id)
        {
             var farmProduct = await _farmProductRepository.GetFarmProductReturningFarmProductObjectAsync(id);
            if(farmProduct == null)
            {
                return new BaseResponse<FarmProductDto>
                {
                    IsSucess = false,
                    Message = "Farm Product Not Found"
                };
            }
            if(farmProduct.OrderProducts.Count == 0)
            {
                farmProduct.Price = farmProduct.Price == 0 ? farmProduct.Price :  model.price;
            }
           
            
            var farmProductInfo = await _farmProductRepository.UpdateAsync(farmProduct);
            return new BaseResponse<FarmProductDto>
            {
                IsSucess = true,
                Message = "Farm Product Updated Successfully",
                Data = farmProductInfo
            };
        }

       
    }
}