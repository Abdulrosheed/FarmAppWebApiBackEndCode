using System.Collections.Generic;

namespace FirstProject.Entities
{
    public class FarmProduce : BaseEntity
    {
        public string Name {get;set;}
        public string Description {get;set;}
        public IList<CategoryFarmProduce> CategoryFarmProduce {get;set;} = new List<CategoryFarmProduce>();
        public IList<FarmProduceFarm> FarmProduceFarm {get;set;} = new List<FarmProduceFarm>();
        public IList<Request> Requests {get;set;} = new List<Request>();
    }
}