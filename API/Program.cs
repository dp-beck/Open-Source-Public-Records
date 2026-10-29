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

// CRUD Operations - Countries

// Get ALL
app.MapGet("/countries", async(OpenRecordsContext context) => 
    await context.Countries.ToListAsync());

// Get by ID
app.MapGet("/countries/{id}", async (int id, OpenRecordsContext context) => 
    await context.Countries.FindAsync(id)
        is { } country
            ? Results.Ok(country)
            : Results.NotFound());

// Create Country
app.MapPost("/countries", async (Country country, OpenRecordsContext context) =>
{
    context.Countries.Add(country);
    await context.SaveChangesAsync();

    return Results.Created($"/countries/{country.Id}", country);
});

// Delete Country
app.MapDelete("/countries/{id}", async (int id, OpenRecordsContext context) =>
{
    if (await context.Countries.FindAsync(id) is { } country)
    {
        context.Countries.Remove(country);
        await context.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
});

// TO DO: PUT Action

// CRUD Operations - States
// Get ALL
app.MapGet("/states", async(OpenRecordsContext context) => 
    await context.States.ToListAsync());

// Get by ID
app.MapGet("/states/{id}", async (int id, OpenRecordsContext context) => 
    await context.States.FindAsync(id)
        is { } state
        ? Results.Ok(state)
        : Results.NotFound());

// Create State
app.MapPost("/states", async (State state, OpenRecordsContext context) =>
{
    context.States.Add(state);
    await context.SaveChangesAsync();

    return Results.Created($"/states/{state.Id}", state);
});

// Delete State
app.MapDelete("/states/{id}", async (int id, OpenRecordsContext context) =>
{
    if (await context.States.FindAsync(id) is { } state)
    {
        context.States.Remove(state);
        await context.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
});

// TO DO: PUT Action

// CRUD Operations - Counties
// Get All
app.MapGet("/counties", async(OpenRecordsContext context) => 
    await context.Counties.ToListAsync());

// Get by ID
app.MapGet("/counties/{id}", async (int id, OpenRecordsContext context) => 
    await context.Counties.FindAsync(id)
        is { } county
        ? Results.Ok(county)
        : Results.NotFound());

// Create County
app.MapPost("/counties", async (County county, OpenRecordsContext context) =>
{
    context.Counties.Add(county);
    await context.SaveChangesAsync();

    return Results.Created($"/countries/{county.Id}", county);
});

// Delete County
app.MapDelete("/counties/{id}", async (int id, OpenRecordsContext context) =>
{
    if (await context.Counties.FindAsync(id) is { } county)
    {
        context.Counties.Remove(county);
        await context.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
});

// TO DO: PUT Action

// CRUD Operations - Resources
// Get All
app.MapGet("/resources", async(OpenRecordsContext context) => 
    await context.Resources.ToListAsync());

// Get by ID
app.MapGet("/resources/{id}", async (int id, OpenRecordsContext context) => 
    await context.Resources.FindAsync(id)
        is { } resource
        ? Results.Ok(resource)
        : Results.NotFound());

// Create Resource
app.MapPost("/resources", async (Resource resource, OpenRecordsContext context) =>
{
    context.Resources.Add(resource);
    await context.SaveChangesAsync();

    return Results.Created($"/countries/{resource.Id}", resource);
});

// Delete Resource
app.MapDelete("/resources/{id}", async (int id, OpenRecordsContext context) =>
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
