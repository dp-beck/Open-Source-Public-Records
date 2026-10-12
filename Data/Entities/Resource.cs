namespace Data.Entities;

public class Resource
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ResourceType? Type { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public State? State { get; set; }
    public Country? Country { get; set; }
}