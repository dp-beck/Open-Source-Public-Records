using Data.Entities;

namespace Shared.Dtos;

public class CountryDto
{
    public string? Name { get; set; }
    public List<string?>? StateNames { get; set; }
    
    public CountryDto() { }

    public CountryDto(Country country)
    {
        Name = country.Name;
        StateNames = country.States.Select(s => s.Name).ToList();
    }
}