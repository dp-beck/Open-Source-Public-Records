using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class State
{
    public int Id { get; set; }
    [MaxLength(500)]
    public string? Name { get; set; } = string.Empty;
    public ICollection<County> Counties { get; } = new List<County>();
    public ICollection<Resource> Resources { get; } = new List<Resource>();
    public int CountryId { get; set; }
    public Country Country { get; set; } = null!;
    public DateTime? CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
}