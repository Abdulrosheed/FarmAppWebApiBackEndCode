using System.Collections.Generic;

namespace FirstProject.Entities
{
    public class Category : BaseEntity
    {
        public string Name {get;set;}
        public string Description {get;set;}
        public IList<CategoryFarmProduce> CategoryFarmProduce {get;set;} = new List<CategoryFarmProduce>();
    }
}