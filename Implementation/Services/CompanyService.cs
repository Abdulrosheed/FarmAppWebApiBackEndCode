using System.Collections.Generic;
using System.Threading.Tasks;
using FirstProject.Dtos;
using FirstProject.Entities;
using FirstProject.Interfaces.Repositories;
using FirstProject.Interfaces.Services;
using FirstProject.MailBox;

namespace FirstProject.Implementation.Services
{
    public class CompanyService : ICompanyService
    {
         private readonly ICompanyRepository _companyRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMailMessage _mailMessage;


        public CompanyService(ICompanyRepository companyRepository,   IUserRepository userRepository , IRoleRepository roleRepository , IMailMessage mailMessage)
        {
            _companyRepository = companyRepository;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _mailMessage = mailMessage;
        }

        public async Task<BaseResponse<CompanyDto>> CreateAsync(CreateCompanyRequestModel model)
        {
             if(await _companyRepository.ExistsByEmailAsync(model.Email))
            {
                
                return new BaseResponse<CompanyDto>
                {
                    Message = "Company with this email already exists",
                    IsSucess = true
                };
            }
              var user = new User
            {
                FirstName = model.Name,
                LastName = model.Name,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                PassWord = model.PassWord
                
                
            };
            var role = await _roleRepository.GetRoleByNameReturningRoleObjectAsync("Company");
            var userRole = new UserRole
            {
                User = user,
                UserId = user.Id,
                Role = role,
                RoleId = role.Id
            };
            user.UserRole.Add(userRole);
            
            var company = new Company
            {
                Name = model.Name,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                UserName = model.UserName,
                State = model.State,
                Country = model.Country,
                LocalGoverment = model.LocalGovernment,
                Image = model.Image,
                UserId = user.Id,
                User = user,
                
            };
         
            await _userRepository.CreateAsync(user);
            var companyInfo = await _companyRepository.CreateAsync(company);
            _mailMessage.RegistrationNotificationEmail(companyInfo.Email , "");
            
            return new BaseResponse<CompanyDto>
            {
                Message = "company Successfully created",
                IsSucess = true,
                Data = companyInfo
            };
        }

        public async  Task<BaseResponse<CompanyDto>> DeleteAsync(int id)
        {
             if(!(await _companyRepository.ExistsByIdAsync(id)))
            {
                return new BaseResponse<CompanyDto>
                {
                    IsSucess = false,
                    Message = "Company Not Found"
                };
            }
            var company = await _companyRepository.GetCompanyReturningCompanyObjectAsync(id);
            var user = await _userRepository.GetUserReturningUserObjectAsync(company.UserId);
            user.IsDeleted = true;
            company.IsDeleted = true;
            await _userRepository.UpdateAsync(user);
            await _companyRepository.UpdateAsync(company);
            return new BaseResponse<CompanyDto>
            {
                Message = "Company Sucessfully Deleted",
                IsSucess = true
            };
        }

        public async Task<BaseResponse<IEnumerable<CompanyDto>>> GetAllCompanyAsync()
        {
           var companies =  await _companyRepository.GetAllCompanyAsync();
            if(companies == null)
            {
                return new BaseResponse<IEnumerable<CompanyDto>>
                {
                    Message = "No Company Found",
                    IsSucess = false,
                
                };  
            }
            return new BaseResponse<IEnumerable<CompanyDto>>
            {
                Message = "Companies Sucessfully Retrieved",
                IsSucess = true,
                Data = await _companyRepository.GetAllCompanyAsync()
            };
        }

        public async Task<BaseResponse<CompanyDto>> GetCompanyByEmailReturningCompanyDtoObjectAsync(string email)
        {
            var company =  await _companyRepository.GetCompanyByEmailReturningCompanyDtoObjectAsync(email);
            if(company == null)
            {
                return new BaseResponse<CompanyDto>
                {
                    Message = "No Company Found",
                    IsSucess = false,
                
                };  
            }
            return new BaseResponse<CompanyDto>
            {
                Message = "Company Sucessfully Retrieved",
                IsSucess = true,
                Data = company
            };
        }

        public async Task<BaseResponse<CompanyDto>> GetCompanyReturningCompanyDtoObjectAsync(int id)
        {
            var company =  await _companyRepository.GetCompanyReturningCompanyDtoObjectAsync(id);
            if(company == null)
            {
                return new BaseResponse<CompanyDto>
                {
                    Message = "No Company Found",
                    IsSucess = false,
                
                };  
            }
            return new BaseResponse<CompanyDto>
            {
                Message = "Company Sucessfully Retrieved",
                IsSucess = true,
                Data = company
            };
        }

        public async Task<BaseResponse<CompanyDto>> UpdateAsync(UpdateCompanyRequestModel model , int id)
        {
            var company = await _companyRepository.GetCompanyReturningCompanyObjectAsync(id);
            var user = await _userRepository.GetUserReturningUserObjectAsync(company.UserId);
            if(company == null)
            {
                return new BaseResponse<CompanyDto>
                {
                    IsSucess = false,
                    Message = "Company Not Found"
                };
            } 

            user.FirstName = model.Name ?? user.FirstName;
            user.PhoneNumber = model.PhoneNumber ?? user.PhoneNumber;
            user.LastName = model.Name ?? user.LastName;
            user.PassWord = model.PassWord ?? user.PassWord;
           
            company.State = model.State ?? company.State;
            company.LocalGoverment = model.LocalGovernment ?? company.LocalGoverment;
            company.Country = model.Country ?? company.Country;
           
            company.Name = model.Name ?? company.Name;
            company.PhoneNumber = model.PhoneNumber ?? company.PhoneNumber;
            company.UserName = model.UserName ?? company.UserName;

            await _userRepository.UpdateAsync(user);
            var companyInfo = await _companyRepository.UpdateAsync(company);
            _mailMessage.UpdateNotificationEmail(companyInfo.Email , "");
            return new BaseResponse<CompanyDto>
            {
                Message = "Company Sucessfully Updated",
                IsSucess = true,
                Data = companyInfo
            };

        }
    }
}