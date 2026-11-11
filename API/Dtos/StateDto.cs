using Data.Entities;

namespace API.Dtos;

public class StateDto
{
    public string? Name { get; set; }
    public int CountryId { get; set; }
    public string? CountryName { get; set; }
    public List<string> CountyNames { get; set; }

    public StateDto() { }

    public StateDto(State state)
    {
        Name = state.Name;
        if (state.Country != null)
        {
            CountryName = state.Country.Name;
            CountryId = state.Country.Id;
        }
        CountyNames = state.Counties.Select(c => c.Name).ToList();

    }
}