using Data;
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
app.MapGet("/countries", async(OpenRecordsContext context) => 
    await context.Countries.ToListAsync());

// CRUD Operations - States
app.MapGet("/states", async(OpenRecordsContext context) => 
    await context.States.ToListAsync());

// CRUD Operations - Counties
app.MapGet("/counties", async(OpenRecordsContext context) => 
    await context.Counties.ToListAsync());

// CRUD Operations - Resources
app.MapGet("/resources", async(OpenRecordsContext context) => 
    await context.Resources.ToListAsync());
app.Run();
