using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

string connection;
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

// CRUD Operations - States
app.MapGet("/states", async(OpenRecordsContext context) => 
    await context.States.ToListAsync());

// Get by ID
app.MapGet("/states/{id}", async (int id, OpenRecordsContext context) => 
    await context.States.FindAsync(id)
        is { } state
        ? Results.Ok(state)
        : Results.NotFound());

// CRUD Operations - Counties
app.MapGet("/counties", async(OpenRecordsContext context) => 
    await context.Counties.ToListAsync());

// Get by ID
app.MapGet("/counties/{id}", async (int id, OpenRecordsContext context) => 
    await context.Counties.FindAsync(id)
        is { } county
        ? Results.Ok(county)
        : Results.NotFound());

// CRUD Operations - Resources
app.MapGet("/resources", async(OpenRecordsContext context) => 
    await context.Resources.ToListAsync());

// Get by ID
app.MapGet("/resources/{id}", async (int id, OpenRecordsContext context) => 
    await context.Resources.FindAsync(id)
        is { } resource
        ? Results.Ok(resource)
        : Results.NotFound());

app.Run();
