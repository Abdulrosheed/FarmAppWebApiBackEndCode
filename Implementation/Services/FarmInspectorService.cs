using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FirstProject.Dtos;
using FirstProject.Entities;
using FirstProject.Interfaces.Repositories;
using FirstProject.Interfaces.Services;
using FirstProject.MailBox;

namespace FirstProject.Implementation.Services
{
    public class FarmInspectorService : IFarmInspectorService
    {
        private readonly IFarmInspectorRepository _farmInspectorRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMailMessage _mailMessage;

        public FarmInspectorService(IFarmInspectorRepository farmInspectorRepository, IUserRepository userRepository, IRoleRepository roleRepository , IMailMessage mailMessage)
        {
            _farmInspectorRepository = farmInspectorRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _mailMessage = mailMessage;

        }

        public async Task<BaseResponse<FarmInspectorDto>> DeleteAsync(int id)
        {
            if(!(await _farmInspectorRepository.ExistsByIdAsync(id)))
            {
                return new BaseResponse<FarmInspectorDto>
                {
                    IsSucess = false,
                    Message = "Farm Inspector Not Found"
                };
            }
            var farmInspector = await _farmInspectorRepository.GetFarmInspectorReturningFarmInspectorObjectAsync(id);
            var user = await _userRepository.GetUserReturningUserObjectAsync(farmInspector.UserId);
            user.IsDeleted = true;
            farmInspector.IsDeleted = true;
            await _userRepository.UpdateAsync(user);
            await _farmInspectorRepository.UpdateAsync(farmInspector);
            return new BaseResponse<FarmInspectorDto>
            {
                Message = "Farm Inspector Sucessfully Deleted",
                IsSucess = true
            };
        }

        public async Task<BaseResponse<IEnumerable<FarmInspectorDto>>> GetAllFarmInspectorAsync()
        {
            var farmInspector = await _farmInspectorRepository.GetAllFarmInspectorAsync();
          if(farmInspector == null)
          {
              return new BaseResponse<IEnumerable<FarmInspectorDto>>
              {
                  Message = "No Admin has been created",
                  IsSucess = false
              };
          }
          return new BaseResponse<IEnumerable<FarmInspectorDto>>
            {
                Message = "Admins Sucessfully Retrieved",
                IsSucess = true,
                Data = await _farmInspectorRepository.GetAllFarmInspectorAsync()
            };   
        }

        public async Task<BaseResponse<FarmInspectorDto>> GetFarmInspectorAsync(int id)
        {
           if(!(await _farmInspectorRepository.ExistsByIdAsync(id)))
            {
                return new BaseResponse<FarmInspectorDto>
                {
                    IsSucess = false,
                    Message = "Farm Inspector Not Found"
                };
            }
            var farmInspector = await _farmInspectorRepository.GetFarmerReturningFarmInspectorDtoObjectAsync(id);
            return new BaseResponse<FarmInspectorDto>
            {
                IsSucess = true,
                Message = "Farm Inspector Sucessfully Retrieved",
                Data = farmInspector
               
            };
            
        }

        public async Task<BaseResponse<IList<FarmInspectorDto>>> GetFarmInspectorByCountryAsync(string country)
        {
            var farmInspector = await _farmInspectorRepository.GetFarmInspectorByCountryReturningFarmInspectorObjectDtoAsync(country);
            if(farmInspector == null)
            {
                return new BaseResponse<IList<FarmInspectorDto>>
                {
                    Message = "No farmInspector found",
                    IsSucess = false,
                };
            }
            return new BaseResponse<IList<FarmInspectorDto>>
            {
                Message = "Farm Inspectors retrieved",
                IsSucess = true,
                Data = farmInspector
            };
        }

        public async Task<BaseResponse<FarmInspectorDto>> GetFarmInspectorByEmailAsync(string email)
        {
            var farmInspector = await _farmInspectorRepository.GetFarmInspectorReturningFarmInspectorDtoObjectAsync(email);
            if(farmInspector == null)
            {
                return new BaseResponse<FarmInspectorDto>
                {
                    Message = "Farmer not found",
                    IsSucess = false
                };
            }
            return new BaseResponse<FarmInspectorDto>
            {
                Message = "Farmer Sucessfully retrieved",
                IsSucess = true,
                Data = farmInspector
            };
        }

