using System;
using System.Collections.Generic;

namespace FirstProject.Entities
{
    public class Farm : BaseEntity
    {
        public string Name {get;set;}
        public int LandSize {get;set;}
        public int FarmerId {get;set;}
        public Farmer Farmer {get;set;}
        public FarmStatus FarmStatus {get;set;}
        public int? FarmInspectorId {get;set;}
       
        public  FarmInspector FarmInspector {get;set;}
         public DateTime? InspectionDate {get;set;}
        public string State {get;set;}
        public string Country {get;set;}
        public string LocalGoverment {get;set;}
       
        public string FarmPicture1 {get;set;}
        public string FarmPicture2 {get;set;}
        public IList<FarmProduct> FarmProducts {get;set;}
        public IList<FarmReport> FarmReports {get;set;}
        
        public IList<FarmProduceFarm> FarmProduceFarm {get;set;} = new List<FarmProduceFarm>();
  
    }
}