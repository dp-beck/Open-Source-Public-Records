using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class Country
{
    public int Id { get; set; }
    [MaxLength(500)]
    public string Name { get; set; } = string.Empty;
    public List<State> States = [];
    public List<Resource> Resources = [];
    public DateTime? CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
}