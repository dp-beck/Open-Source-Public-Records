using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;

namespace API.Endpoints;

public static class CountyEndpoints
{
    public static void Map(WebApplication app)
    {
        var counties = app.MapGroup("/counties");
        counties.MapGet("/", GetAllCounties);
        counties.MapGet("/{id}", GetCountyById);
        counties.MapPost("/", CreateCounty);
        counties.MapDelete("/{id}", DeleteCounty);
        counties.MapPatch("/{id}", PatchCounty);
    }
    
    private static async Task<IResult> GetAllCounties(OpenRecordsContext context)
    {
        var countyDtos = await context.Counties
            .Include(x => x.State)
            .Select(x => new CountyDto(x))
            .ToListAsync();
        
        return TypedResults.Ok(countyDtos);
    }
    
    private static async Task<IResult> GetCountyById(int id, OpenRecordsContext context)
    {
        return await context.Counties.Include(x => x.State).FirstOrDefaultAsync(c => c.Id == id)
            is { } county
            ? TypedResults.Ok(new CountyDto(county))
            : TypedResults.NotFound();
    }

    private static async Task<IResult> CreateCounty(CountyDto countyInformation, OpenRecordsContext context)
    {
        County newCounty = new County()
        {
            Name = countyInformation.Name ?? throw new InvalidOperationException("County name not provided."),
            FederalCourtDistrictName = countyInformation.FederalCourtDistrictName,
            StateId = countyInformation.StateId,
        };
        
        context.Counties.Add(newCounty);
        await context.SaveChangesAsync();
        
        countyInformation = new CountyDto(newCounty);
        return TypedResults.Created($"/counties/{newCounty.Id}", countyInformation);
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
        if (countyUpdates.FederalCourtDistrictName is not null) county.FederalCourtDistrictName = countyUpdates.FederalCourtDistrictName;
        if (state is not null) county.State = state;
    
        county.ModifiedOn = DateTime.UtcNow;

        await context.SaveChangesAsync();

        return TypedResults.NoContent();
    }
}