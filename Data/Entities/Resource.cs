using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class Resource
{
    public int Id { get; set; }
    [MaxLength(500)]
    public string Name { get; set; } = string.Empty;
    public ResourceType? Type { get; set; }
    [MaxLength(1000)]
    public string Description { get; set; } = string.Empty;
    [MaxLength(500)]
    public string Url { get; set; } = string.Empty;
    [MaxLength(1000)]
    public string Notes { get; set; } = string.Empty;
    public County? County { get; set; }
    public State? State { get; set; }
    public Country? Country { get; set; }
    public DateTime? CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
}