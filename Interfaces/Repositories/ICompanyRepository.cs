using System.Collections.Generic;
using System.Threading.Tasks;
using FirstProject.Dtos;
using FirstProject.Entities;

namespace FirstProject.Interfaces.Repositories
{
    public interface ICompanyRepository
    {
        Task<CompanyDto> CreateAsync (Company company);
        Task<CompanyDto> UpdateAsync (Company company);
        // void DeleteAsync (Company company);
        Task<IEnumerable<CompanyDto>> GetAllCompanyAsync ();
        Task<CompanyDto> GetCompanyReturningCompanyDtoObjectAsync (int id);
        Task<CompanyDto> GetCompanyByEmailReturningCompanyDtoObjectAsync (string email);
        Task<Company> GetCompanyReturningCompanyObjectAsync (int id);
        Task<bool> ExistsByEmailAsync (string email);
       
        Task<bool> ExistsByIdAsync (int id);
    }
}