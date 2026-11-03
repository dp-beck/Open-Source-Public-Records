using API.Dtos;
using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Endpoints;

public class CountyEndpoints
{
    public static void Map(WebApplication app)
    {
        var counties = app.MapGroup("/counties");
        counties.MapGet("/", GetAllCounties);
        counties.MapGet("/{id}", GetCountyById);
        counties.MapPost("/{id}", CreateCounty);
        counties.MapDelete("/", DeleteCounty);
        counties.MapPatch("/{id}", PatchCounty);
    }
    
    private static async Task<IResult> GetAllCounties(OpenRecordsContext context)
    {
        return TypedResults.Ok(await context.States.ToArrayAsync());
    }
    
    private static async Task<IResult> GetCountyById(int id, OpenRecordsContext context)
    {
        return await context.Counties.FindAsync(id)
            is { } county
            ? TypedResults.Ok(county)
            : TypedResults.NotFound();
    }

    private static async Task<IResult> CreateCounty(County county, OpenRecordsContext context)
    {
        context.Counties.Add(county);
        await context.SaveChangesAsync();
        return TypedResults.Created($"/counties/{county.Id}", county);
    }

    private static async Task<IResult> DeleteCounty(int id, OpenRecordsContext context)
    {
        if (await context.Counties.FindAsync(id) is { } county)
        {
            context.Counties.Remove(county);
            await context.SaveChangesAsync();
            return TypedResults.NoContent();
        }
        return TypedResults.NotFound();
    }

    private static async Task<IResult> PatchCounty(int id, CountyDto countyUpdates, OpenRecordsContext context)
    {
        var county = await context.Counties.FindAsync(id);
        var state = await context.States.FindAsync(countyUpdates.StateId);
    
        if (county is null) return TypedResults.NotFound();

        if (countyUpdates.Name is not null) county.Name = countyUpdates.Name;
        if (state is not null) county.State = state;
    
        county.ModifiedOn = DateTime.UtcNow;

        await context.SaveChangesAsync();

        return TypedResults.NoContent();
    }
}