using ECommerce.Aggregator.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<ICatalogService, CatalogService>(options =>
{
    options.BaseAddress = new Uri(builder.Configuration["apiSettings:CatalogUrl"]);
});

builder.Services.AddHttpClient<IBasketService, BasketService>(options =>
{
    options.BaseAddress = new Uri(builder.Configuration["apiSettings:BasketUrl"]);
});

builder.Services.AddHttpClient<IOrderService, OrderService>(options =>
{
    options.BaseAddress = new Uri(builder.Configuration["apiSettings:OrderingUrl"]);
});

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
