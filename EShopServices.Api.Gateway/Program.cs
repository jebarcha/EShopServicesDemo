using EShopServices.Api.Gateway.ImplementRemote;
using EShopServices.Api.Gateway.InterfaceRemote;
using EShopServices.Api.Gateway.MessageHandler;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

IConfiguration configuration = new ConfigurationBuilder()
                            .AddJsonFile("ocelot.json")
                            .Build();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers();
builder.Services.AddHttpClient("AuthorService", config =>
{
    config.BaseAddress = new Uri(builder.Configuration["Services:Author"]);
});

builder.Services.AddSingleton<IAuthorRemote, AuthorRemote>();


builder.Services.AddOcelot(configuration).AddDelegatingHandler<BookHandler>();


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

app.UseAuthorization();

app.MapControllers();

/*await*/ app.UseOcelot();


app.Run();
