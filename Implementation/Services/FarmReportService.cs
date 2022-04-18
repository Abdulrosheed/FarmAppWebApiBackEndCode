using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FirstProject.Dtos;
using FirstProject.Entities;
using FirstProject.Enums;
using FirstProject.Interfaces.Repositories;
using FirstProject.Interfaces.Services;

namespace FirstProject.Implementation.Services
{
    public class FarmReportService : IFarmReportService
    {
        private readonly IFarmReportRepository _farmReportRepository;
        private readonly IFarmInspectorRepository _farmInspectorRepository;
        private readonly IFarmerRepository _farmerRepository;
        private readonly IFarmRepository _farmRepository;
        private readonly IFarmProductRepository _farmProductRepository;

        public FarmReportService(IFarmReportRepository farmReportRepository , IFarmInspectorRepository farmInspectorRepository , IFarmerRepository farmerRepository, IFarmRepository farmRepository,IFarmProductRepository farmProductRepository)
        {
            _farmReportRepository = farmReportRepository;
            _farmInspectorRepository = farmInspectorRepository;
            _farmerRepository = farmerRepository;
            _farmRepository = farmRepository;
            _farmProductRepository = farmProductRepository;
        }

        public async Task<BaseResponse<FarmProductDto>> ApproveFarmReport(int id)
        {
            var farmReport = await _farmReportRepository.GetFarmReportReturningFarmReportObjectAsync(id);
            var farmer = await _farmerRepository.GetFarmerReturningFarmerObjectAsync(farmReport.FarmerId);
            var farm = await _farmRepository.GetFarmReturningFarmObjectAsync(farmReport.FarmId);
            farmReport.FarmReportStatus = FarmReportStatus.Approved;
            farmer.FarmerStatus = FarmerStatus.Approved;
            farm.FarmStatus = FarmStatus.Approved;

            var farmProduct = new FarmProduct
            {
                Quantity = farmReport.Quantity,
                FarmReportId = farmReport.Id,
                Grade = farmReport.Grade,
                LocalGoverment = farm.LocalGoverment,
                Country = farm.Country,
                State = farm.State,
                FarmProductStatus = FarmProductStatus.Available,
                FarmProduce = farmReport.FarmProduct,
                FarmerId = farmer.Id,
                FarmId = farm.Id,
                HarvestedPeriod = farmReport.HarvestedPeriod,
                ProductImage1 = farmReport.ProductImage1,
                ProductImage2 = farmReport.ProductImage2,
                
            };
            var farmProductInfo = await _farmProductRepository.CreateAsync(farmProduct);
            return new BaseResponse<FarmProductDto>
            {
                Message = "Farm product sucessfully created",
                IsSucess = true,
                Data = farmProductInfo
            };

        }

        public async Task<BaseResponse<FarmProductDto>> ApproveUpdatedFarmReport(int id)
        {
            var farmReport = await _farmReportRepository.GetFarmReportReturningFarmReportObjectAsync(id);
            var farmProduct = await _farmProductRepository.GetFarmProductByNameAndFarmId(farmReport.FarmId , farmReport.FarmProduct);
            if(farmReport == null || farmProduct == null)
            {
                return new BaseResponse<FarmProductDto>
                {

                };
            }
            farmReport.FarmReportStatus = FarmReportStatus.Approved;

            farmProduct.Quantity =  farmReport.Quantity;
            farmProduct.Grade = farmReport.Grade ;
            farmProduct.HarvestedPeriod = farmReport.HarvestedPeriod;
            
            var farmReportInfo = await _farmProductRepository.UpdateAsync(farmProduct);
             await _farmReportRepository.UpdateAsync(farmReport);
            return new BaseResponse<FarmProductDto>
            {
                IsSucess = true,
                Message = "Farm Product Updated Successfully",
                Data = farmReportInfo
            };
        }

        public async Task<BaseResponse<FarmReportDto>> CreateAsync(string email , CreateFarmReportRequestModel model , SendFarmProductPics farmProductPics)
        {
            var farmInspector = await _farmInspectorRepository.GetFarmInspectorReturningFarmInspectorDtoObjectAsync(email);
            var farmReport = new FarmReport
            {
                FarmId = model.FarmId,
                Quantity = model.Quantity,
                FarmProduct = model.FarmProduct,
                HarvestedPeriod = DateTime.Now.AddMonths(model.ToBeHarvestedTime),
                Grade = (FarmGrade)model.Grade,
                FarmInspectorId = farmInspector.Id,
                FarmerId = model.FarmerId,
                ProductImage1 = farmProductPics.FarmProdctImage1,
                ProductImage2 = farmProductPics.FarmProdctImage2,
                FarmReportStatus = Enums.FarmReportStatus.ProcessingApproval

            };
            var farmReportInfo = await _farmReportRepository.CreateAsync(farmReport);
            return new BaseResponse<FarmReportDto>
            {
                Message = "Farm Report Sucessfully created",
                IsSucess = true,
                Data = farmReportInfo
            };
        }

