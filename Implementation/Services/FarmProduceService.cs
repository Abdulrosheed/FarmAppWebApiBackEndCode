using System.Collections.Generic;
using System.Threading.Tasks;
using FirstProject.Dtos;
using FirstProject.Entities;
using FirstProject.Interfaces;
using FirstProject.Interfaces.Repositories;
using FirstProject.Interfaces.Services;

namespace FirstProject.Implementation.Services
{
   
    public class FarmProduceService : IFarmProduceService
    {
         private readonly ICategoryRepository _categoryRepository;
        private readonly IFarmProduceRepository _farmProduceRepository;

        public FarmProduceService(ICategoryRepository categoryRepository, IFarmProduceRepository farmProduceRepository)
        {
            _categoryRepository = categoryRepository;
            _farmProduceRepository = farmProduceRepository;
        }

        public async Task<BaseResponse<FarmProduceDto>> DeleteAsync(int id)
        {
            var farmProduce = await _farmProduceRepository.GetFarmProduceReturningFarmProduceObjectAsync(id);
            if(farmProduce == null)
            {
                return new BaseResponse<FarmProduceDto>
                {
                    IsSucess = false,
                    Message = "Farm Produce Not Found"
                };
            }
              farmProduce.IsDeleted = true;
            await _farmProduceRepository.UpdateAsync(farmProduce);
            return new BaseResponse<FarmProduceDto>
            {
                Message = "Farm Produce Sucessfully Deleted",
                IsSucess = true
            };

        }

        public async Task<BaseResponse<IEnumerable<FarmProduceDto>>> GetAllFarmProduceAsync()
        {
            var farmProduce = await _farmProduceRepository.GetAllFarmProduceAsync();
            if(farmProduce == null)
            {
                return new BaseResponse<IEnumerable<FarmProduceDto>>
                {
                    Message = "No farm produce has been created",
                    IsSucess = false
                };
            }
            return new BaseResponse<IEnumerable<FarmProduceDto>>
            {
                Message = "Farm Produce Sucessfully Retrieved",
                IsSucess = true,
                Data = farmProduce
            };
        }

        public async Task<BaseResponse<FarmProduceDto>> GetFarmProduceAsync(int id)
        {
            var farmProduce = await _farmProduceRepository.GetFarmProduceReturningFarmProduceDtoObjectAsync(id);
            if(farmProduce == null)
            {
                return new BaseResponse<FarmProduceDto>
                {
                    IsSucess = false,
                    Message = "Farm Produce Not Found"
                };
            }
            return new BaseResponse<FarmProduceDto>
            {
                Message = "Farm Produce Sucessfully Retrieved",
                IsSucess = true,
                Data = farmProduce
            };
        }

        public async Task<BaseResponse<FarmProduceDto>> RegisterAsync(CreateFarmProduceRequestModel model)
        {
          if((await _farmProduceRepository.ExistsByNameAsync(model.Name)))
            {
                return new BaseResponse<FarmProduceDto>
                {
                    Message = "Farm Produce already exists",
                    IsSucess = false
                };
            }
            var farmProduce = new FarmProduce
            {
              Name = model.Name,
              Description = model.Description  
            };

            var categories = await _categoryRepository.GetSelectedCategory(model.CategoriesIds);
            foreach(var item in categories)
            {
                var categoryFarmProduce = new CategoryFarmProduce
                {
                    Category = item,
                    CategoryId = item.Id,
                    FarmProduce = farmProduce,
                    FarmProduceId = farmProduce.Id
                };
                farmProduce.CategoryFarmProduce.Add(categoryFarmProduce);
            }
            var farmProduceInfo = await _farmProduceRepository.CreateAsync(farmProduce);
            return new BaseResponse<FarmProduceDto>
            {
                Message = "Farm Produce Sucessfully Created",
                IsSucess = true,
                Data = farmProduceInfo
            };
        }

        public async Task<BaseResponse<FarmProduceDto>> UpdateAsync(UpdateFarmProduceRequestModel model, int id)
        {
            var farmProduce = await _farmProduceRepository.GetFarmProduceReturningFarmProduceObjectAsync(id);
            if(farmProduce == null)
            {
                return new BaseResponse<FarmProduceDto>
                {
                    IsSucess = false,
                    Message = "Farm Produce Not Found"
                };
            }
            farmProduce.Name = model.Name ?? farmProduce.Name;
            farmProduce.Description = model.Description ?? farmProduce.Description;
            var farmProduceInfo = await _farmProduceRepository.UpdateAsync(farmProduce);
            return new BaseResponse<FarmProduceDto>
            {
                IsSucess = true,
                Message = "Farm Produce Updated Successfully",
                Data = farmProduceInfo
            };
        }
    }
}