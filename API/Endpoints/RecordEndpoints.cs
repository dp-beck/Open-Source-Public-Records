using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;

namespace API.Endpoints;

public class RecordEndpoints
{
    public static void Map(WebApplication app)
    {
        var resources = app.MapGroup("/records");
        resources.MapGet("/", GetAllRecords);
        resources.MapGet("/{id}", GetRecordById);
    }

    private static async Task<IResult> GetAllRecords(OpenRecordsContext context)
    {
        var recordDtos = await context.Records
            .Select(r => new RecordDto(r))
            .ToListAsync();
        return TypedResults.Ok(recordDtos);
    }
    
    private static async Task<IResult> GetRecordById(int id, OpenRecordsContext context)
    {
        return await context.Records.FirstOrDefaultAsync(r => r.Id == id)
            is { } record
            ? TypedResults.Ok(new RecordDto(record))
            : TypedResults.NoContent();
    }

}