using Data.Entities;

namespace API.Dtos;

public class StateDto
{
    public string? Name { get; set; }
    public int CountryId { get; set; }

    public StateDto() { }

    public StateDto(State state)
    {
        Name = state.Name;
        if (state.Country != null) CountryId = state.Country.Id;
    }
}