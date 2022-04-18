using System;
using System.Collections.Generic;
using FirstProject.Enums;

namespace FirstProject.Entities
{
    public class FarmReport : BaseEntity
    {
        public int FarmInspectorId {get;set;}
        public FarmInspector FarmInspector {get;set;}
       public DateTime HarvestedPeriod {get;set;}
        public int FarmerId {get;set;}
        public Farmer Farmer {get;set;}
         public int FarmId {get;set;}
        public Farm Farm {get;set;}
        public string FarmProduct {get;set;}
        public int Quantity {get;set;}
        public string ProductImage1 {get;set;}
        public string ProductImage2 {get;set;}
        public FarmReportStatus FarmReportStatus {get;set;} 
        public FarmGrade Grade {get;set;}
        public IList<FarmProduct> FarmProducts {get;set;}
        
        
        
    }
}