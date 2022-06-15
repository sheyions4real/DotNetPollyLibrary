using RequestService.Client;


namespace RequestService.Client;
public static class ApiClientExtension
{
    // all services configuration and middleware registration for the APIClient
    public static IServiceCollection AddCustomApiClient(this IServiceCollection services)
    {
            //add the dependency injection life cycle for HttpContextMiddleware Class
            services.AddTransient<HttpContextMiddleware>();       // this can cause cyclic dependency issue since it already exist in the startup file
            

            // add an HttpClient Factory customeHttp Client
            services.AddHttpClient<CustomHttpClient>("customClient", client => { 
                client.BaseAddress = new Uri("https://localhost:7241/api/");
            }).AddHttpMessageHandler<HttpContextMiddleware>(); // registering the middleware for the httpcusomeclient

            return services;
    }
}