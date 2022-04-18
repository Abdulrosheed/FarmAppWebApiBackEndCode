using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstProject.Context;
using FirstProject.Dtos;
using FirstProject.Entities;
using FirstProject.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FirstProject.Implementation.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationContext _context;

        public CategoryRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<CategoryDto> CreateAsync(Category category)
        {
           await _context.Categories.AddAsync(category);
           await _context.SaveChangesAsync();
           return new CategoryDto
           {
               Name = category.Name,
               Id = category.Id,
               Description = category.Description,
               FarmProduces = category.CategoryFarmProduce.Select(a => new FarmProduceDto{

               }).ToList()
           };
        }

        // public async void DeleteAsync(Category category)
        // {
        //     _context.Categories.Remove(category);
        //     await _context.SaveChangesAsync();
        // }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
           var categories = await _context.Categories.Include(a => a.CategoryFarmProduce).ThenInclude(a => a.FarmProduce).Where(a => a.IsDeleted == false).ToListAsync();
           if(categories == null)
           {
               return null;
           }
           return categories.Select(a => new CategoryDto
           {
                Name = a.Name,
               Id = a.Id,
               Description = a.Description,
               FarmProduces = a.CategoryFarmProduce.Select(a => new FarmProduceDto
               {

               }).ToList()
           }).AsEnumerable();
        }

        public async Task<Category> GetCategoryReturningCategoryObjectAsync(int id)
        {
            var category = await _context.Categories.SingleOrDefaultAsync(a => a.Id == id && a.IsDeleted == false);
            if(category == null)
            {
                return null;
            }
            return category;
        }

        public async Task<CategoryDto> GetCategoryReturningCategoryDtoObjectAsync(int id)
        {
            var category = await _context.Categories.Include(a => a.CategoryFarmProduce).ThenInclude(a => a.FarmProduce).FirstOrDefaultAsync(a => a.Id == id && a.IsDeleted == false);
            if(category == null)
            {
                return null;
            }
            return new CategoryDto
            {
                Name = category.Name,
               Id = category.Id,
               Description = category.Description,
               FarmProduces = category.CategoryFarmProduce.Select(a => new FarmProduceDto{

               }).ToList() 
        
            };
        }

        public async Task<IEnumerable<Category>> GetSelectedCategory(IList<int> ids)
        {
            return await _context.Categories.Where(a => ids.Contains(a.Id) && a.IsDeleted == false).ToListAsync();
        }

        public async Task<CategoryDto> UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return new CategoryDto
            {
                Name = category.Name,
               Id = category.Id,
               Description = category.Description,
              
            };

        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
             return await _context.Categories.AnyAsync(a => a.Name == name && a.IsDeleted == false);
        }

       
    }
}