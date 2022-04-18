using System.Collections.Generic;
using System.Threading.Tasks;
using FirstProject.Dtos;
using FirstProject.Entities;

namespace FirstProject.Interfaces.Repositories
{
    public interface IFarmInspectorRepository
    {
        Task<FarmInspectorDto> CreateAsync (FarmInspector farmInspector);
        Task<FarmInspectorDto> UpdateAsync (FarmInspector farmInspector);
        // void DeleteAsync (FarmInspector farmInspector);
        Task<IEnumerable<FarmInspectorDto>> GetAllFarmInspectorAsync ();
        Task<FarmInspectorDto> GetFarmerReturningFarmInspectorDtoObjectAsync (int id);
        Task<FarmInspector> GetFarmInspectorReturningFarmInspectorObjectAsync (int id);
         Task<FarmInspectorDto> GetFarmInspectorReturningFarmInspectorDtoObjectAsync (string email);
        Task<FarmInspector> GetFarmInspectorReturningFarmInspectorObjectAsync (string email);
        Task<IList<FarmInspectorDto>> GetFarmInspectorByStateReturningFarmInspectorObjectDtoAsync (string state);
        Task<IList<FarmInspectorDto>> GetFarmInspectorByCountryReturningFarmInspectorObjectDtoAsync (string country);
        Task<IList<FarmInspectorDto>> GetFarmInspectorByLocalGovernmentReturningFarmInspectorObjectDtoAsync (string localGoverment);
       
        Task<bool> ExistsByEmailAsync (string email);
        Task<bool> ExistsByIdAsync (int id);
    }
}