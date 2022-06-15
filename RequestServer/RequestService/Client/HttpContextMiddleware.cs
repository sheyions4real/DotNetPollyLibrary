namespace RequestService.Client;

// creating a middle ware

public class HttpContextMiddleware : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContext;
    private string _middlewareCtor;

    public HttpContextMiddleware(IHttpContextAccessor accessor)
    {
        _middlewareCtor = Guid.NewGuid().ToString();
        _httpContext = accessor;        // grab the HttpCOntext from the accessor
      
    }


// with this middle ware you can attaching the bearer token, authentication, catching 401 errors, and so many 
// this middle ware when applied to the services addhttpclient pipeline of the application all request made to the httpclient factory will pass 
// through this middle ware to add the bearer token, catch errors, add the headers and other setting of choice before executing the actual
// purpose of the httpclient
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
             var httpContext = _httpContext.HttpContext != null ? _httpContext.HttpContext :  _httpContext.HttpContext;
// u can use the HttpContextAccessor to check if the User identity is valid or if the bearer token is valid 
        if(httpContext != null){
            if(httpContext.User != null){
                var method = $"Method ${Guid.NewGuid().ToString()}";
                request.Headers.Add("Middleware-Ctor", _middlewareCtor);
                request.Headers.Add("Middleware-Method", method);
            }
        }
       

        return base.SendAsync(request, cancellationToken);
    }
}