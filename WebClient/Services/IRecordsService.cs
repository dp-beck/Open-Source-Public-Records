using Shared.Dtos;

namespace WebClient.Services;

public interface IRecordsService
{
    public Task<IEnumerable<RecordDto>?> GetAllRecords();
}