using System.Collections.Generic;
using System.Threading.Tasks;
using FirstProject.Dtos;
using FirstProject.Entities;

namespace FirstProject.Interfaces
{
    public interface ICategoryRepository
    {
        Task<CategoryDto> CreateAsync (Category category);
        Task<CategoryDto> UpdateAsync (Category category);
        // void DeleteAsync (Category category);
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync ();
        Task<CategoryDto> GetCategoryReturningCategoryDtoObjectAsync (int id);
        Task<Category> GetCategoryReturningCategoryObjectAsync (int id);
        Task<IEnumerable<Category>> GetSelectedCategory (IList<int> ids);
         Task<bool> ExistsByNameAsync (string name);
    }
}