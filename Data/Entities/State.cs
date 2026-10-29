using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class State
{
    public int Id { get; set; }
    [MaxLength(500)]
    public string? Name { get; set; } = string.Empty;
    public List<County> Counties = [];
    public List<Resource> Resources = [];
    public Country? Country { get; set; }
    public DateTime? CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
}