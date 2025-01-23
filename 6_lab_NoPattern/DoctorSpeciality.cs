using System;
using System.Collections.Generic;

namespace _6_lab_NoPattern;

public partial class DoctorSpeciality
{
    public long Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateOnly DateStart { get; set; }

    public DateOnly? DateEnd { get; set; }

    public long SpecialityId { get; set; }

    public long DoctorId { get; set; }

    public long DepartmentId { get; set; }

    public virtual Department Department { get; set; } = null!;

    public virtual Doctor Doctor { get; set; } = null!;

    public virtual Speciality Speciality { get; set; } = null!;
}
