using System.Collections.Generic;
using FirstProject.Entities;
using FirstProject.Enums;

namespace FirstProject.Dtos
{
    public class FarmerDto
    {
        public int Id {get;set;}
        public int UserId {get;set;}
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
        public string State {get;set;}
        public string Country {get;set;}
        public string LocalGovernment {get;set;}
        public string Gender {get;set;}
        public string UserName {get;set;}
      
        public User User {get;set;}
        public string FarmerStatus {get;set;}
        public IList<FarmDto> Farms {get;set;} = new List<FarmDto>();
    }
    public class CreateFarmerRequestModel
    {
        
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Image { get; set; }

        public string Email { get; set; }
        public string FarmPicture1 {get;set;}
        public string FarmPicture2 {get;set;}
        public string State {get;set;}
         public string Country {get;set;}
         public string LocalGovernment {get;set;}

        public string PhoneNumber { get; set; }
    
        public Gender Gender {get;set;}
        public string UserName {get;set;}
        public string PassWord {get;set;}
         public string FarmName {get;set;}
        public int LandSize {get;set;}
            public string FarmState {get;set;}
         public string FarmCountry {get;set;}
         public string FarmLocalGovernment {get;set;}
        public IList<int> FarmProduceIds {get;set;} = new List<int>();
    }
      public class UpdateFarmerRequestModel
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string State {get;set;}
        public string Country {get;set;}
        public string LocalGovernment {get;set;}
                public string PassWord {get;set;}
        public string UserName {get;set;}
        
    }
}