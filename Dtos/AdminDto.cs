using System;
using FirstProject.Entities;
using FirstProject.Enums;

namespace FirstProject.Dtos
{
    public class AdminDto
    {
        public int Id {get;set;}
        public int UserId {get;set;}
        public string FirstName { get; set; }
  
        public User User {get;set;}
        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
        public string State {get;set;}
        public string Country {get;set;}
        public string LocalGovernment {get;set;}
        public Gender Gender {get;set;}
        public string UserName {get;set;}
    }
    public class CreateAdminRequestModel
    {
         public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
       public string Image {get;set;}
        public Gender Gender {get;set;}
        public string UserName {get;set;}
         public string PassWord {get;set;}
         public string State {get;set;}
         public string Country {get;set;}
         public string LocalGovernment {get;set;}
    }
    public class UpdateAdminRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string State {get;set;}
        public string Country {get;set;}
        public string LocalGovernment {get;set;}
        public string UserName {get;set;}
        
         public string PassWord {get;set;}
       
    }
    public class AssignFarmInspectorToFarmRequestModel
    {
        public int FarmId {get;set;}
        public string FarmInspectorEmail {get;set;}
        public DateTime InspectingDate {get;set;}
    }
  
}