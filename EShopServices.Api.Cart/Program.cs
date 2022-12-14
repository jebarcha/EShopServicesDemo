using EShopServices.Api.Cart.Applicationñ;
using EShopServices.Api.Cart.Persistence;
using EShopServices.Api.Cart.RemoteInterface;
using EShopServices.Api.Cart.RemoteService;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

string connectionString = builder.Configuration.GetConnectionString("ConnectionDB")!.ToString();
builder.Services.AddDbContext<CartContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddLogging(logging => logging.AddConsole());

builder.Services.AddScoped<IBookService, BookService>();

builder.Services.AddMediatR(typeof(New.Handler).Assembly);

builder.Services.AddHttpClient("Books", config =>
{
    config.BaseAddress = new Uri(builder.Configuration["Services:Books"]);
});

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

app.Run();
