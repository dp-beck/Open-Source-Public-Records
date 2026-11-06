using Data.Entities;

namespace API.Dtos;

public class CountryDto
{
    public string? Name { get; set; }
    public List<string?>? StateNames { get; set; }
    public ICollection<Resource>? Resources { get; set; }
    public DateTime? CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }

    public CountryDto() { }

    public CountryDto(Country country)
    {
        Name = country.Name;
        StateNames = country.States.Select(s => s.Name).ToList();
        Resources = country.Resources;
        CreatedOn = country.CreatedOn;
        ModifiedOn = country.ModifiedOn;
    }
}