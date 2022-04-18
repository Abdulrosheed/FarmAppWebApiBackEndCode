using System.Collections.Generic;
using System.Threading.Tasks;
using FirstProject.Dtos;
using FirstProject.Entities;
using FirstProject.Enums;

namespace FirstProject.Interfaces.Repositories
{
    public interface IFarmerRepository
    {
        Task<FarmerDto> CreateAsync (Farmer farmer);
        Task<FarmerDto> UpdateAsync (Farmer farmer);
        // void DeleteAsync (Farmer farmer);
        Task<IEnumerable<FarmerDto>> GetAllFarmersAsync ();
        Task<FarmerDto> GetFarmerReturningFarmerDtoObjectAsync (int id);
        Task<Farmer> GetFarmerReturningFarmerObjectAsync (int id);
         Task<FarmerDto> GetFarmerReturningFarmerDtoObjectAsync (string email);
        Task<Farmer> GetFarmerReturningFarmerObjectAsync (string email);
        Task<bool> ExistsByEmailAsync (string email);
  
       Task<IList<FarmerDto>> AllProcessingFarmerAprovalAsync ();
       Task<IList<FarmerDto>> AllDeclinedFarmerAprovalAsync ();
       Task<IList<FarmerDto>> AllAssignedFarmerAprovalAsync ();
        Task<bool> ExistsByIdAsync (int id);
    }
}