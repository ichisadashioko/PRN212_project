using System;
using System.Collections.Generic;

public partial class Registration
{
    public int RegistrationId { get; set; }

    public int? UserId { get; set; }

    public string RegistrationType { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string Status { get; set; } = null!;

    public int? ApprovedBy { get; set; }

    public string? Comments { get; set; }

    public virtual User? ApprovedByNavigation { get; set; }

    public virtual User? User { get; set; }
}
