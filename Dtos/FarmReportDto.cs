using System;

namespace FirstProject.Dtos
{
    public class FarmReportDto
    {
        public int Id {get;set;}
         public int FarmInspectorId {get;set;}
         public string FarmInspectorEmail {get;set;}
         public string FarmerEmail {get;set;}
        
        public int FarmId {get;set;}
        public string FarmProduct {get;set;}
        public int Quantity {get;set;}
        public string Grade {get;set;}
        public int HarvestedTime {get;set;}
        public string State {get;set;}
        public string LocalGovernment {get;set;}
        public string Country {get;set;}
    }
    public class CreateFarmReportRequestModel
    {
        public string FarmProduct {get;set;}
        public int Quantity {get;set;}
        public int Grade {get;set;}
        public int FarmId {get;set;}
        public int FarmerId {get;set;}
        public int ToBeHarvestedTime {get;set;}
     
       
    }
    public class UpdateFarmReportRequestModel
    {
        public string FarmProduct {get;set;}
        public int Quantity {get;set;}
        public FarmGrade Grade {get;set;}
        public int FarmId {get;set;}
        public int FarmerId {get;set;}
        public int ToBeHarvestedTime {get;set;}
    
    }
    public class SendFarmProductPics
    {
        public string FarmProdctImage1 {get;set;}
        public string FarmProdctImage2 {get;set;}
    }
     public class UpdateFarmReportRequestModelForm
    {
       public string FarmProduct {get;set;} 
        public int Quantity {get;set;}
        public FarmGrade Grade {get;set;}
        public int FarmId {get;set;}
        public int FarmerId {get;set;}
        public int ToBeHarvestedTime {get;set;}
        public string FarmInspectorEmail {get;set;}
    }
}