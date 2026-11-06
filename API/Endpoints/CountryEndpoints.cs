using API.Dtos;
using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Endpoints;

public static class CountryEndpoints
{
    public static void Map(WebApplication app)
    {
        var countries = app.MapGroup("/countries");
        countries.MapGet("/", GetAllCountries);
        countries.MapGet("/{id}", GetCountryById);
        countries.MapPost("/", CreateCountry);
        countries.MapDelete("/{id}", DeleteCountry);
        countries.MapPatch("/{id}", PatchCountry);
    }

    static async Task<IResult> GetAllCountries(OpenRecordsContext context)
    {
        return TypedResults.Ok(await context.Countries.Select(x => new CountryDto(x)).ToArrayAsync());
    }
    
    static async Task<IResult> GetCountryById(OpenRecordsContext context, int id)
    {
        return await context.Countries.FindAsync(id)
            is { } country
            ? TypedResults.Ok(new CountryDto(country))
            : TypedResults.NotFound();
    }
    
    static async Task<IResult> CreateCountry(CountryDto countryInformation, OpenRecordsContext context)
    {
        Country newCountry = new()
        {
            Name = countryInformation.Name ?? throw new InvalidOperationException("No country name was provided"),
        };
        
        context.Countries.Add(newCountry);
        await context.SaveChangesAsync();
        
        countryInformation = new CountryDto(newCountry);

        return TypedResults.Created($"/countries/{newCountry.Id}", countryInformation);
    }
    
    static async Task<IResult> DeleteCountry(int id, OpenRecordsContext context)
    {
        if (await context.Countries.FindAsync(id) is { } country)
        {
            context.Countries.Remove(country);
            await context.SaveChangesAsync();
            return TypedResults.NoContent();
        }
    
        return TypedResults.NotFound();
    }

    static async Task<IResult> PatchCountry(int id, CountryDto countryUpdates, OpenRecordsContext context)
    {
        var country = await context.Countries.FindAsync(id);

        if (country is null) return TypedResults.NotFound();

        if (countryUpdates.Name is not null) country.Name = countryUpdates.Name;
        country.ModifiedOn = DateTime.UtcNow;

        await context.SaveChangesAsync();

        return TypedResults.NoContent();
    }
    
}