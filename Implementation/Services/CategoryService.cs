using System.Collections.Generic;
using System.Threading.Tasks;
using FirstProject.Dtos;
using FirstProject.Entities;
using FirstProject.Interfaces;
using FirstProject.Interfaces.Services;

namespace FirstProject.Implementation.Services
{
      
    public class CategoryService : ICategoryService
    {
         private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<BaseResponse<CategoryDto>> DeleteAsync(int id)
        {
            var category = await _categoryRepository.GetCategoryReturningCategoryObjectAsync(id);
             if(category == null)
            {
                return new BaseResponse<CategoryDto>
                {
                    IsSucess = false,
                    Message = "Category Not Found"
                };
            }
            category.IsDeleted = true; 
            await _categoryRepository.UpdateAsync(category);
            return new BaseResponse<CategoryDto>
            {
                Message = "Category Sucessfully Deleted",
                IsSucess = true
            };
        }

        public async Task<BaseResponse<IEnumerable<CategoryDto>>> GetAllCategoryAsync()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            if(categories == null)
            {
                return new BaseResponse<IEnumerable<CategoryDto>>
                {
                    Message = "No Categories has been created",
                    IsSucess = false,
                    
                };
            }
            return new BaseResponse<IEnumerable<CategoryDto>>
            {
                Message = "Categories Sucessfully Retrieved",
                IsSucess = true,
                Data = categories
            };
        }

        public async Task<BaseResponse<CategoryDto>> GetCategoryAsync(int id)
        {
            var category = await _categoryRepository.GetCategoryReturningCategoryDtoObjectAsync(id);
            if(category == null)
            {
                return new BaseResponse<CategoryDto>
                {
                    IsSucess = false,
                    Message = "Category Not Found"
                };
            }
            return new BaseResponse<CategoryDto>
            {
                Message = "Category Sucessfully Retrieved",
                IsSucess = true,
                Data = category
            };
        }

        public async Task<BaseResponse<CategoryDto>> RegisterAsync(CreateCategoryRequestModel model)
        {
            if((await _categoryRepository.ExistsByNameAsync(model.Name)))
            {
                return new BaseResponse<CategoryDto>
                {
                    Message = "Category already exists",
                    IsSucess = false
                };
            }
            var category = new Category
            {
              Name = model.Name,
              Description = model.Description  
            };
            var categoryInfo = await _categoryRepository.CreateAsync(category);
            return new BaseResponse<CategoryDto>
            {
                Message = "Category Sucessfully Created",
                IsSucess = true,
                Data = categoryInfo
            };
        }

        public async Task<BaseResponse<CategoryDto>> UpdateAsync(UpdateCategoryRequestModel model, int id)
        {
            var category = await _categoryRepository.GetCategoryReturningCategoryObjectAsync(id);
            if(category == null)
            {
                return new BaseResponse<CategoryDto>
                {
                    IsSucess = false,
                    Message = "Category Not Found"
                };
            }
            category.Name = model.Name ?? category.Name;
            category.Description = model.Description ?? category.Description;
            var categoryInfo = await _categoryRepository.UpdateAsync(category);
            return new BaseResponse<CategoryDto>
            {
                IsSucess = true,
                Message = "Category Updated Successfully",
                Data = categoryInfo
            };
        }
    }
}