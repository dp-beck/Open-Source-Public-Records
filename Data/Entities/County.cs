using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class County
{
    public int Id { get; set; }
    [MaxLength(500)]
    public string Name { get; set; } = string.Empty;
    [MaxLength(500)]
    public string FederalCourtDistrictName { get; set; } = string.Empty;
    public List<Resource> Resources = [];
    public State? State { get; set; }
    public DateTime? CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
}