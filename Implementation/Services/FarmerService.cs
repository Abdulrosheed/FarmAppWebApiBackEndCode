using System.Collections.Generic;
using System.Threading.Tasks;
using FirstProject.Dtos;
using FirstProject.Entities;
using FirstProject.Enums;
using FirstProject.Interfaces.Repositories;
using FirstProject.Interfaces.Services;
using FirstProject.MailBox;

namespace FirstProject.Implementation.Services
{
    public class FarmerService : IFarmerService
    {
        private readonly IFarmerRepository _farmerRepository;
        private readonly IFarmRepository _farmRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IFarmProduceRepository _farmProduceRepository;
        private readonly IMailMessage _mailMessage;

        public FarmerService(IFarmerRepository farmerRepository, IFarmRepository farmRepository, IUserRepository userRepository, 
        IRoleRepository roleRepository,IFarmProduceRepository farmProduceRepository , IMailMessage mailMessage)
        {
            _farmerRepository = farmerRepository;
            _farmRepository = farmRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _farmProduceRepository = farmProduceRepository;
            _mailMessage = mailMessage;
        }

        public async Task<BaseResponse<IList<FarmerDto>>> AllAssignedFarmerAprovalAsync()
        {
            var farmer = await _farmerRepository.AllAssignedFarmerAprovalAsync();
            if(farmer == null)
            {
                return new BaseResponse<IList<FarmerDto>>
                {
                    IsSucess = false,
                    Message = " Farmer has been declined"
                };
            }
            return new BaseResponse<IList<FarmerDto>>
            {
                Message = "Farmer Retrieved",
                IsSucess = true,
                Data = farmer
            };
        }

        public async Task<BaseResponse<IList<FarmerDto>>> AllDeclinedFarmerAprovalAsync()
        {
           var farmer = await _farmerRepository.AllDeclinedFarmerAprovalAsync();
            if(farmer == null)
            {
                return new BaseResponse<IList<FarmerDto>>
                {
                    IsSucess = false,
                    Message = " Farmer has been declined"
                };
            }
            return new BaseResponse<IList<FarmerDto>>
            {
                Message = "Farmer Retrieved",
                IsSucess = true,
                Data = farmer
            };
        }

        public async Task<BaseResponse<IList<FarmerDto>>> AllProcessingFarmerAprovalAsync()
        {
             var farmer = await _farmerRepository.AllProcessingFarmerAprovalAsync();
            if(farmer == null)
            {
                return new BaseResponse<IList<FarmerDto>>
                {
                    IsSucess = false,
                    Message = "No Farmer waiting for approval"
                };
            }
            return new BaseResponse<IList<FarmerDto>>
            {
                Message = "Farmer Retrieved",
                IsSucess = true,
                Data = farmer
            };
        }


        public async Task<BaseResponse<FarmerDto>> ChangeFarmerStatusToDeclinedAsync(int id)
        {
            var farmer = await _farmerRepository.GetFarmerReturningFarmerObjectAsync(id);
            if(farmer.Farms.Count == 0)
            {
                farmer.FarmerStatus = FarmerStatus.Declined;
            }
            
            var farmerInfo = await _farmerRepository.UpdateAsync(farmer);
            return new BaseResponse<FarmerDto>
            {
                Message = "Farm sucessfully updated",
                IsSucess = true,
                Data = farmerInfo
            };
        }

        public async Task<BaseResponse<FarmerDto>> DeleteAsync(int id)
        {
            if(!(await _farmerRepository.ExistsByIdAsync(id)))
            {
                return new BaseResponse<FarmerDto>
                {
                    IsSucess = false,
                    Message = "Farmer Not Found"
                };
            }
            var farmer = await _farmerRepository.GetFarmerReturningFarmerObjectAsync(id);
            var user = await _userRepository.GetUserReturningUserObjectAsync(farmer.UserId);
            user.IsDeleted = true;
            farmer.IsDeleted = true;
            await _userRepository.UpdateAsync(user);
            await _farmerRepository.UpdateAsync(farmer);
            return new BaseResponse<FarmerDto>
            {
                Message = "Farmer Sucessfully Deleted",
                IsSucess = true
            };
        }

        public async Task<BaseResponse<IEnumerable<FarmerDto>>> GetAllFarmersAsync()
        {
            var farmer = await _farmerRepository.GetAllFarmersAsync();
            if(farmer == null)
            {
                return new BaseResponse<IEnumerable<FarmerDto>>
                {
                    Message = "No Admin has been created"
                };
            }

            return new BaseResponse<IEnumerable<FarmerDto>>
            {
                Message = "Admins Sucessfully Retrieved",
                IsSucess = true,
                Data = farmer
            };
        }
        public async Task<BaseResponse<FarmerDto>> GetFarmerByEmailAsync(string email)
        {
            if(!(await _farmerRepository.ExistsByEmailAsync(email)))
            {
                return new BaseResponse<FarmerDto>
                {
                    IsSucess = false,
                    Message = "Farmer Not Found"
                };
            }
            var farmer = await _farmerRepository.GetFarmerReturningFarmerDtoObjectAsync(email);
            return new BaseResponse<FarmerDto>
            {
                IsSucess = true,
                Message = "Farmer Sucessfully Retrieved",
                Data = farmer
               
            };
        }

