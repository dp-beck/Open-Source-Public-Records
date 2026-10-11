namespace Data.Entities;

public class Country
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<State> States = [];
    public List<Resource> Resources = [];
}