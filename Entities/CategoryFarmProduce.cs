namespace FirstProject.Entities
{
    public class CategoryFarmProduce : BaseEntity
    {
      public int CategoryId {get;set;}
      public Category Category {get;set;}
        public int FarmProduceId {get;set;}
      public FarmProduce FarmProduce {get;set;}
    }
}