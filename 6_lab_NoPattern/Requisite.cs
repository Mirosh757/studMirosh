using System;
using System.Collections.Generic;

namespace _6_lab_NoPattern;

public partial class Requisite
{
    public long Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateOnly RegistrationDate { get; set; }

    public string HospitalReduceName { get; set; } = null!;

    public string NameLegalFaces { get; set; } = null!;

    public string Ogrn { get; set; } = null!;

    public string Inn { get; set; } = null!;

    public string Kpp { get; set; } = null!;

    public long HospitalId { get; set; }

    public virtual Hospital Hospital { get; set; } = null!;
}
