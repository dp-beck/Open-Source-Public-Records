using Data.Entities;

namespace API.Dtos;

public class CountyDto
{
    public string? Name { get; set; }
    public string? FederalCourtDistrictName { get; set; }
    public int StateId { get; set; }

    public CountyDto() { }
    public CountyDto(County county) => (Name, FederalCourtDistrictName) = (county.Name,  county.FederalCourtDistrictName);
    
}