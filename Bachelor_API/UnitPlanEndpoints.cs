using Microsoft.EntityFrameworkCore;
using Bachelor_API.Data;
using Bachelor_API.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.VisualStudio.Web.CodeGeneration;
using System.Drawing.Text;

namespace Bachelor_API;

public static class UnitPlanEndpoints
{
    public static void MapUnitPlanEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/UnitPlan").WithTags(nameof(UnitPlan));

        group.MapGet("/", async (Bachelor_APIContext db) =>
        {
            return await db.UnitPlan.ToListAsync();
        })
        .WithName("GetAllUnitPlans")
        .WithOpenApi();

        group.MapGet("/id/{id}", async Task<Results<Ok<UnitPlan>, NotFound>> (int id, Bachelor_APIContext db) =>
        {
            return await db.UnitPlan.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is UnitPlan model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetUnitPlanById")
        .WithOpenApi();

        group.MapGet("/code/{code}", async Task<Results<Ok<UnitPlan>, NotFound>> (int code, Bachelor_APIContext db) =>
        {
            return await db.UnitPlan.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Code == code)
                is UnitPlan model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetUnitPlanByCode")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, UnitPlan unitPlan, Bachelor_APIContext db) =>
        {
            var affected = await db.UnitPlan
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.Id, unitPlan.Id)
                    .SetProperty(m => m.Code, unitPlan.Code)
                    .SetProperty(m => m.Title, unitPlan.Title)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateUnitPlan")
        .WithOpenApi();

        group.MapPost("/", async (UnitPlan unitPlan, Bachelor_APIContext db) =>
        {
            var codeGenerator = new CodeGenerator(db);

            int uniqueCode = codeGenerator.GenerateUniqueCode();
            unitPlan.Code = uniqueCode;

            db.UnitPlan.Add(unitPlan);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/UnitPlan/{unitPlan.Id}",unitPlan);
        })
        .WithName("CreateUnitPlan")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, Bachelor_APIContext db) =>
        {
            var affected = await db.UnitPlan
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteUnitPlan")
        .WithOpenApi();

    }
}