        public async Task<BaseResponse<IList<FarmInspectorDto>>> GetFarmInspectorByLocalGovernmentAsync(string localGoverment)
        {
            var farmInspector = await _farmInspectorRepository.GetFarmInspectorByLocalGovernmentReturningFarmInspectorObjectDtoAsync(localGoverment);
            if(farmInspector == null)
            {
                return new BaseResponse<IList<FarmInspectorDto>>
                {
                    Message = "No farmInspector found",
                    IsSucess = false,
                };
            }
            return new BaseResponse<IList<FarmInspectorDto>>
            {
                Message = "Farm Inspectors retrieved",
                IsSucess = true,
                Data = farmInspector
            };
        }

        public async Task<BaseResponse<IList<FarmInspectorDto>>> GetFarmInspectorByStateAsync(string state)
        {
            var farmInspector = await _farmInspectorRepository.GetFarmInspectorByStateReturningFarmInspectorObjectDtoAsync(state);
            if(farmInspector == null)
            {
                return new BaseResponse<IList<FarmInspectorDto>>
                {
                    Message = "No farmInspector found",
                    IsSucess = false,
                };
            }
            return new BaseResponse<IList<FarmInspectorDto>>
            {
                Message = "Farm Inspectors retrieved",
                IsSucess = true,
                Data = farmInspector
            };
        }

        public async Task<BaseResponse<FarmInspectorDto>> RegisterAsync(CreateFarmInspectorRequestModel model)
        {
            
            if((await _userRepository.ExistsByEmailAsync(model.Email) ))
            {
                return new BaseResponse<FarmInspectorDto>
                {
                    IsSucess = false,
                    Message = "Farm Inspector with this password or email already exists"
                };
            }
            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                PassWord = Guid.NewGuid().ToString().Substring(0, 10).Replace("-", "").ToUpper().Trim()
                
                
            };
            var role = await _roleRepository.GetRoleByNameReturningRoleObjectAsync("FarmInspector");
            var userRole = new UserRole
            {
                User = user,
                UserId = user.Id,
                Role = role,
                RoleId = role.Id
            };
            user.UserRole.Add(userRole);
            
            
            var farmInspector = new FarmInspector
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                State = model.State,
                Country = model.Country,
                LocalGoverment = model.LocalGovernment,
                UserName = model.UserName,
                Image = model.Image,
                UserId = user.Id,
                User = user,
                
            };
         
            var userInfo = await _userRepository.CreateAsync(user);
            var farmInspectorInfo = await _farmInspectorRepository.CreateAsync(farmInspector);
            var link = $"https://localhost:5001/api/User/Login/";
             _mailMessage.RegistrationNotificationForFarmInspector(farmInspectorInfo.Email , userInfo.PassWord , link);
            return new BaseResponse<FarmInspectorDto>
            {
                Message = "Farm Inspector Successfully created",
                IsSucess = true,
                Data = farmInspectorInfo
            };
        }

        public async Task<BaseResponse<FarmInspectorDto>> UpdateAsync(UpdateFarmInspectorRequestModel model, int id)
        {
            var farmInspector = await _farmInspectorRepository.GetFarmInspectorReturningFarmInspectorObjectAsync(id);
            var user = await _userRepository.GetUserReturningUserObjectAsync(farmInspector.UserId);
            if(farmInspector == null)
            {
                return new BaseResponse<FarmInspectorDto>
                {
                    IsSucess = false,
                    Message = "Farm Inspector Not Found"
                };
            } 

            user.FirstName = model.FirstName ?? user.FirstName;
            user.PhoneNumber = model.PhoneNumber ?? user.PhoneNumber;
            user.LastName = model.LastName ?? user.LastName;
            user.PassWord = model.PassWord ?? user.PassWord;
           

            farmInspector.State = model.State ?? farmInspector.State;
            farmInspector.Country = model.Country ?? farmInspector.Country;
            farmInspector.LocalGoverment = model.LocalGovernment ?? farmInspector.LocalGoverment;
            
            farmInspector.FirstName = model.FirstName ?? farmInspector.FirstName;
            farmInspector.LastName = model.LastName ?? farmInspector.LastName;
            farmInspector.PhoneNumber = model.PhoneNumber ?? farmInspector.PhoneNumber;
            farmInspector.UserName = model.UserName ?? farmInspector.UserName;

            await _userRepository.UpdateAsync(user);
            var farmInspectorInfo = await _farmInspectorRepository.UpdateAsync(farmInspector);
            _mailMessage.UpdateNotificationEmail(farmInspectorInfo.Email , "");
            return new BaseResponse<FarmInspectorDto>
            {
                Message = "Farm Inspector Sucessfully Updated",
                IsSucess = true,
                Data = farmInspectorInfo
            };

        }
    }
}