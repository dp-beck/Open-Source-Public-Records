using Data;
using Data.Entities;

namespace API.Dtos;

public class ResourceDto
{
    public string? Name { get; set; }
    public ResourceType? Type { get; set; }
    public string? Description { get; set; }
    public string? Url { get; set; }
    public string? Notes { get; set; }
    public int? CountyId { get; set; }
    public int? StateId { get; set; }
    public int? CountryId { get; set; }
}