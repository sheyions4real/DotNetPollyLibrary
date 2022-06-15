using RequestService.Policies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add the Polly Policy
builder.Services.AddSingleton<ClientPolicy>(new ClientPolicy());

// create a httpclient factory called Test to use the ImmediateHttpRetry Policy
builder.Services.AddHttpClient("Test").AddPolicyHandler( 
    (service, request) => request.Method == HttpMethod.Get ? new ClientPolicy().ImmediateHttpRetry : new ClientPolicy().noHttpPolicy
 );



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
