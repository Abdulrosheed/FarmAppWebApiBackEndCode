using System.Collections.Generic;
using System.Threading.Tasks;
using FirstProject.Dtos;

namespace FirstProject.Interfaces.Services
{
    public interface IEmailService
    {
        Task<BaseResponse<EmailDto>> CreateAsync (CreateEmailRequestModel model);
        Task<BaseResponse<EmailDto>> UpdateAsync (UpdateEmailRequestModel model , int id);
        Task<BaseResponse<EmailDto>> DeleteAsync (int id);
        Task<BaseResponse<IList<EmailDto>>> GetAllAsync ();
      
        Task<BaseResponse<EmailDto>> GetEmailByIdAsync (int id);
        Task<BaseResponse<EmailDto>> GetEmailByEmailTypeAsync (EmailType emailType);
    }
}