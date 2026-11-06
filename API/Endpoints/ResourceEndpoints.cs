using API.Dtos;
using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Endpoints;

public static class ResourceEndpoints
{
    public static void Map(WebApplication app)
    {
        var resources = app.MapGroup("/resources");
        resources.MapGet("/", GetAllResources);
        resources.MapGet("/{id}", GetResourceById);
        resources.MapPost("/", CreateResource);
        resources.MapDelete("/{id}", DeleteResource);
        resources.MapPatch("/{id}", PatchResource);
    }

    private static async Task<IResult> GetAllResources(OpenRecordsContext context)
    {
        return TypedResults.Ok(await context.Resources.Select(x => new ResourceDto(x)).ToArrayAsync());
    }

    private static async Task<IResult> GetResourceById(int id, OpenRecordsContext context)
    {
        return await context.Resources.FindAsync(id)
            is { } resource
            ? TypedResults.Ok(new ResourceDto(resource))
            : TypedResults.NoContent();
    }

    private static async Task<IResult> CreateResource(OpenRecordsContext context, ResourceDto resourceInformation)
    {
        State? state;
        if (resourceInformation.StateId is not null)
        {
            state = await context.States.FindAsync(resourceInformation.StateId);
        }
        else
        {
            state = null;
        }

        County? county;
        if (resourceInformation.CountyId is not null)
        {
            county = await context.Counties.FindAsync(resourceInformation.CountyId);
        }
        else
        {
            county = null;
        }

        Country? country;
        if (resourceInformation.CountryId is not null)
        {
            country = await context.Countries.FindAsync(resourceInformation.CountryId);
        }
        else
        {
            country = null;
        }

        Resource resource = new()
        {
            Name = resourceInformation.Name ?? throw new InvalidOperationException("Resource name is required"),
            Description = resourceInformation.Description,
            Url = resourceInformation.Url,
            Notes = resourceInformation.Notes,
            Type = resourceInformation.Type,
            State = state,
            County = county,
            Country = country
        };
        
        context.Resources.Add(resource);
        await context.SaveChangesAsync();
        
        resourceInformation = new ResourceDto(resource);
        return TypedResults.Created($"/resources/{resource.Id}", resourceInformation);
    }

    private static async Task<IResult> DeleteResource(int id, OpenRecordsContext context)
    {
        if (await context.Resources.FindAsync(id) is { } resource)
        {
            context.Resources.Remove(resource);
            await context.SaveChangesAsync();
            return TypedResults.NoContent();
        }
        return TypedResults.NotFound();
    }

    private static async Task<IResult> PatchResource(int id, OpenRecordsContext context, ResourceDto resourceUpdates)
    {
        var resource = await context.Resources.FindAsync(id);
        var country = await context.Countries.FindAsync(resourceUpdates.CountryId);
        var state = await context.States.FindAsync(resourceUpdates.StateId);
        var county = await context.Counties.FindAsync(resourceUpdates.CountyId);
    
        if (resource is null) return TypedResults.NotFound();

        if (resourceUpdates.Name is not null) resource.Name = resourceUpdates.Name;
        if (resourceUpdates.Type is not null) resource.Type = resourceUpdates.Type;
        if (resourceUpdates.Description is not null) resource.Description = resourceUpdates.Description;
        if (resourceUpdates.Url is not null) resource.Url = resourceUpdates.Url;
        if (resourceUpdates.Notes  is not null) resource.Notes = resourceUpdates.Notes;
    
        if (country is not null) resource.Country = country;
        if (state is not null) resource.State = state;
        if (county is not null) resource.County = county;
    
        resource.ModifiedOn = DateTime.UtcNow;

        await context.SaveChangesAsync();

        return TypedResults.NoContent();
    }
}