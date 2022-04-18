namespace FirstProject.Entities
{
    public class OrderFarmProduct : BaseEntity
    {
          public int OrderId { get; set; }

        public Order Order { get; set; }

        public int FarmProductId { get; set; }

        public FarmProduct FarmProduct { get; set; }

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }
    }
}