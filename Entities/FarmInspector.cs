using System.Collections.Generic;

namespace FirstProject.Entities
{
    public class FarmInspector : UserInfo
    {
        public int UserId {get;set;}
        public User User {get;set;}
        public IList<FarmReport> FarmReports {get;set;}
        public IList<Farm> Farm {get;set;}
    }
}