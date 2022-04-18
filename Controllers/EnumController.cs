using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstProject.Enums;
using Microsoft.AspNetCore.Mvc;

namespace FirstProject.Controllers
{
  [ApiController]
  [Route("api/[controller]")]

  public class EnumController : ControllerBase
  {
    [HttpGet("GetGenderEnum")]
    public IActionResult GetGenderEnum()
    {

      var response = Enum.GetValues(typeof(Gender)).Cast<int>().ToList();
      List<string> gend = new List<string>();
      foreach (var item in response)
      {
        gend.Add(Enum.GetName(typeof(Gender) , item));
      }
        return Ok(gend);



    }
    [HttpGet("GetEmailTypeEnum")]
    public IActionResult GetEmailTypeEnum()
    {

      var response = Enum.GetValues(typeof(EmailType)).Cast<int>().ToList();
      List<string> emailType = new List<string>();
      foreach (var item in response)
      {
        emailType.Add(Enum.GetName(typeof(EmailType) , item));
      }
        return Ok(emailType);



    }

      [HttpGet("GetFarmGrade")]

      public IActionResult GetFarmGrade()
      {

       var response = Enum.GetValues(typeof(FarmGrade)).Cast<int>().ToList();
      List<string> farmGrade = new List<string>();
      foreach (var item in response)
      {
        farmGrade.Add(Enum.GetName(typeof(FarmGrade) , item));
      }
        return Ok(farmGrade);



      }



    }
}