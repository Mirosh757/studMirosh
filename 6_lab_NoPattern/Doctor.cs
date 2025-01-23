using System;
using System.Collections.Generic;

namespace _6_lab_NoPattern;

public partial class Doctor
{
    public long Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string PassportDetails { get; set; } = null!;

    public DateOnly DateBirth { get; set; }

    public virtual ICollection<DoctorSpeciality> DoctorSpecialities { get; set; } = new List<DoctorSpeciality>();
}
