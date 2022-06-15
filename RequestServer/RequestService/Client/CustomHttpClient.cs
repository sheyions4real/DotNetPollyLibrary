using Microsoft.AspNetCore.Mvc;
using RequestService.Models;

namespace RequestService.Client;


public class CustomHttpClient{

        private readonly HttpClient _client;
    private string _guidy;



// anytime this custom HttpClient Factory is called it will pass the request to the HttpContextMiddleware pipeline and add the neccessary
// headers before executing the request
    public CustomHttpClient(HttpClient client)
    {
        _client = client;

        // base configuration
        _guidy = Guid.NewGuid().ToString();
        _client.DefaultRequestHeaders.Add("StartupHeader", Guid.NewGuid().ToString());
    }


public Task<HomeObj?> GetHome()
{
    //return  _client.GetStringAsync($"https://localhost:7241/api/Response/GetHome/{_guidy}"); // returns string
            // using the System.Net.Http.Json to convert result to json
      return  _client.GetFromJsonAsync<HomeObj>($"https://localhost:7241/api/Response/GetHome/{_guidy}");
}


}