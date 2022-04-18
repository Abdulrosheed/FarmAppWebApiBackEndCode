using System.Collections.Generic;
using System.Threading.Tasks;
using FirstProject.Dtos;

namespace FirstProject.Interfaces.Services
{
    public interface ICompanyService
    {
        Task<BaseResponse<CompanyDto>> CreateAsync (CreateCompanyRequestModel model);
        Task<BaseResponse<CompanyDto>> UpdateAsync (UpdateCompanyRequestModel model , int id);
         Task<BaseResponse<CompanyDto>> DeleteAsync (int id);
        Task<BaseResponse<IEnumerable<CompanyDto>>> GetAllCompanyAsync ();
        Task<BaseResponse<CompanyDto>> GetCompanyReturningCompanyDtoObjectAsync (int id);
        Task<BaseResponse<CompanyDto>> GetCompanyByEmailReturningCompanyDtoObjectAsync (string email);
    }
}