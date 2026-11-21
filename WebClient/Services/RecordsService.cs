using System.Text.Json;
using System.Text.Json.Serialization;
using Shared.Dtos;

namespace WebClient.Services;

public class RecordsService : IRecordsService
{
    private readonly HttpClient _httpClient;

    private readonly JsonSerializerOptions _jsonSerializerOptions =
        new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            ReferenceHandler = ReferenceHandler.Preserve
        };

    public RecordsService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<RecordDto>?> GetAllRecords()
    {

        var recordsResponse = await _httpClient.GetAsync("records/");

        recordsResponse.EnsureSuccessStatusCode();

        var recordsJson = await recordsResponse.Content.ReadAsStringAsync();
        var recordDtos = JsonSerializer.Deserialize<IEnumerable<RecordDto>>(recordsJson, _jsonSerializerOptions);

        return recordDtos;
    }
}