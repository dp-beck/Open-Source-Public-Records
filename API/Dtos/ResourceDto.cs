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
    public string? CountyName { get; set; }
    public int? StateId { get; set; }
    public string? StateName { get; set; }
    public int? CountryId { get; set; }
    public string? CountryName { get; set; }
    public ResourceDto() { }
    public ResourceDto(Resource resource)
    {
        (Name, Type, Description, Url, Notes) =
            (resource.Name, resource.Type, resource.Description, resource.Url, resource.Notes);
        
        if (resource.Country != null)
        {
            CountryName = resource.Country.Name;
            CountryId = resource.Country.Id;
        }
        
        if (resource.County != null)
        {
            CountyName = resource.County.Name;
            CountyId = resource.County.Id;
        }
        
        if (resource.State != null)
        {
            StateName = resource.State.Name;
            StateId = resource.State.Id;
        }
    }
}