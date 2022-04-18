using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FirstProject.Dtos;
using FirstProject.Entities;

namespace FirstProject.Interfaces.Repositories
{
    public interface IFarmRepository
    {
         Task<FarmDto> CreateAsync (Farm farm);
        Task<FarmDto> UpdateAsync (Farm farm);
        // void DeleteAsync (Farm farm);
        Task<IEnumerable<FarmDto>> GetAllFarmAsync ();
        Task<IEnumerable<FarmDto>> GetAllProcessingFarmAsync ();
        Task<IEnumerable<FarmDto>> GetAllDeclinedFarmAsync ();
        Task<IEnumerable<FarmDto>> GetAllDeclinedFarmByFarmerAsync (string email);
        Task<IEnumerable<FarmDto>> GetAllAssignedFarmAsync ();
        Task<IEnumerable<FarmDto>> GetAllAssignedFarmByFarmerAsync (string email);
        Task<FarmDto> GetFarmReturningFarmDtoObjectAsync (int id);
        Task<Farm> GetFarmReturningFarmObjectAsync (int id);
             Task<IList<FarmDto>> GetAllInspectedFarmerByFarmInspectorIdAsync (string farmInspectorEmail);
       Task<IList<FarmDto>> GetTobeInspectedFarmerByFarmInspectorIdAsync (string farmInspectorEmail);
    //    Task<IList<Farm>> GetFarmByInspectionDate (DateTime date);
          Task<bool> ExistsByNameAsync (string name);
    }
}