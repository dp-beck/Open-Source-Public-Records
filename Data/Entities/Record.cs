using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class Record
{
    public int Id { get; init; }
    
    [MaxLength(500)]
    [Required]
    public string Name { get; init; } = string.Empty;
    
    [MaxLength(500)]
    public string? Country { get; set; }
        
    [MaxLength(500)]
    public string? State {get; set;} 
    
    [MaxLength(500)]
    public string? County {get; set;} 
    
    [MaxLength(500)]
    public string? RecordType { get; set; }
    
    [MaxLength(1000)]
    public string? Description { get; set; } 
    
    [MaxLength(500)]
    public string? Url { get; set; } 
    
    [MaxLength(1000)]
    public string? Notes { get; set; } 
    
    public DateTime? CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
}