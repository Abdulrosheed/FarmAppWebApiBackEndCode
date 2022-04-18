using System;
using System.Collections.Generic;

namespace FirstProject.Entities
{
    public class FarmProduct : BaseEntity
    {
        public DateTime HarvestedPeriod {get;set;}
        public int FarmId {get;set;}
        public  Farm Farm {get;set;}
        public int FarmerId {get;set;}
        public  Farmer Farmer {get;set;}
        public int FarmReportId {get;set;}
        public FarmReport FarmReport {get;set;}
        public string State {get;set;}
        public string Country {get;set;}
        public string LocalGoverment {get;set;}
        public string FarmProduce {get;set;}
        public int Quantity {get;set;}
        public decimal? Price {get;set;}
        public string ProductImage1 {get;set;}
        public string ProductImage2 {get;set;}
        public FarmGrade Grade {get;set;}
        public FarmProductStatus FarmProductStatus {get;set;}
        
        public ICollection<OrderFarmProduct> OrderProducts { get; set; } = new List<OrderFarmProduct>();
        public ICollection<FarmProductRequest> FarmProductRequests { get; set; } = new List<FarmProductRequest>();
        
      
        
    }
}