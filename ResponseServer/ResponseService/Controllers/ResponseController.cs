using Microsoft.AspNetCore.Mvc;

namespace ResponseService.Controllers;


[Route("api/[controller]")]
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
}