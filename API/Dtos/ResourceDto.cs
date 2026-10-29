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
    public County? County { get; set; }
    public State? State { get; set; }
    public Country? Country { get; set; }
}