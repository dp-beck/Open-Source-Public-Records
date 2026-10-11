namespace Data.Entities;

public class State
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<County> Counties = [];
    public List<Resource> Resources = [];
}