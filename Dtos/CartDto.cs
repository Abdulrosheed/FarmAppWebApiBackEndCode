using System.Collections.Generic;

namespace FirstProject.Dtos
{
    public class CartDto
    {
        public string FarmProductImage {get;set;}
        public int Id {get;set;}
        public int CompanyId {get;set;}
        public ICollection<FarmProductDto> FarmProductsCarts { get; set; } = new List<FarmProductDto>();

         

    }
    public class CreateCartRequestModel
    {
        public int CompanyId {get;set;}
        public int ItemId {get;set;}
        public int Quantity {get;set;}
    }
    public class UpdateCartRequestModel
    {
        public int CompanyId {get;set;}
        public int ItemId {get;set;}
        
    }
    public class UpdateItemCartRequestModel
    {
        public int CompanyId {get;set;}
        public int ItemId {get;set;}
        public int Quantity {get;set;}
    }
}