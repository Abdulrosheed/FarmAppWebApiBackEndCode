using System;
using FirstProject.Enums;

namespace FirstProject.Entities
{
    public class UserInfo
    {
        
        public int Id { get; set; }
        public bool IsDeleted { get; set; } 
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
         public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
        
        public Gender Gender {get;set;}
        public string UserName {get;set;}
        public string Image {get;set;}
        public string State {get;set;}
        public string Country {get;set;}
        public string LocalGoverment {get;set;}
      
    }
}