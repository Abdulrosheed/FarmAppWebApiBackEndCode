using System.Collections.Generic;

namespace FirstProject.Entities
{
    public class Company : BaseEntity
    {
   
        public string Name {get;set;}
        public string Country {get;set;}
        public string State {get;set;}
        public string LocalGoverment {get;set;}
        public string PhoneNumber {get;set;}
        public string Email {get;set;}
       
         public string UserName {get;set;}
         public string Image {get;set;}
        public int UserId {get;set;}
        public User User {get;set;}
        public IList<Order> Orders {get;set;} = new List<Order>();
        public IList<Request> Requests {get;set;} = new List<Request>();
       
    }
}