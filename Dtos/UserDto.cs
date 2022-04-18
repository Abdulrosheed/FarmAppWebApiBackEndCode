using System.Collections.Generic;
using FirstProject.Entities;

namespace FirstProject.Dtos
{
    public class UserDto
    {
        public int Id {get;set;}
        
        public string Email {get;set;}
        public string Name {get;set;}
        public string PhoneNumber {get;set;}
        public Admin Admin {get;set;}
        public Farmer Farmer {get;set;}
        public FarmInspector FarmInspector {get;set;}
        public IList<RoleDto> Roles {get;set;} = new List<RoleDto>();
    }
    public class LoginRequestModel
    {
        public string Email {get;set;}
        public string PassWord {get;set;}
    }
  
}