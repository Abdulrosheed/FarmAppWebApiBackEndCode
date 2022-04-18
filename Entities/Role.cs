using System.Collections.Generic;

namespace FirstProject.Entities
{
    public class Role : BaseEntity
    {
        public string Name {get;set;}
        public string Description {get;set;}
        public IList<UserRole> UserRole {get;set;} = new List<UserRole>();
    }
}