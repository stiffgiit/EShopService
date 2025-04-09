using EShop.Domain.Repositories;
using EShop.Application.Services;
using EShop.Domain.Seeders;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICreditCardService, CreditCardValidator>();

builder.Services.AddDbContext<DataContext>(options =>
    options.UseInMemoryDatabase("EShopDB"));


builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IRepository, Repository>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<DataContext>();
    EShopSeeder.Seed(context);
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
