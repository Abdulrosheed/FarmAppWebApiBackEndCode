using System.Collections.Generic;

namespace FirstProject.Dtos
{
    public class CategoryDto
    {
        public int Id {get;set;}
          public string Name {get;set;}
        public string Description {get;set;}
        public IList<FarmProduceDto> FarmProduces {get;set;} = new List<FarmProduceDto>();
    }
    public class CreateCategoryRequestModel
    {
        
        public string Name {get;set;}
        public string Description {get;set;}
    }
     public class UpdateCategoryRequestModel
    {
        
        public string Name {get;set;}
        public string Description {get;set;}
    }
}