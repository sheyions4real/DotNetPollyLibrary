using Microsoft.AspNetCore.Mvc;
using RequestService.Policies;
using System.Net.Http;

namespace RequestService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RequestServiceController : ControllerBase
{

    private readonly ClientPolicy _clientPolicy; // you may not need this if you configure polly in the startup class
    private readonly IHttpClientFactory _httpClientFactory;

    public RequestServiceController(ClientPolicy clientPolicy, IHttpClientFactory httpClientFactory)
    {
        _clientPolicy = clientPolicy;
        _httpClientFactory = httpClientFactory;
    }

     //Get api/request
    [HttpGet]
    public async Task<ActionResult> MakeRequest()
    {
       // Console.WriteLine("I was called");
    // using httpclient
      //  var client = new HttpClient();

    // using the httpclientfactory
      //  var client = _httpClientFactory.CreateClient();
        var client = _httpClientFactory.CreateClient("Test");  // this will create the client with The Test configuration in startup services

        // to call the client
        var response = await client.GetAsync("https://localhost:7241/api/Response/50");


//  wrapping the client.GetAsync call in the Client Policy will enforce the Polly Policy rules defined
      //  var response = await _clientPolicy.ImmediateHttpRetry.ExecuteAsync(
       //         () => client.GetAsync("https://localhost:7241/api/Response/50")
      //      );


//  wrapping the client.GetAsync call in the Client Policy will enforce the Polly Policy rules defined (retry 5 times but at 3 sec interval)
       // var response = await _clientPolicy.linearHttpRetry.ExecuteAsync(
        //        () => client.GetAsync("https://localhost:7241/api/Response/50")
        //    );
/// using the client factory you can configure polly in the program class hence you dont have to call it anytime you use the client factory

    //var response = await _clientPolicy.exponentialHttpRetry.ExecuteAsync(
     //           () => client.GetAsync("https://localhost:7241/api/Response/50")
     //       );

        if(response.IsSuccessStatusCode)
        {
            Console.WriteLine("--> Response Service returned SUCCESS");
               return Ok();
        }
        
          Console.WriteLine("--> Response Service returned FAILED");
          return StatusCode(StatusCodes.Status500InternalServerError);
    } 

}