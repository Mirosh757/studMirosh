using System;
using System.Collections.Generic;

namespace _6_lab_NoPattern;

public partial class Hospital
{
    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public long Id { get; set; }

    public long RegionId { get; set; }

    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();

    public virtual GeneralPage IdNavigation { get; set; } = null!;

    public virtual Region Region { get; set; } = null!;

    public virtual ICollection<Requisite> Requisites { get; set; } = new List<Requisite>();
}
