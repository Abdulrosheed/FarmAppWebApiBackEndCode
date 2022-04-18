using System.Collections.Generic;
using System.Threading.Tasks;
using FirstProject.Dtos;
using FirstProject.Entities;
using FirstProject.Interfaces.Repositories;
using FirstProject.Interfaces.Services;

namespace FirstProject.Implementation.Services
{
    public class EmailService : IEmailService
    {
        private readonly IEmailRepository _emailRepository;

        public EmailService(IEmailRepository emailRepository)
        {
            _emailRepository = emailRepository;
        }

        public async Task<BaseResponse<EmailDto>> CreateAsync(CreateEmailRequestModel model)
        {
            var email = new Email
            {
                Subject = model.Subject,
                Content = model.Content,
                EmailType = model.EmailType
            };
            var emailInfo = await _emailRepository.CreateAsync(email);
            return new BaseResponse<EmailDto>
            {
                Message = "Email successfully created",
                IsSucess = true,
                Data = emailInfo
            };
        }

        public async Task<BaseResponse<EmailDto>> DeleteAsync(int id)
        {
            var email = await _emailRepository.GetEmailByIdReturningEmailObjectAsync(id);
            email.IsDeleted = true;
            await _emailRepository.UpdateAsync(email);
            return new BaseResponse<EmailDto>
            {
                Message = "Email successfully Deleted",
                IsSucess = true
            };
        }

        public async Task<BaseResponse<IList<EmailDto>>> GetAllAsync()
        {
            var email = await _emailRepository.GetAllAsync();
            if(email == null)
            {
                return new BaseResponse<IList<EmailDto>>
                {
                    Message = "No Email has been created",
                    IsSucess = false,
                };
            }
            return new BaseResponse<IList<EmailDto>>
            {
                Message = "Emails successfully retrieved",
                IsSucess = true,
                Data = await _emailRepository.GetAllAsync()
                
            };
        }

        public async Task<BaseResponse<EmailDto>> GetEmailByEmailTypeAsync(EmailType emailType)
        {
            var email = await _emailRepository.GetEmailByEmailTypeReturningEmailObjectDtoAsync(emailType);
            if(email == null)
            {
                return new BaseResponse<EmailDto>
                {
                    Message = "Email not found",
                    IsSucess = true,

                };
            }
            return new BaseResponse<EmailDto>
            {
                Message = "Email successfully retrieved",
                IsSucess = true,
                Data = email
            };
        }

        public async Task<BaseResponse<EmailDto>> GetEmailByIdAsync(int id)
        {
            var email = await _emailRepository.GetEmailByIdReturningEmailObjectDtoAsync(id);
            if(email == null)
            {
                return new BaseResponse<EmailDto>
                {
                    Message = "Email not found",
                    IsSucess = false,
                };
            }

            return new BaseResponse<EmailDto>
            {
                Message = "Email successfully retrieved",
                IsSucess = true,
                Data = email
            };
        }

        public async Task<BaseResponse<EmailDto>> UpdateAsync(UpdateEmailRequestModel model, int id)
        {
            var email = await _emailRepository.GetEmailByIdReturningEmailObjectAsync(id);
            email.Content = model.Content ?? email.Content;
            email.Subject = model.Subject ?? email.Subject;
            var emailInfo = await _emailRepository.UpdateAsync(email);
            return new BaseResponse<EmailDto>
            {
                Message = "Email successfully updated",
                IsSucess = true,
                Data = emailInfo
            };
          

        }
    }
}