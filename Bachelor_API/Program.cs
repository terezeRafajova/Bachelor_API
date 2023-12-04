using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Bachelor_API.Data;
using Bachelor_API;
using Bachelor_API.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Reactive.Subjects;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Bachelor_APIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AzureDBConnection") ?? throw new InvalidOperationException("Connection string 'Bachelor_APIContext' not found.")));


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<TokenService>();
builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization();

// Enable CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAny",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

app.UseCors("AllowAny");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();

app.MapLessonEndpoints();

app.MapTeacherEndpoints(builder);

app.Run();

