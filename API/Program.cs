using System;
using API;
using API.Dtos;
using API.Endpoints;
using Data;
using Data.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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

// Map Endpoints
CountryEndpoints.Map(app);
StateEndpoints.Map(app);
CountyEndpoints.Map(app);

// Map Group -- moving to separate endpoints files and using typed results
var resources = app.MapGroup("/resources");

// CRUD Operations 

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

// Patch State
resources.MapPatch("/{id}", async (int id, ResourceDto updatedResource, OpenRecordsContext context) =>
{
    var resource = await context.Resources.FindAsync(id);
    var country = await context.Countries.FindAsync(updatedResource.CountryId);
    var state = await context.States.FindAsync(updatedResource.StateId);
    var county = await context.Counties.FindAsync(updatedResource.CountyId);
    
    if (resource is null) return Results.NotFound();

    if (updatedResource.Name is not null) resource.Name = updatedResource.Name;
    if (updatedResource.Type is not null) resource.Type = updatedResource.Type;
    if (updatedResource.Description is not null) resource.Description = updatedResource.Description;
    if (updatedResource.Url is not null) resource.Url = updatedResource.Url;
    if (updatedResource.Notes  is not null) resource.Notes = updatedResource.Notes;
    
    if (country is not null) resource.Country = country;
    if (state is not null) resource.State = state;
    if (county is not null) resource.County = county;
    
    resource.ModifiedOn = DateTime.UtcNow;

    await context.SaveChangesAsync();

    return Results.NoContent();
});

app.Run();
