namespace FirstProject.Entities
{
    public class FarmProduceFarm : BaseEntity
    {
        public int FarmProduceId {get;set;}
        public FarmProduce FarmProduce {get;set;}
        public int FarmId {get;set;}
        public Farm Farm {get;set;}
    }
}