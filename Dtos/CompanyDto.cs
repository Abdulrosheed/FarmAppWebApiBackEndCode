using System.Collections.Generic;

namespace FirstProject.Dtos
{
    public class CompanyDto
    {
        public string Name {get;set;}
        public string Country {get;set;}
        public string State {get;set;}
        public string LocalGovernment {get;set;}
        public string PhoneNumber {get;set;}
        public string Email {get;set;}
       
         public string UserName {get;set;}
        public int UserId {get;set;}
        public int Id {get;set;}
        public IList<OrderDto> Orders {get;set;} = new List<OrderDto>();
    }
    public class CreateCompanyRequestModel
    {
        public string Name {get;set;}
        public string Image {get;set;}
        public string Country {get;set;}
        public string State {get;set;}
        public string LocalGovernment {get;set;}
        public string PhoneNumber {get;set;}
        public string Email {get;set;}
        public string PassWord {get;set;}
         public string UserName {get;set;}
    }
    public class UpdateCompanyRequestModel
    {
        public string Name {get;set;}
        public string Country {get;set;}
        public string State {get;set;}
        public string LocalGovernment {get;set;}
        public string PhoneNumber {get;set;}
        public string PassWord {get;set;}
         public string UserName {get;set;}
    }
}