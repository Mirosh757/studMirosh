using System;
using System.Collections.Generic;

namespace _6_lab_NoPattern;

public partial class Speciality
{
    public long Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<DoctorSpeciality> DoctorSpecialities { get; set; } = new List<DoctorSpeciality>();
}
