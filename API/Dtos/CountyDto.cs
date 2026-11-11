using Data.Entities;

namespace API.Dtos;

public class CountyDto
{
    public string? Name { get; set; }
    public string? FederalCourtDistrictName { get; set; }
    public int StateId { get; set; }
    public string? StateName { get; set; }

    public CountyDto() { }

    public CountyDto(County county)
    {
        Name = county.Name;
        FederalCourtDistrictName = county.FederalCourtDistrictName;
        if (county.State != null)
        {
            StateName = county.State.Name;
            StateId = county.State.Id;
        }
        
    } 
}