using System;
using System.Collections.Generic;
using FirstProject.Enums;

namespace FirstProject.Dtos
{
    public class OrderDto
    {
        public int Id {get;set;}
        public string OrderReference { get; set; }

        public string CompanyEmail { get; set; }

        public string CompanyName { get; set; }

        public string Status { get; set; }

      

        public DateTime DeliveryDate { get; set; }

        public decimal TotalPrice { get; set; }

        public ICollection<FarmProductDto> OrderProducts { get; set; } = new List<FarmProductDto>();
    }
    public class CreateOrderRequestModel
    {
        public int RequestId { get; set; }
        public List<int> FarmProductId { get; set; }

        // public IEnumerable<Cart> OrderItems { get; set; }

       

    }
    // public class Cart
    // {
    //     public int ItemId { get; set; }

    //     public int Quantity { get; set; }
    // }
    public class UpdateOrderRequestModel
    {
        public int Id {get;set;}
        public OrderStatus Status { get; set; }
        public DateTime? DeliveryDate { get; set; }
    }
   
}