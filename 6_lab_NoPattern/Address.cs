using System;
using System.Collections.Generic;

namespace _6_lab_NoPattern;

public partial class Address
{
    public long Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string Address1 { get; set; } = null!;

    public long GeneralPageId { get; set; }

    public virtual GeneralPage GeneralPage { get; set; } = null!;
}
