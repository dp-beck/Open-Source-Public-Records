using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class County
{
    public int Id { get; set; }
    [MaxLength(500)]
    [Required]
    public string Name { get; set; } = string.Empty;
    [MaxLength(500)]
    public string? FederalCourtDistrictName { get; set; }
    
    public ICollection<Resource> Resources { get; } = new List<Resource>();
    
    public int StateId { get; set; }
    public State? State { get; set; }
    
    public DateTime? CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
}