using System;
using System.Collections.Generic;

namespace _6_lab_NoPattern;

public partial class GeneralPage
{
    public long Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string Title { get; set; } = null!;

    public string Website { get; set; } = null!;

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual Department? Department { get; set; }

    public virtual Hospital? Hospital { get; set; }

    public virtual ICollection<PhoneNumber> PhoneNumbers { get; set; } = new List<PhoneNumber>();
}
