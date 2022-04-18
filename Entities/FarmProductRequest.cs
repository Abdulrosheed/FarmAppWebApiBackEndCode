namespace FirstProject.Entities
{
    public class FarmProductRequest : BaseEntity
    {
        public int RequestId {get;set;}
        public Request Request {get;set;}
        public int FarmProductId {get;set;}
        public FarmProduct FarmProduct {get;set;}
    }
}