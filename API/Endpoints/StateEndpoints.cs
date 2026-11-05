using API.Dtos;
using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Endpoints;

public static class StateEndpoints
{
    public static void Map(WebApplication app)
    {
        var states = app.MapGroup("/states");
        states.MapGet("/", GetAllStates);
        states.MapGet("/{id}", GetStateById);
        states.MapPost("/", CreateState);
        states.MapDelete("/{id}", DeleteState);
        states.MapPatch("/{id}", PatchState);
    }

    private static async Task<IResult> GetAllStates(OpenRecordsContext context)
    {
        return TypedResults.Ok(await context.States.Include(s => s.Country).ToArrayAsync());
    }
    
    private static async Task<IResult> GetStateById(int id, OpenRecordsContext context)
    {
        return await context.States.FindAsync(id)
            is { } state
            ? TypedResults.Ok(state)
            : TypedResults.NotFound();
    }

    static async Task<IResult> CreateState(StateDto newStateDto, OpenRecordsContext context)
    {
        Country? country = context.Countries.Find(newStateDto.CountryId);
        
        State newState = new()
        {
            Name = newStateDto.Name,
            Country = country
        };
        
        context.States.Add(newState);
        await context.SaveChangesAsync();

        return TypedResults.Created($"/states/{newState.Id}", newState);
    }
    
    static async Task<IResult> DeleteState(int id, OpenRecordsContext context)
    {
        if (await context.States.FindAsync(id) is { } state)
        {
            context.States.Remove(state);
            await context.SaveChangesAsync();
            return TypedResults.NoContent();
        }
    
        return TypedResults.NotFound();
    }
    
    static async Task<IResult> PatchState(int id, StateDto stateUpdates, OpenRecordsContext context)
    {
        var state = await context.States.FindAsync(id);
        var country = await context.Countries.FindAsync(stateUpdates.CountryId);
    
        if (state is null) return TypedResults.NotFound();

        if (stateUpdates.Name is not null) state.Name = stateUpdates.Name;
        if (country is not null) state.Country = country;
    
        state.ModifiedOn = DateTime.UtcNow;

        await context.SaveChangesAsync();

        return TypedResults.NoContent();
    }
}