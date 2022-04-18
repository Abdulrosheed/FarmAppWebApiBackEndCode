using System;
using System.Collections.Generic;
using FirstProject.Dtos;

namespace FirstProject
{
    public class FarmProductDto
    {
        public int FarmId { get; set; }
        public int Id { get; set; }
        
        public string State { get; set; }
        public string Country { get; set; }
        public string LocalGovernment { get; set; }
        public string FarmerEmail {get;set;}
        public int FarmerId {get;set;}
        public int FarmInspectorId {get;set;}
        public string FarmProduct { get; set; }
        public int Quantity { get; set; }
        public decimal Price {get;set;}
        public string ProductImage1 {get;set;}
        public string ProductImage2 {get;set;}
        public string Grade { get; set; }
        public DateTime HarvestedTime {get;set;}
        public string FarmProductStatus { get; set; }
         public ICollection<OrderDto> OrderProducts { get; set; } = new List<OrderDto>();
         public ICollection<CartDto> FarmProductsCarts { get; set; } = new List<CartDto>();
    }
    public class SearchRequestFarmProductDto
    {
        public int Id {get;set;}
        public decimal Price {get;set;}
        public int Quantity {get;set;}
        public string FarmerName {get;set;}
        public int FarmerId {get;set;}
        public string FarmerEmail {get;set;}
        public string FarmName {get;set;}
        public string FarmState {get;set;}
        public string FarmCountry {get;set;}
        public string FarmCity {get;set;}
        public bool PullResources {get;set;}
    }
    public class UpdateFarmProductForAdminRequestModel
    {
        public int Quantity { get; set; }
        public FarmGrade Grade {get;set;}
        public int Id {get;set;}
        public int HarvestedTime {get;set;}
        public int FarmId {get;set;}
        public int FarmerId {get;set;}
        public int FarmInspectorId {get;set;}
        public string FarmProduct {get;set;}
    }
    public class UpdateFarmProductForFarmerRequestModel
    {
       public decimal price {get;set;}
       public int Id{get;set;}
    }
    public class GetFarmProductByRequestModel
    {
        public GetFarmProductByRequestModel(int yearNeeded, int monthNeeded, FarmGrade grade, string farmProduce, int quantity)
        {
            YearNeeded = yearNeeded;
            MonthNeeded = monthNeeded;
            Grade = grade;
            FarmProduce = farmProduce;
            Quantity = quantity;
        }

        public int YearNeeded { get; set; }
        public int MonthNeeded { get; set; }
        public FarmGrade Grade { get; set; }
        public string FarmProduce { get; set; }
        public int Quantity { get; set; }
   
    }
     public class GetFarmProductByLocalGovernmentRequestModel
    {

        public string LocalGovernment { get; set; }
        public int Quantity { get; set; }
        public FarmGrade Grade { get; set; }
        public string FarmProduce { get; set; }
    }
}