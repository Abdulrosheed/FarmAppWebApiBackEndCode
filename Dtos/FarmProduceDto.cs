using System.Collections.Generic;

namespace FirstProject.Dtos
{
    public class FarmProduceDto
    {
        public int Id {get;set;}
         public string Name {get;set;}
        public string Description {get;set;}
        public IList<CategoryDto> Categories {get;set;} = new List<CategoryDto>();
        public IList<FarmDto> Farms {get;set;} = new List<FarmDto>();
    }
    public class CreateFarmProduceRequestModel
    {
         public string Name {get;set;}
        public string Description {get;set;}
        public IList<int> CategoriesIds {get;set;} = new List<int>();
    }
    public class UpdateFarmProduceRequestModel
    {
         public string Name {get;set;}
        public string Description {get;set;}
    }
}