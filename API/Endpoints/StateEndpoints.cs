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
        var stateDtos = await context.States
            .Include(c => c.Country)
            .Include(c => c.Counties)
            .Select(c => new StateDto(c))
            .ToListAsync();
        
        return TypedResults.Ok(stateDtos);
    }
    
    private static async Task<IResult> GetStateById(int id, OpenRecordsContext context)
    {
        return await context.States
                .Include(s => s.Country)
                .Include(s => s.Counties)
                .FirstOrDefaultAsync(s => s.Id == id)
            is { } state
            ? TypedResults.Ok(new StateDto(state))
            : TypedResults.NotFound();
    }

    static async Task<IResult> CreateState(StateDto stateInformation, OpenRecordsContext context)
    {
        
        State newState = new()
        {
            Name = stateInformation.Name,
            CountryId = stateInformation.CountryId
        };
        
        context.States.Add(newState);
        await context.SaveChangesAsync();

        stateInformation = new StateDto(newState);

        return TypedResults.Created($"/states/{newState.Id}", stateInformation);
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