        public async Task<BaseResponse<FarmReportDto>> CreateForUpdateAsync(string email ,UpdateFarmReportRequestModel model)
        {
            var farmInspector = await _farmInspectorRepository.GetFarmInspectorReturningFarmInspectorDtoObjectAsync(email);
            var farmReport = new FarmReport
            {
                FarmId = model.FarmId,
                Quantity = model.Quantity,
                FarmProduct = model.FarmProduct,
                HarvestedPeriod = DateTime.Now.AddMonths(model.ToBeHarvestedTime),
                Grade = model.Grade,
                FarmInspectorId = farmInspector.Id,
                FarmerId = model.FarmerId,
                FarmReportStatus = Enums.FarmReportStatus.ProcessingUpdateApproval

            };
            var farmReportInfo = await _farmReportRepository.CreateAsync(farmReport);
            return new BaseResponse<FarmReportDto>
            {
                Message = "Farm Report Sucessfully created",
                IsSucess = true,
                Data = farmReportInfo
            };
        }

        public async Task<BaseResponse<FarmReportDto>> DeclineFarmReport(int id)
        {
            int count = 0;
            int count2 = 0;
            var farmReport = await _farmReportRepository.GetFarmReportReturningFarmReportObjectAsync(id);
            var farmer = await _farmerRepository.GetFarmerReturningFarmerObjectAsync(farmReport.FarmerId);
            var farm = await _farmRepository.GetFarmReturningFarmObjectAsync(farmReport.FarmId);
            farmReport.FarmReportStatus = FarmReportStatus.Declined;
           
            foreach(var product in farm.FarmProducts)
           {
               if(product.FarmProductStatus == FarmProductStatus.Available)
               {
                   count++;
               }
                
            }
            if(count < 1)
            {
                farm.FarmStatus = FarmStatus.Declined;
            }
            
           
            var farmReportInfo = await _farmReportRepository.UpdateAsync(farmReport);
            foreach(var item in farmer.Farms)
            {
                if(item.FarmStatus == FarmStatus.Approved)
                {
                    count2++;
                }
            }
               if(count2 < 1)
            {
                farmer.FarmerStatus = FarmerStatus.Declined;
            }
            await _farmerRepository.UpdateAsync(farmer);
            return new BaseResponse<FarmReportDto>
            {
                Message = "Farm report sucessfully updated",
                IsSucess = true,
                Data = farmReportInfo
            };

        }

        public async Task<BaseResponse<FarmReportDto>> DeleteAsync(int id)
        {
           var farmReport = await _farmReportRepository.GetFarmReportReturningFarmReportObjectAsync(id);
            if(farmReport == null)
            {
                return new BaseResponse<FarmReportDto>
                {
                    IsSucess = false,
                    Message = "Farm Report Not Found"
                };
            }
             farmReport.IsDeleted = true; 
            await _farmReportRepository.UpdateAsync(farmReport);
            return new BaseResponse<FarmReportDto>
            {
                Message = "Farm Product Sucessfully Deleted",
                IsSucess = true
            };
        }

        public async Task<BaseResponse<IEnumerable<FarmReportDto>>> GetAllApprovedFarmReportsAsync()
        {
            var farmReport = await _farmReportRepository.GetAllApprovedFarmReportsAsync();
            if(farmReport == null)
            {
                return new BaseResponse<IEnumerable<FarmReportDto>>
                {
                    Message = "No farm report has been created"
                };
            }
             return new BaseResponse<IEnumerable<FarmReportDto>>
            {
                Message = "Farm report Sucessfully Retrieved",
                IsSucess = true,
                Data = await _farmReportRepository.GetAllApprovedFarmReportsAsync()
            };
        }

        public async Task<BaseResponse<IEnumerable<FarmReportDto>>> GetAllApprovedFarmReportsByFarmInspectorEmailAsync(string email)
        {
            var farmReport = await _farmReportRepository.GetAllApprovedFarmReportsByFarmInspectorAsync(email);
            if(farmReport == null)
            {
                return new BaseResponse<IEnumerable<FarmReportDto>>
                {
                    Message = "No farm report has been created"
                };
            }
             return new BaseResponse<IEnumerable<FarmReportDto>>
            {
                Message = "Farm report Sucessfully Retrieved",
                IsSucess = true,
                Data = await _farmReportRepository.GetAllApprovedFarmReportsByFarmInspectorAsync(email)
            };
        }

        public async Task<BaseResponse<IEnumerable<FarmReportDto>>> GetAllDeclinedFarmReportsAsync()
        {
              var farmReport = await _farmReportRepository.GetAllDeclinedFarmReportsAsync();
            if(farmReport == null)
            {
                return new BaseResponse<IEnumerable<FarmReportDto>>
                {
                    Message = "No farm report has been created"
                };
            }
             return new BaseResponse<IEnumerable<FarmReportDto>>
            {
                Message = "Farm report Sucessfully Retrieved",
                IsSucess = true,
                Data = await _farmReportRepository.GetAllDeclinedFarmReportsAsync()
            };
        }

