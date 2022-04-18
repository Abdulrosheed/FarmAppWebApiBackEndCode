using System.Collections.Generic;
using FirstProject.Enums;

namespace FirstProject.Entities
{
    public class Farmer : UserInfo
    {
        public int UserId {get;set;}
        public User User {get;set;}
        public FarmerStatus FarmerStatus {get;set;}
        public IList<FarmReport> FarmReports {get;set;}
        public IList<FarmProduct> FarmProducts {get;set;}
        public IList<Farm> Farms {get;set;} = new List<Farm>();
    }
}