using Microsoft.AspNetCore.Mvc;
using ResponseService.Models;

namespace ResponseService.Controllers;


[Route("api/[controller]/[action]")]
[ApiController]
public class ResponseController : ControllerBase
{
    [HttpGet]
    [Route("{id:int}")]
    public ActionResult GetResponse(int id)
    {

        Random rand = new Random();
        var randIntValue =rand.Next(1,101);
        if(randIntValue >= id)
        {
            Console.WriteLine("--> Failure - Generate a Http 500");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        Console.WriteLine("--> Success - Generate a Http 200");
        return Ok();
    }





     [HttpGet(Name="GetHome")]
    [Route("{guidy}")]
    public ActionResult GetHome(string guidy)
    {
            var result = new HomeObj{
                Id = 1,
                Name = guidy,
                Location = "Winnipeg - Canada",
                Dimension ="2.045, 255.2",
                YearBuilt ="2021",
                Amount = 258000.00
            };

            return Ok(result);
    }


}