        public async Task<BaseResponse<IEnumerable<FarmReportDto>>> GetAllFarmReportsAsync()
        {
            var farmReport = await _farmReportRepository.GetAllFarmReportsAsync();
            if(farmReport == null)
            {
                return new BaseResponse<IEnumerable<FarmReportDto>>
                {
                    Message = "No farm report has been created"
                };
            }
             return new BaseResponse<IEnumerable<FarmReportDto>>
            {
                Message = "Farm report Sucessfully Retrieved",
                IsSucess = true,
                Data = await _farmReportRepository.GetAllFarmReportsAsync()
            };
        }

        public async Task<BaseResponse<IEnumerable<FarmReportDto>>> GetAllProcessingFarmReportsAsync()
        {
            var farmReport = await _farmReportRepository.GetAllProcessingFarmReportsAsync();
            if(farmReport == null)
            {
                return new BaseResponse<IEnumerable<FarmReportDto>>
                {
                    Message = "No processing farmReport has been created",
                    IsSucess = false
                };
            }
            return new BaseResponse<IEnumerable<FarmReportDto>>
            {
                Message = "Farm report Sucessfully Retrieved",
                IsSucess = true,
                Data = farmReport
            };
        }

        public async Task<BaseResponse<IEnumerable<FarmReportDto>>> GetAllUpdatedFarmReportsAsync()
        {
             var farmReport = await _farmReportRepository.GetAllUpdatedFarmReportsAsync();
            if(farmReport == null)
            {
                return new BaseResponse<IEnumerable<FarmReportDto>>
                {
                    IsSucess = false,
                    Message = "Farm Report Not Found"
                };
            }
            return new BaseResponse<IEnumerable<FarmReportDto>>
            {
                Message = "Farm Report Sucessfully Retrieved",
                IsSucess = true,
                Data = farmReport
            };
        }

        public async Task<BaseResponse<FarmReportDto>> GetFarmReportAsync(int id)
        {
            var farmReport = await _farmReportRepository.GetFarmReportReturningFarmReportDtoObjectAsync(id);
            if(farmReport == null)
            {
                return new BaseResponse<FarmReportDto>
                {
                    IsSucess = false,
                    Message = "Farm Report Not Found"
                };
            }
            return new BaseResponse<FarmReportDto>
            {
                Message = "Farm Report Sucessfully Retrieved",
                IsSucess = true,
                Data = farmReport
            };
        }

        public async Task<BaseResponse<FarmReportDto>> GetFarmReportByFarmIdAsync(int farmId)
        {
           var farmReport = await _farmReportRepository.GetFarmReportByFarmIdReturningFarmReportDtoObjectAsync(farmId);
            if(farmReport == null)
            {
                return new BaseResponse<FarmReportDto>
                {
                    IsSucess = false,
                    Message = "Farm Report Not Found"
                };
            }
            return new BaseResponse<FarmReportDto>
            {
                Message = "Farm Report Sucessfully Retrieved",
                IsSucess = true,
                Data = farmReport
            };
        }

        public async Task<BaseResponse<FarmReportDto>> GetFarmReportByFarmInspectorEmailAsync(string email)
        {
            var farmReport = await _farmReportRepository.GetFarmReportByFarmInspectorEmailReturningFarmReportDtoObjectAsync(email);
            if(farmReport == null)
            {
                return new BaseResponse<FarmReportDto>
                {
                    IsSucess = false,
                    Message = "Farm Report Not Found"
                };
            }
            return new BaseResponse<FarmReportDto>
            {
                Message = "Farm Report Sucessfully Retrieved",
                IsSucess = true,
                Data = farmReport
            };
        }

        public async Task<BaseResponse<FarmReportDto>> UpdateAsync(UpdateFarmReportRequestModel model, int id)
        {
            var farmReport = await _farmReportRepository.GetFarmReportReturningFarmReportObjectAsync(id);
            
            var farmProduct = await _farmProductRepository.GetFarmProductByFarmIdReturningFarmProductObjectAsync(farmReport.FarmId);
            
            if(farmReport == null || farmProduct == null)
            {
                return new BaseResponse<FarmReportDto>
                {
                    IsSucess = false,
                    Message = "Farm Report Not Found"
                };
            }
        
            farmReport.Quantity = farmReport.Quantity == 0 ? farmReport.Quantity :  model.Quantity;
            farmReport.Grade = model.Grade ;
            farmReport.HarvestedPeriod = DateTime.Now.AddMonths(model.ToBeHarvestedTime);
            farmReport.FarmReportStatus = FarmReportStatus.Approved;

            farmProduct.Quantity = farmProduct.Quantity == 0 ? farmProduct.Quantity :  model.Quantity;
            farmProduct.Grade = model.Grade ;
            farmProduct.HarvestedPeriod = DateTime.Now.AddMonths(model.ToBeHarvestedTime);
            
            await _farmProductRepository.UpdateAsync(farmProduct);
            var farmReportInfo = await _farmReportRepository.UpdateAsync(farmReport);
            return new BaseResponse<FarmReportDto>
            {
                IsSucess = true,
                Message = "Farm Product Updated Successfully",
                Data = farmReportInfo
            };
        }
    }
}