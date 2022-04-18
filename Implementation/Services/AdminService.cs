using System.Collections.Generic;
using System.Threading.Tasks;
using FirstProject.Dtos;
using FirstProject.Entities;
using FirstProject.Interfaces.Repositories;
using FirstProject.Interfaces.Services;
using FirstProject.MailBox;

namespace FirstProject.Implementation.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMailMessage _mailMessage;

        public AdminService(IAdminRepository adminRepository , IUserRepository userRepository ,  IRoleRepository roleRepository , IMailMessage mailMessage)
        {
            _adminRepository = adminRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _mailMessage = mailMessage;
        }

        public async Task<BaseResponse<AdminDto>> DeleteAsync(int id)
        {
            if(!(await _adminRepository.ExistsByIdAsync(id)))
            {
                return new BaseResponse<AdminDto>
                {
                    IsSucess = false,
                    Message = "Admin Not Found"
                };
            }
            var admin = await _adminRepository.GetAdminReturningAdminObjectAsync(id);
            var user = await _userRepository.GetUserReturningUserObjectAsync(admin.UserId);
            user.IsDeleted = true;
            admin.IsDeleted = true;
            await _userRepository.UpdateAsync(user);
           await _adminRepository.UpdateAsync(admin);
            return new BaseResponse<AdminDto>
            {
                Message = "Admin Sucessfully Deleted",
                IsSucess = true
            };
        }

        public async Task<BaseResponse<AdminDto>> GetAdminAsync(int id)
        {
            if(!(await _adminRepository.ExistsByIdAsync(id)))
            {
                return new BaseResponse<AdminDto>
                {
                    IsSucess = false,
                    Message = "Admin Not Found"
                };
            }
            var admin = await _adminRepository.GetAdminReturningAdminDtoObjectAsync(id);
            return new BaseResponse<AdminDto>
            {
                IsSucess = true,
                Message = "Admin Sucessfully Retrieved",
                Data = admin
               
            };
      
            
        }

        public async Task<BaseResponse<AdminDto>> GetAdminByEmailAsync(string email)
        {
            var admin = await _adminRepository.GetAdminByEmailReturningAdminDtoObjectAsync(email);
            return new BaseResponse<AdminDto>
            {
                Message = "Admin Retrieved",
                IsSucess = true,
                Data = admin
            };
        }

        public async Task<BaseResponse<IEnumerable<AdminDto>>> GetAllAdminAsync()
        {
            var admins =  await _adminRepository.GetAllAdminAsync();
            if(admins == null)
            {
                return new BaseResponse<IEnumerable<AdminDto>>
                {
                    Message = "No Admin Found",
                    IsSucess = false,
                
                };  
            }
            return new BaseResponse<IEnumerable<AdminDto>>
            {
                Message = "Admins Sucessfully Retrieved",
                IsSucess = true,
                Data = await _adminRepository.GetAllAdminAsync()
            };
        }

        public async Task<BaseResponse<AdminDto>> RegisterAsync(CreateAdminRequestModel model)
        {
            bool exists = await _adminRepository.ExistsByEmailAsync(model.Email);
            if(exists)
            {
                return new BaseResponse<AdminDto>
                {
                    IsSucess = false,
                    Message = "Admin with this password or email already exists"
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
            var role = await _roleRepository.GetRoleByNameReturningRoleObjectAsync("Admin");
            var userRole = new UserRole
            {
                User = user,
                UserId = user.Id,
                Role = role,
                RoleId = role.Id
            };
            user.UserRole.Add(userRole);
            
            var admin = new Admin
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                UserName = model.UserName,
                Gender = model.Gender,
                State = model.State,
                Country = model.Country,
                LocalGoverment = model.LocalGovernment,
                Image = model.Image,
                UserId = user.Id,
                User = user
            };
         
            await _userRepository.CreateAsync(user);
            var adminInfo = await _adminRepository.CreateAsync(admin);
            _mailMessage.RegistrationNotificationEmail(adminInfo.Email , "");
            
            return new BaseResponse<AdminDto>
            {
                Message = "Admin Successfully created",
                IsSucess = true,
                Data = adminInfo
            };
        }

        public async Task<BaseResponse<AdminDto>> UpdateAsync(UpdateAdminRequestModel model, int id)
        {
            var admin = await _adminRepository.GetAdminReturningAdminObjectAsync(id);
            var user = await _userRepository.GetUserReturningUserObjectAsync(admin.UserId);
            if(admin == null || user == null)
            {
                return new BaseResponse<AdminDto>
                {
                    IsSucess = false,
                    Message = "Admin Not Found"
                };
            } 
            user.FirstName = model.FirstName ?? user.FirstName;
            user.PhoneNumber = model.PhoneNumber ?? user.PhoneNumber;
            user.LastName = model.LastName ?? user.LastName;
            user.PassWord = model.PassWord ?? user.PassWord;
            
            admin.State = model.State ?? admin.State;
            admin.LocalGoverment = model.LocalGovernment ?? admin.LocalGoverment;
            admin.Country = model.Country ?? admin.Country;
            
            admin.FirstName = model.FirstName ?? admin.FirstName;
            admin.LastName = model.LastName ?? admin.LastName;
            admin.PhoneNumber = model.PhoneNumber ?? admin.PhoneNumber;
            admin.UserName = model.UserName ?? admin.UserName;

            await _userRepository.UpdateAsync(user);
            var adminInfo = await _adminRepository.UpdateAsync(admin);
            _mailMessage.UpdateNotificationEmail(adminInfo.Email , "");
            return new BaseResponse<AdminDto>
            {
                Message = "Admin Sucessfully Updated",
                IsSucess = true,
                Data = adminInfo
            };

        }

    }
}