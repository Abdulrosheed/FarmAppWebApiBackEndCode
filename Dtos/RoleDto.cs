using System.Collections.Generic;

namespace FirstProject.Dtos
{
    public class RoleDto
    {
        public int Id {get;set;}
        public string Name {get;set;}
        public string Description {get;set;}
        public IList<UserDto> Users {get;set;} = new List<UserDto>();
    }
     public class CreateRoleRequestModel
    {
         public string Name {get;set;}
        public string Description {get;set;}
    
    }
    public class UpdateRoleRequestModel
    {
         public string Name {get;set;}
        public string Description {get;set;}
    }
}