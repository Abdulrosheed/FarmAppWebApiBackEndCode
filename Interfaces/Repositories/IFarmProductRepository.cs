using System.Collections.Generic;
using System.Threading.Tasks;
using FirstProject.Entities;

namespace FirstProject.Interfaces.Repositories
{
    public interface IFarmProductRepository
    {
         
        Task<FarmProductDto> CreateAsync (FarmProduct farmProduct);
        Task<FarmProductDto> UpdateAsync (FarmProduct farmProduct);
        // void DeleteAsync ( FarmProduct farmProduct);
        Task<IEnumerable<FarmProductDto>> GetAllFarmProductsAsync ();
        Task<IEnumerable<FarmProductDto>> GetAllFarmProductsInspectedByFarmInspectorAsync (string email);
        Task<IEnumerable<FarmProductDto>> Search (string searchText);
        
        Task<FarmProductDto> GetFarmProductByIdAsync (int id);
        Task<IList<SearchRequestFarmProductDto>> GetFarmProductByRequestAsync (GetFarmProductByRequestModel model);
        Task<IEnumerable<FarmProductDto>> GetFarmProductsByStateReturningFarmProductDtoObjectAsync (string state);
        Task<IEnumerable<FarmProduct>> GetSelectedFarmProducts (IDictionary<int , int> items);
        Task<IEnumerable<FarmProductDto>> GetFarmProductsByLocalGovernmentReturningFarmProductDtoObjectAsync (string localGoverment);
        Task<FarmProduct> GetFarmProductReturningFarmProductObjectAsync (int id);
        Task<FarmProduct> GetFarmProductByFarmIdReturningFarmProductObjectAsync (int farmId);
        Task<IList<FarmProductDto>> GetFarmProductByFarmerEmailReturningFarmProductObjectAsync (string email);
        Task<List<FarmProduct>> GetAllFarmProductObjectAsync ();
        Task<IList<FarmProductDto>> GetSoldFarmProductByFarmerAsync (string email);
        Task<IList<FarmProductDto>> GetNotSoldFarmProductByFarmerAsync (string email);
        Task<IList<FarmProductDto>> GetToBeUpdatedFarmProductByFarmerEmailReturningFarmProductObjectAsync (string email);
         Task<IList<FarmProductDto>> GetToBeUpdatedFarmProductByFarmerInspectorReturningFarmProductObjectAsync(string email);
         Task<FarmProduct> GetFarmProductByNameAndFarmId (int farmId , string farmProduct);
      
        
    }
}