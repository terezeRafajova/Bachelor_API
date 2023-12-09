using Microsoft.EntityFrameworkCore;
using Bachelor_API.Data;
using Bachelor_API.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Bachelor_API;

public static class LessonEndpoints
{
    public static void MapLessonEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Lesson").WithTags(nameof(Lesson));

        group.MapGet("/", GetAllLessons)
        .WithName("GetAllLessons")
        .WithOpenApi();

        group.MapGet("/code/{code}", GetLessonByCode)
        .WithName("GetLessonByCode")
        .WithOpenApi();

        group.MapPost("/", ShareLesson)
        .WithName("CreateLesson")
        .WithOpenApi()
        .RequireAuthorization();


        group.MapGet("/id/{id}", async Task<Results<Ok<Lesson>, NotFound>> (int id, Bachelor_APIContext db) =>
        {
            return await db.Lesson.AsNoTracking()
                .FirstOrDefaultAsync(model => model.LessonId == id)
                is Lesson model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetLessonById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int lessonid, Lesson lesson, Bachelor_APIContext db) =>
        {
            var affected = await db.Lesson
                .Where(model => model.LessonId == lessonid)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.LessonId, lesson.LessonId)
                    .SetProperty(m => m.Title, lesson.Title)
                    .SetProperty(m => m.Username, lesson.Username)
                    .SetProperty(m => m.SharingCode, lesson.SharingCode)
                    .SetProperty(m => m.SharingTime, lesson.SharingTime)
                    .SetProperty(m => m.NumberOfPages, lesson.NumberOfPages)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateLesson")
        .WithOpenApi();
        
        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int lessonid, Bachelor_APIContext db) =>
        {
            var affected = await db.Lesson
                .Where(model => model.LessonId == lessonid)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteLesson")
        .WithOpenApi();
    }

    public static async Task<Lesson[]> GetAllLessons(Bachelor_APIContext db)
    {
        return await db.Lesson.ToArrayAsync();

    }

    public static async Task<Results<Ok<Lesson>, NotFound>> GetLessonByCode (int code, Bachelor_APIContext db)
        {
            var lesson = await db.Lesson
            .Include(l => l.CodeBlocks)
            .Include(l => l.Descriptions)
            .Include(l => l.Titles)
            .AsNoTracking()
            .FirstOrDefaultAsync(model => model.SharingCode == code);

            return lesson != null
                ? TypedResults.Ok(lesson)
                : TypedResults.NotFound();
        }

    public static async Task<Created<Lesson>> ShareLesson(Lesson lesson, Bachelor_APIContext db)
        {
            var codeGenerator = new CodeGenerator(db);

            int uniqueCode = codeGenerator.GenerateUniqueCode();
            lesson.SharingCode = uniqueCode;

            db.Lesson.Add(lesson);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Lesson/{lesson.LessonId}",lesson);
        }
}
