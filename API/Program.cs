using API.Dtos;
using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

string? connection;
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddEnvironmentVariables().AddJsonFile("appsettings.Development.json");
    connection = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
}
else
{
    connection = Environment.GetEnvironmentVariable("AZURE_SQL_CONNECTIONSTRING");
}

builder.Services.AddDbContext<OpenRecordsContext>(options =>
    options.UseSqlServer(connection));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

// Map Group
var countries = app.MapGroup("/countries");
var states = app.MapGroup("/states");
var counties = app.MapGroup("/counties");
var resources = app.MapGroup("/resources");

// CRUD Operations - Countries

// Get ALL
countries.MapGet("/", async(OpenRecordsContext context) => 
    await context.Countries.ToListAsync());

// Get by ID
countries.MapGet("/{id}", async (int id, OpenRecordsContext context) => 
    await context.Countries.FindAsync(id)
        is { } country
            ? Results.Ok(country)
            : Results.NotFound());

// Create Country
countries.MapPost("/", async (Country country, OpenRecordsContext context) =>
{
    context.Countries.Add(country);
    await context.SaveChangesAsync();

    return Results.Created($"/countries/{country.Id}", country);
});

// Delete Country
countries.MapDelete("/{id}", async (int id, OpenRecordsContext context) =>
{
    if (await context.Countries.FindAsync(id) is { } country)
    {
        context.Countries.Remove(country);
        await context.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
});

// PATCH Action
// Rename Country
countries.MapPatch("/{id}", async (int id, CountryDto updatedCountry, OpenRecordsContext context) =>
{
    var country = await context.Countries.FindAsync(id);

    if (country is null) return Results.NotFound();

    if (updatedCountry.Name is not null) country.Name = updatedCountry.Name;
    country.ModifiedOn = DateTime.UtcNow;

    await context.SaveChangesAsync();

    return Results.NoContent();
});

// CRUD Operations - States
// Get ALL
states.MapGet("/", async(OpenRecordsContext context) => 
    await context.States.ToListAsync());

// Get by ID
states.MapGet("/{id}", async (int id, OpenRecordsContext context) => 
    await context.States.FindAsync(id)
        is { } state
        ? Results.Ok(state)
        : Results.NotFound());

// Create State
states.MapPost("/", async (StateDto stateInput, OpenRecordsContext context) =>
{
    var country = await context.Countries.FindAsync(stateInput.CountryId);
    
    var state = new State
    {
        Name = stateInput.Name,
        Country = country
    };

    context.States.Add(state);
    await context.SaveChangesAsync();

    return Results.Created($"/{state.Id}", state);
});

// Delete State
states.MapDelete("/{id}", async (int id, OpenRecordsContext context) =>
{
    if (await context.States.FindAsync(id) is { } state)
    {
        context.States.Remove(state);
        await context.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
});

// Patch State
states.MapPatch("/{id}", async (int id, StateDto updatedState, OpenRecordsContext context) =>
{
    var state = await context.States.FindAsync(id);
    var country = await context.Countries.FindAsync(updatedState.CountryId);
    
    if (state is null) return Results.NotFound();

    if (updatedState.Name is not null) state.Name = updatedState.Name;
    if (country is not null) state.Country = country;
    
    state.ModifiedOn = DateTime.UtcNow;

    await context.SaveChangesAsync();

    return Results.NoContent();
});

// CRUD Operations - Counties
// Get All
counties.MapGet("/", async(OpenRecordsContext context) => 
    await context.Counties.ToListAsync());

// Get by ID
counties.MapGet("/{id}", async (int id, OpenRecordsContext context) => 
    await context.Counties.FindAsync(id)
        is { } county
        ? Results.Ok(county)
        : Results.NotFound());

// Create County
counties.MapPost("/", async (County county, OpenRecordsContext context) =>
{
    context.Counties.Add(county);
    await context.SaveChangesAsync();

    return Results.Created($"/countries/{county.Id}", county);
});

// Delete County
counties.MapDelete("/{id}", async (int id, OpenRecordsContext context) =>
{
    if (await context.Counties.FindAsync(id) is { } county)
    {
        context.Counties.Remove(county);
        await context.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
});

// Patch State
counties.MapPatch("/{id}", async (int id, CountyDto updatedCounty, OpenRecordsContext context) =>
{
    var county = await context.Counties.FindAsync(id);
    var state = await context.States.FindAsync(updatedCounty.StateId);
    
    if (county is null) return Results.NotFound();

    if (updatedCounty.Name is not null) county.Name = updatedCounty.Name;
    if (state is not null) county.State = state;
    
    county.ModifiedOn = DateTime.UtcNow;

    await context.SaveChangesAsync();

    return Results.NoContent();
});
// CRUD Operations - Resources
// Get All
resources.MapGet("/", async(OpenRecordsContext context) => 
    await context.Resources.ToListAsync());

// Get by ID
resources.MapGet("/{id}", async (int id, OpenRecordsContext context) => 
    await context.Resources.FindAsync(id)
        is { } resource
        ? Results.Ok(resource)
        : Results.NotFound());

// Create Resource
resources.MapPost("/", async (Resource resource, OpenRecordsContext context) =>
{
    context.Resources.Add(resource);
    await context.SaveChangesAsync();

    return Results.Created($"/{resource.Id}", resource);
});

// Delete Resource
resources.MapDelete("/{id}", async (int id, OpenRecordsContext context) =>
{
    if (await context.Resources.FindAsync(id) is { } resource)
    {
        context.Resources.Remove(resource);
        await context.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
});

// TO DO: PUT Action

app.Run();
