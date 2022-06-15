using RequestService.Client;
using RequestService.Policies;

var builder = WebApplication.CreateBuilder(args);

// add the dependency injection life cycle for HttpContextMiddleware Class
builder.Services.AddTransient<HttpContextMiddleware>();

// Http context accessor used to access context like the identity user
builder.Services.AddHttpContextAccessor();

// Add services to the container.
// Add the Polly Policy
builder.Services.AddSingleton<ClientPolicy>(new ClientPolicy());

// create a httpclient factory clientcalled Test to use the ImmediateHttpRetry Policy
builder.Services.AddHttpClient("Test").AddPolicyHandler( 
    (service, request) => request.Method == HttpMethod.Get ? new ClientPolicy().ImmediateHttpRetry : new ClientPolicy().noHttpPolicy
  ).AddHttpMessageHandler<HttpContextMiddleware>(); // registering the middleware for the httpcusomeclient;



// create a httpclient factory client with the base address called SimpleClient to use the ImmediateHttpRetry Policy
builder.Services.AddHttpClient("SimpleClient", 
                client => { client.BaseAddress = new Uri("https://localhost:7241/api/"); 
                            client.DefaultRequestHeaders.Add("StartupHeader", Guid.NewGuid().ToString()); // can add a bearer token
                          } 
  ).AddPolicyHandler( 
    (service, request) => request.Method == HttpMethod.Get ? new ClientPolicy().ImmediateHttpRetry : new ClientPolicy().noHttpPolicy
  ).AddHttpMessageHandler<HttpContextMiddleware>(); // registering the middleware for the httpcusomeclient;


// // add an HttpClient Factory customeHttp Client usinf the ApiCLientExtension


// builder.Services.AddHttpClient<CustomHttpClient>("customClient", client => { 
//      client.BaseAddress = new Uri("https://localhost:7241/api/");
// }).AddHttpMessageHandler<HttpContextMiddleware>(); // registering the middleware for the httpcusomeclient



// addinf our own middle extensions
builder.Services.AddCustomApiClient();

// adding HttpCLientfactory
builder.Services.AddHttpClient();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
