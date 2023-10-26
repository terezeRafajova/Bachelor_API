using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Bachelor_API.Data;
using Bachelor_API;
using Bachelor_API.Model;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Bachelor_APIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AzureDBConnection") ?? throw new InvalidOperationException("Connection string 'Bachelor_APIContext' not found.")));


// Add services to the container.
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


app.MapUnitPlanEndpoints();

app.Run();