        public async Task<BaseResponse<FarmerDto>> GetFarmerByIdAsync(int id)
        {
            if(!(await _farmerRepository.ExistsByIdAsync(id)))
            {
                return new BaseResponse<FarmerDto>
                {
                    IsSucess = false,
                    Message = "Farmer Not Found"
                };
            }
            var farmer = await _farmerRepository.GetFarmerReturningFarmerDtoObjectAsync(id);
            return new BaseResponse<FarmerDto>
            {
                IsSucess = true,
                Message = "Farmer Sucessfully Retrieved",
                Data = farmer
               
            };
        }
        public async Task<BaseResponse<FarmerDto>>  RegisterAsync(CreateFarmerRequestModel model )
        {
            if(await _userRepository.ExistsByEmailAsync(model.Email))
            {
                return new BaseResponse<FarmerDto>
                {
                    IsSucess = false,
                    Message = "Farmer with this password or email already exists"
                };
            }
            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                PassWord = model.PassWord
                
                
            };
            var role = await _roleRepository.GetRoleByNameReturningRoleObjectAsync("Farmer");
            var userRole = new UserRole
            {
                User = user,
                UserId = user.Id,
                Role = role,
                RoleId = role.Id
            };
            user.UserRole.Add(userRole);
            
            var farmer = new Farmer
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                 UserName = model.UserName,
                State = model.State,
                Country = model.Country,
                LocalGoverment = model.LocalGovernment,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Image = model.Image,
                FarmerStatus = Enums.FarmerStatus.ProcessingApproval,
                UserId = user.Id,
                User = user
                
            };
            var farm = new Farm
            {
                Name = model.FarmName,
                LandSize = model.LandSize,
                FarmPicture1 = model.FarmPicture1,
                FarmPicture2 = model.FarmPicture2,
                 State = model.FarmState,
                Country = model.FarmCountry,
                LocalGoverment = model.FarmLocalGovernment,
                FarmStatus = FarmStatus.ProcessingApproval,
                FarmerId = farmer.Id,
                Farmer = farmer
               
            
            };
            var farmProduce = await _farmProduceRepository.GetSelectedFarmProduce(model.FarmProduceIds);
            
            foreach(var item in farmProduce)
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
         
            await _userRepository.CreateAsync(user);
              var farmerInfo = await _farmerRepository.CreateAsync(farmer);
            await _farmRepository.CreateAsync(farm);
            _mailMessage.RegistrationNotificationEmail(farmerInfo.Email , "");
            _mailMessage.RegistrationNotificationForFarm(farmerInfo.Email);
          
            return new BaseResponse<FarmerDto>
            {
                Message = "Farmer Successfully created but not yet approved",
                IsSucess = true,
                Data = farmerInfo
            };
        }

        public async Task<BaseResponse<FarmerDto>> UpdateAsync(UpdateFarmerRequestModel model, int id)
        {
           var farmer = await _farmerRepository.GetFarmerReturningFarmerObjectAsync(id);
           var user = await _userRepository.GetUserReturningUserObjectAsync(farmer.UserId);
            if(farmer == null)
            {
                return new BaseResponse<FarmerDto>
                {
                    IsSucess = false,
                    Message = "Farmer Not Found"
                };
            } 

                user.FirstName = model.FirstName ?? user.FirstName;
            user.PhoneNumber = model.PhoneNumber ?? user.PhoneNumber;
            user.LastName = model.LastName ?? user.LastName;
             user.PassWord = model.PassWord ?? user.PassWord;
          

            farmer.State = model.State ?? farmer.State;
            farmer.Country = model.Country ?? farmer.Country;
            farmer.LocalGoverment = model.LocalGovernment ?? farmer.LocalGoverment;
           
            farmer.FirstName = model.FirstName ?? farmer.FirstName;
            farmer.LastName = model.LastName ?? farmer.LastName;
            farmer.PhoneNumber = model.PhoneNumber ?? farmer.PhoneNumber;
            farmer.UserName = model.UserName ?? farmer.UserName;
    
            await _userRepository.UpdateAsync(user);
            var farmerInfo = await _farmerRepository.UpdateAsync(farmer);
            _mailMessage.UpdateNotificationEmail(farmerInfo.Email , "");
            return new BaseResponse<FarmerDto>
            {
                Message = "Farmer Sucessfully Updated",
                IsSucess = true,
                Data = farmerInfo
            };
        }
        
        public async Task<BaseResponse<FarmerDto>> UpdateFarmerAsync( int id)
        {
           var farmer = await _farmerRepository.GetFarmerReturningFarmerObjectAsync(id);
            if(farmer == null)
            {
                return new BaseResponse<FarmerDto>
                {
                    IsSucess = false,
                    Message = "Farmer Not Found"
                };
            }
    
            farmer.FarmerStatus = FarmerStatus.Assigned;
            var farmerInfo = await _farmerRepository.UpdateAsync(farmer);
            return new BaseResponse<FarmerDto>
            {
                Message = "Farmer Sucessfully Updated",
                IsSucess = true,
                Data = farmerInfo
            };

        }
    }
}