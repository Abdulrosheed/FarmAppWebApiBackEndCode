using System.Collections.Generic;
using System.Threading.Tasks;
using FirstProject.Dtos;
using FirstProject.Entities;

namespace FirstProject.Interfaces.Repositories
{
    public interface IEmailRepository
    {
        Task<EmailDto> CreateAsync (Email email);
        Task<EmailDto> UpdateAsync (Email email);
        // void DeleteAsync (Email email);
        Task<IList<EmailDto>> GetAllAsync ();
        Task<Email> GetEmailByIdReturningEmailObjectAsync (int id);
        Task<EmailDto> GetEmailByIdReturningEmailObjectDtoAsync (int id);
        Task<EmailDto> GetEmailByEmailTypeReturningEmailObjectDtoAsync (EmailType emailType);

    }
}