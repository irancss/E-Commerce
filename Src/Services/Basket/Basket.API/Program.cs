using Basket.API.GrpcServices;
using Basket.API.Repositories;
using Discount.Grpc.Protos;
using MassTransit;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Basket.API", Version = "v1" });
});

//Start Redis Connection
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetValue<string>("CacheSettings:ConnectionString");
});
//End Redis Connection

//Start General Configuration
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]);
});

builder.Services.AddScoped<DiscountGrpcService>();
//En d General Configuration

//Start Masstransit Configuration
builder.Services.AddMassTransit(options =>
{
    options.UsingRabbitMq((ctx, config) =>
    {
        config.Host(builder.Configuration["EventBusSettings:HostAddress"]);
    });
});

//no need in masstransit version 8
//builder.Services.AddMassTransitHostedService();

//End Masstransit Configuration





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Basket.API v1");
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();
