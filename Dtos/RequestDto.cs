using System;
using System.Collections.Generic;

namespace FirstProject.Dtos
{
    public class RequestDto
    {
        public int Id {get;set;}
        public string CompanyName {get;set;}
        public int Quantity {get;set;}
        public int MonthNeeded {get;set;}
        public int YearNeeded {get;set;}
       
        public string FarmProduce{get;set;}
        
        
        public string Grade {get;set;}
        public string Status {get;set;}
        public ICollection<FarmProductDto> FarmProducts { get; set; } = new List<FarmProductDto>();
    }
    public class CreateRequestModel
    {
        

        public int MonthNeeded {get;set;}
        public int YearNeeded {get;set;}
        public int Quantity {get;set;}
        public int FarmProduceId{get;set;}
        public FarmGrade Grade {get;set;}
   
    }
    public class UpdateRequestModel
    {
        public FarmGrade Grade {get;set;}
        public int MonthNeeded {get;set;}
        public int YearNeeded {get;set;}
        public int Quantity {get;set;}
    }
}