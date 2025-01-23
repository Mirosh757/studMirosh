using System;
using System.Collections.Generic;

namespace _6_lab_NoPattern;

public partial class Department
{
    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public long Id { get; set; }

    public long HospitalId { get; set; }

    public virtual ICollection<DoctorSpeciality> DoctorSpecialities { get; set; } = new List<DoctorSpeciality>();

    public virtual Hospital Hospital { get; set; } = null!;

    public virtual GeneralPage IdNavigation { get; set; } = null!;
}
