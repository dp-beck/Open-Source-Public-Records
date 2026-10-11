namespace Data.Entities;

public class County
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<Resource> Resources = [];
}