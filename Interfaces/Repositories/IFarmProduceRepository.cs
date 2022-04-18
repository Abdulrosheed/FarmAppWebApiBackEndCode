using System.Collections.Generic;
using System.Threading.Tasks;
using FirstProject.Dtos;
using FirstProject.Entities;

namespace FirstProject.Interfaces.Repositories
{
    public interface IFarmProduceRepository
    {
         Task<FarmProduceDto> CreateAsync (FarmProduce farmProduce);
        Task<FarmProduceDto> UpdateAsync (FarmProduce farmProduce);
        // void DeleteAsync (FarmProduce farmProduce);
        Task<IEnumerable<FarmProduceDto>> GetAllFarmProduceAsync ();
        Task<FarmProduceDto> GetFarmProduceReturningFarmProduceDtoObjectAsync (int id);
        Task<FarmProduce> GetFarmProduceReturningFarmProduceObjectAsync (int id);
        Task<IEnumerable<FarmProduce>> GetSelectedFarmProduce (IList<int> ids);
         Task<bool> ExistsByNameAsync (string name);
     
    }
}