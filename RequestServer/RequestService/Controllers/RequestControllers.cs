using Microsoft.AspNetCore.Mvc;
using RequestService.Client;
using RequestService.Models;
using RequestService.Policies;
using System.Net.Http;

using System.Net.Http.Json;

namespace RequestService.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class RequestServiceController : ControllerBase
{

    private readonly ClientPolicy _clientPolicy; // you may not need this if you configure polly in the startup class
    private readonly IHttpClientFactory _httpClientFactory;

  private readonly CustomHttpClient _client;

    public RequestServiceController(ClientPolicy clientPolicy, IHttpClientFactory httpClientFactory, CustomHttpClient client)
    {
        _clientPolicy = clientPolicy;
        _httpClientFactory = httpClientFactory;
        _client = client;
    }

     //Get api/request
    [HttpGet(Name ="MakeRequest")]
    public async Task<ActionResult> MakeRequest()
    {
        Console.WriteLine("I was called");
    // using httpclient
      //  var client = new HttpClient();

    // using the httpclientfactory
      //  var client = _httpClientFactory.CreateClient();
        var client = _httpClientFactory.CreateClient("Test");  // this will create the client with The Test configuration in startup services

        // to call the client
        var response = await client.GetAsync("https://localhost:7241/api/Response/GetResponse/50");

       // client.BaseAddress = new Uri("https://localhost:7241/api/Response/GetResponse/50");


//  wrapping the client.GetAsync call in the Client Policy will enforce the Polly Policy rules defined
      //  var response = await _clientPolicy.ImmediateHttpRetry.ExecuteAsync(
       //         () => client.GetAsync("https://localhost:7241/api/Response/GetResponse/50)
      //      );


//  wrapping the client.GetAsync call in the Client Policy will enforce the Polly Policy rules defined (retry 5 times but at 3 sec interval)
       // var response = await _clientPolicy.linearHttpRetry.ExecuteAsync(
        //        () => client.GetAsync("https://localhost:7241/api/Response/GetResponse/50")
        //    );
/// using the client factory you can configure polly in the program class hence you dont have to call it anytime you use the client factory

    //var response = await _clientPolicy.exponentialHttpRetry.ExecuteAsync(
     //           () => client.GetAsync("https://localhost:7241/api/Response/GetResponse/50")
     //       );

        if(response.IsSuccessStatusCode)
        {
            Console.WriteLine("--> Response Service returned SUCCESS");
               return Ok();
        }
        
          Console.WriteLine("--> Response Service returned FAILED");
          return StatusCode(StatusCodes.Status500InternalServerError);
    } 




    [HttpGet(Name="Typed")]
    public Task<HomeObj?> Typed()
    {
        Console.WriteLine("Typed Controller");
        // using the custom client factory will hide the details of the HttpClient from the controller 
       return  _client.GetHome();
    }

}