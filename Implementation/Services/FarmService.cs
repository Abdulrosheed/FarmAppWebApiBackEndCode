using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using crypto;
using FirstProject.Dtos;
using FirstProject.Entities;
using FirstProject.Interfaces.Repositories;
using FirstProject.Interfaces.Services;
using FirstProject.MailBox;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace FirstProject.Implementation.Services
{
    public class FarmService : IFarmService
    {
        private readonly IFarmProduceRepository _farmProduceRepository;
        private readonly IFarmRepository _farmRepository;
        private readonly IFarmerRepository _farmerRepository;
        private readonly IMailMessage _mailMessage;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FarmService(IFarmProduceRepository farmProduceRepository, IFarmRepository farmRepository, IFarmerRepository farmerRepository, IWebHostEnvironment webHostEnvironment , IMailMessage mailMessage)
        {
            _farmProduceRepository = farmProduceRepository;
            _farmRepository = farmRepository;
            _farmerRepository = farmerRepository;
            _webHostEnvironment = webHostEnvironment;
            _mailMessage = mailMessage;
        }

        public async Task<BaseResponse<FarmDto>> DeleteAsync(int id)
        {
            var farm = await _farmRepository.GetFarmReturningFarmObjectAsync(id);
            if (farm == null)
            {
                return new BaseResponse<FarmDto>
                {
                    IsSucess = false,
                    Message = "Farm  Not Found"
                };
            }
            farm.IsDeleted = true;
            await _farmRepository.UpdateAsync(farm);
            return new BaseResponse<FarmDto>
            {
                Message = "Farm  Sucessfully Deleted",
                IsSucess = true
            };
        }

        public async Task<BaseResponse<IEnumerable<FarmDto>>> GetAllApprovedFarmByFarmerAsync(string email)
        {
            var farms = await _farmRepository.GetAllAssignedFarmByFarmerAsync(email);
            if (farms == null)
            {
                return new BaseResponse<IEnumerable<FarmDto>>
                {
                    Message = "No farm has been created",
                    IsSucess = false
                };
            }
            return new BaseResponse<IEnumerable<FarmDto>>
            {
                Message = "Farm  Sucessfully Retrieved",
                IsSucess = true,
                Data = await _farmRepository.GetAllAssignedFarmByFarmerAsync(email)
            };
        }

        public async Task<BaseResponse<IEnumerable<FarmDto>>> GetAllAssignedFarmAsync()
        {
            var farms = await _farmRepository.GetAllAssignedFarmAsync();
            if (farms == null)
            {
                return new BaseResponse<IEnumerable<FarmDto>>
                {
                    Message = "No farm has been created",
                    IsSucess = false
                };
            }
            return new BaseResponse<IEnumerable<FarmDto>>
            {
                Message = "Farm  Sucessfully Retrieved",
                IsSucess = true,
                Data = await _farmRepository.GetAllAssignedFarmAsync()
            };
        }

        public async Task<BaseResponse<IEnumerable<FarmDto>>> GetAllDeclinedFarmAsync()
        {

            var farms = await _farmRepository.GetAllDeclinedFarmAsync();
            if (farms == null)
            {
                return new BaseResponse<IEnumerable<FarmDto>>
                {
                    Message = "No farm has been created",
                    IsSucess = false
                };
            }
            return new BaseResponse<IEnumerable<FarmDto>>
            {
                Message = "Farm  Sucessfully Retrieved",
                IsSucess = true,
                Data = await _farmRepository.GetAllDeclinedFarmAsync()
            };
        }

        public async Task<BaseResponse<IEnumerable<FarmDto>>> GetAllDeclinedFarmByFarmerAsync(string email)
        {
             var farms = await _farmRepository.GetAllDeclinedFarmByFarmerAsync(email);
            if (farms == null)
            {
                return new BaseResponse<IEnumerable<FarmDto>>
                {
                    Message = "No farm has been created",
                    IsSucess = false
                };
            }
            return new BaseResponse<IEnumerable<FarmDto>>
            {
                Message = "Farm  Sucessfully Retrieved",
                IsSucess = true,
                Data = await _farmRepository.GetAllDeclinedFarmByFarmerAsync(email)
            };
        }

        public async Task<BaseResponse<IEnumerable<FarmDto>>> GetAllFarmAsync()
        {
            var farms = await _farmRepository.GetAllFarmAsync();
            if (farms == null)
            {
                return new BaseResponse<IEnumerable<FarmDto>>
                {
                    Message = "No farm has been created",
                    IsSucess = false
                };
            }
            return new BaseResponse<IEnumerable<FarmDto>>
            {
                Message = "Farm  Sucessfully Retrieved",
                IsSucess = true,
                Data = await _farmRepository.GetAllFarmAsync()
            };
        }

        public async Task<BaseResponse<IList<FarmDto>>> GetAllInspectedFarmerByFarmInspectorEmailAsync(string farmInspectorEmail)
        {
            var farm = await _farmRepository.GetAllInspectedFarmerByFarmInspectorIdAsync(farmInspectorEmail);
            if (farm == null)
            {
                return new BaseResponse<IList<FarmDto>>
                {
                    IsSucess = false,
                    Message = "This farm inspector has not inspected any farmer"
                };
            }
            return new BaseResponse<IList<FarmDto>>
            {
                Message = "Farmer Retrieved",
                IsSucess = true,
                Data = farm
            };
        }

        public async Task<BaseResponse<IEnumerable<FarmDto>>> GetAllProcessingFarmAsync()
        {
            var farms = await _farmRepository.GetAllProcessingFarmAsync();
            if (farms == null)
            {
                return new BaseResponse<IEnumerable<FarmDto>>
                {
                    Message = "No farm has been created",
                    IsSucess = false
                };
            }
            return new BaseResponse<IEnumerable<FarmDto>>
            {
                Message = "Farm  Sucessfully Retrieved",
                IsSucess = true,
                Data = await _farmRepository.GetAllProcessingFarmAsync()
            };
        }

        public async Task<BaseResponse<FarmDto>> GetFarmAsync(int id)
        {
            var farm = await _farmRepository.GetFarmReturningFarmDtoObjectAsync(id);
            if (farm == null)
            {
                return new BaseResponse<FarmDto>
                {
                    IsSucess = false,
                    Message = "Farm  Not Found"
                };
            }
            return new BaseResponse<FarmDto>
            {
                Message = "Farm Sucessfully Retrieved",
                IsSucess = true,
                Data = farm
            };
        }

        public async Task<BaseResponse<IList<FarmDto>>> GetTobeInspectedFarmerByFarmInspectorEmailAsync(string farmInspectorEmail)
        {
            var farm = await _farmRepository.GetTobeInspectedFarmerByFarmInspectorIdAsync(farmInspectorEmail);
            if (farm == null)
            {
                return new BaseResponse<IList<FarmDto>>
                {
                    IsSucess = false,
                    Message = "This farm inspector has not inspected any farmer"
                };
            }
            return new BaseResponse<IList<FarmDto>>
            {
                Message = "Farm Retrieved",
                IsSucess = true,
                Data = farm
            };
        }

        public async Task<BaseResponse<FarmDto>> RegisterAsync(CreateFarmRequestModel model, string email)
        {


            var farmer = await _farmerRepository.GetFarmerReturningFarmerObjectAsync(email);
            if ((await _farmRepository.ExistsByNameAsync(model.Name)) )
            {
                return new BaseResponse<FarmDto>
                {
                    Message = "Farm  already exists",
                    IsSucess = false
                };
            }

            



            var farm = new Farm
            {
                Name = model.Name,
                FarmPicture1 = model.FarmPicture1,
                FarmPicture2 = model.FarmPicture2,
                LandSize = model.LandSize,
                State = model.State,
                Country = model.Country,
                LocalGoverment = model.LocalGovernment,
                FarmerId = farmer.Id,
                Farmer = farmer,
                FarmStatus = FarmStatus.ProcessingApproval,

            };

            var farmProduces = await _farmProduceRepository.GetSelectedFarmProduce(model.FarmProduceIds);
            foreach (var item in farmProduces)
            {
                var farmProduceFarm = new FarmProduceFarm
                {
                    Farm = farm,
                    FarmId = farm.Id,
                    FarmProduce = item,
                    FarmProduceId = item.Id
                };
                farm.FarmProduceFarm.Add(farmProduceFarm);
            }
            var farmInfo = await _farmRepository.CreateAsync(farm);
            _mailMessage.RegistrationNotificationForFarm(email);

            return new BaseResponse<FarmDto>
            {
                Message = "Farm  Sucessfully Created",
                IsSucess = true,
                Data = farmInfo
            };
        }

        // public async Task<BaseResponse<FarmDto>> UpdateAsync(UpdateFarmRequestModel model, int id)
        // {
        //     var farm = await _farmRepository.GetFarmReturningFarmObjectAsync(id);
        //     if (farm == null)
        //     {
        //         return new BaseResponse<FarmDto>
        //         {
        //             IsSucess = false,
        //             Message = "Farm  Not Found"
        //         };
        //     }
        //     farm.Name = model.Name ?? farm.Name;
        //     var farmInfo = await _farmRepository.UpdateAsync(farm);
        //     return new BaseResponse<FarmDto>
        //     {
        //         IsSucess = true,
        //         Message = "Farm  Updated Successfully",
        //         Data = farmInfo
        //     };
        // }

        public async Task<BaseResponse<FarmDto>> UpdateFarmAsync(int farmInspectorId, int id , DateTime inspectionDate)
        {
            var farm = await _farmRepository.GetFarmReturningFarmObjectAsync(id);
            if (farm == null)
            {
                return new BaseResponse<FarmDto>
                {
                    IsSucess = false,
                    Message = "Farm  Not Found"
                };
            }
            farm.FarmInspectorId = farmInspectorId;
            farm.FarmStatus = FarmStatus.Assigned;
            farm.InspectionDate = inspectionDate;
            var farmInfo = await _farmRepository.UpdateAsync(farm);

            return new BaseResponse<FarmDto>
            {
                IsSucess = true,
                Message = "Farm  Updated Successfully",
                Data = farmInfo
            };
        }

        public async Task<BaseResponse<FarmDto>> UpdateFarmAsync(int id)
        {
            var farm = await _farmRepository.GetFarmReturningFarmObjectAsync(id);
            if (farm == null)
            {
                return new BaseResponse<FarmDto>
                {
                    IsSucess = false,
                    Message = "Farm  Not Found"
                };
            }

            farm.FarmStatus = FarmStatus.Inspected;
            var farmInfo = await _farmRepository.UpdateAsync(farm);
            return new BaseResponse<FarmDto>
            {
                IsSucess = true,
                Message = "Farm  Updated Successfully",
                Data = farmInfo
            };
        }
    }
}