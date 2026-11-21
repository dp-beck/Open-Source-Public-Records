using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class Record
{
    public int Id { get; set; }
    
    [MaxLength(500)]
    [Required]
    public string Name { get; set; } = string.Empty;
    
    [MaxLength(500)]
    public string Country {get; set;} = string.Empty;
    
    [MaxLength(500)]
    public string State {get; set;} = string.Empty;
    
    [MaxLength(500)]
    public string County {get; set;} = string.Empty;
    
    [MaxLength(500)]
    public string RecordType { get; set; } = string.Empty;
    
    [MaxLength(1000)]
    public string Description { get; set; } = string.Empty;
    
    [MaxLength(500)]
    public string? Url { get; set; } = string.Empty;
    
    [MaxLength(1000)]
    public string Notes { get; set; } = string.Empty;
    
    public DateTime? CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
}