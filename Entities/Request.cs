using System;
using System.Collections.Generic;

namespace FirstProject.Entities
{
    public class Request : BaseEntity
    {
        public int CompanyId {get;set;}
        public Company Company {get;set;}
        public int Quantity {get;set;}
        public int MonthNeeded {get;set;}
        public int YearNeeded {get;set;}
       
        public int FarmProduceId{get;set;}
        public FarmProduce FarmProduce{get;set;}
        
        public FarmGrade Grade {get;set;}
        public RequestStatus Status {get;set;}
        public ICollection<FarmProductRequest> FarmProductRequests { get; set; } = new List<FarmProductRequest>();
        
    }
}