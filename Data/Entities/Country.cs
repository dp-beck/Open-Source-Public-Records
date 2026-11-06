using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class Country
{
    public int Id { get; set; }
    [MaxLength(500)]
    public string Name { get; set; } = string.Empty;
    public ICollection<State> States { get; } = new List<State>();
    public ICollection<Resource> Resources {get; } = new List<Resource>();
    public DateTime? CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
}