using Microsoft.EntityFrameworkCore;
using Bachelor_API.Data;
using Bachelor_API.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
namespace Bachelor_API;

public static class TeacherEndpoints
{
    public static void MapTeacherEndpoints(this IEndpointRouteBuilder routes, WebApplicationBuilder builder)
    {

        var group = routes.MapGroup("/api/Teacher").WithTags(nameof(Teacher));


        group.MapPost("/Login", async (Teacher teacher, Bachelor_APIContext db) =>
        {
            var teacherDb = db.Teacher.Where(teacherdb => teacherdb.Username == teacher.Username && teacherdb.Password == teacher.Password).FirstOrDefault();
            if (teacherDb == null)
                return Results.Empty;
            else
            {
                //return token
                var serviceProvider = builder.Services.BuildServiceProvider();
                var tokenService = serviceProvider.GetRequiredService<TokenService>(); 
                var token = tokenService.CreateAcessToken(teacherDb);
                return Results.Ok(token);
            }
        })
        .WithName("Login")
        .WithOpenApi();

    }

}
