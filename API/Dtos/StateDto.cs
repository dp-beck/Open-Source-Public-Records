using Data.Entities;

namespace API.Dtos;

public class StateDto
{
    public string? Name { get; set; }
    public Country? Country { get; set; }
    public DateTime? ModifiedOn { get; set; }
}