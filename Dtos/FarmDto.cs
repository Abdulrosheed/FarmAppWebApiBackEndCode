using System.Collections.Generic;
using FirstProject.Entities;
using Microsoft.AspNetCore.Http;

namespace FirstProject.Dtos
{
    public class FarmDto
    {
        public int Id {get;set;}
        public string Name {get;set;}
        public string Country {get;set;}
        public string State {get;set;}
        public string LocalGovernment {get;set;}
        public int LandSize {get;set;}
        public string FarmerName {get;set;}
        public string FarmerEmail {get;set;}
        public int FarmerId {get;set;}
        public string FarmStatus {get;set;}
        
        public IList<FarmProduceDto> FarmProduces {get;set;} = new List<FarmProduceDto>();
        public IList<FarmProductDto> FarmProduct {get;set;} = new List<FarmProductDto>();
    }
    public class CreateFarmRequestModel
    {
        public string Name {get;set;}
        public int LandSize {get;set;}
        public string FarmPicture1 {get;set;}
        public string FarmPicture2 {get;set;}
        public string State {get;set;}
         public string Country {get;set;}
         public string LocalGovernment {get;set;}
        public IList<int> FarmProduceIds {get;set;} = new List<int>();
    }
    
}