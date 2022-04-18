using System;
using System.Collections.Generic;
using FirstProject.Enums;

namespace FirstProject.Entities
{
    public class Order : BaseEntity
    {
        public string OrderReference { get; set; }

        public int CompanyId { get; set; }

        public Company Company { get; set; }

        public OrderStatus Status { get; set; }

     

        public DateTime? DeliveryDate { get; set; }

        public decimal TotalPrice { get; set; }

        public ICollection<OrderFarmProduct> OrderProducts { get; set; } = new List<OrderFarmProduct>();
    }
}