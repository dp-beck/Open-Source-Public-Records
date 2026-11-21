using Data.Entities;

namespace Shared.Dtos;

public class RecordDto
{
    public string? Name { get; set; }
    public string? RecordType { get; set; }
    public string? Description { get; set; }
    public string? Url { get; set; }
    public string? Notes { get; set; }
    public string? County { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
    public RecordDto() { }
    public RecordDto(Record record)
    {
        (Name, RecordType, Description, Url, Notes, County, State, Country) =
            (record.Name,
                record.RecordType,
                record.Description,
                record.Url,
                record.Notes,
                record.County,
                record.State,
                record.County);
    }
}