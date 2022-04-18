using System.Collections.Generic;

namespace FirstProject.Entities
{
    public class User : BaseEntity
    {
        public string Email {get;set;}
        public string PhoneNumber {get;set;}
        public string PassWord {get;set;}
        public string FirstName {get;set;}
        public string LastName {get;set;}
        public Admin Admin {get;set;}
        public Farmer Farmer {get;set;}
        public Company Company {get;set;}
        public FarmInspector FarmInspector {get;set;}
        public IList<UserRole> UserRole {get;set;} = new List<UserRole>();
    }
}