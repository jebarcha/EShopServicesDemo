using EShopServices.Api.Author.Application;
using EShopServices.Api.Author.Persistence;
using EShopServices.Api.Author.RabbitHandler;
using EShopServices.RabbitMQ.Bus.BusRabbit;
using EShopServices.RabbitMQ.Bus.EventQueue;
using EShopServices.RabbitMQ.Bus.Implement;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddTransient<IRabbitEventBus, RabbitEventBus>();
builder.Services.AddTransient<IEventHandler<EmailEventQueue>, EmailEventHandler>();

// Add services to the container.
builder.Services.AddControllers().AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<NewClass>());
//builder.Services.AddFluentValidationAutoValidation();
//builder.Services.AddFluentValidationClientsideAdapters();
//builder.Services.AddValidatorsFromAssembly(typeof(NewClass).Assembly);


builder.Services.AddDbContext<ContextAuthor>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("ConnectionDatabase"));
});

builder.Services.AddMediatR(typeof(NewClass.Handler).Assembly);
builder.Services.AddAutoMapper(typeof(GetClass.Handler));

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


var eventBus = app.Services.GetRequiredService<IRabbitEventBus>();
eventBus.Subscribe<EmailEventQueue, EmailEventHandler>();


app.Run();
