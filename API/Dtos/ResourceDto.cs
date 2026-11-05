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
    public ResourceDto() { }
    public ResourceDto(Resource resource) => (Name, Type, Description, Url, Notes) = 
        (resource.Name, resource.Type, resource.Description, resource.Url, resource.Notes);
}