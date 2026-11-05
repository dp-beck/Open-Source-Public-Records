using Data.Entities;

namespace API.Dtos;

public class CountryDto
{
    public string? Name { get; set; }

    public CountryDto() { }
    public CountryDto (Country country) => Name = country.Name;
}