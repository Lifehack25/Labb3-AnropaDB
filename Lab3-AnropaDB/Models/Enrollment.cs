using System;
using System.Collections.Generic;

namespace Lab3_AnropaDB.Models;

public partial class Enrollment
{
    public int EnrollmentsId { get; set; }

    public string Student { get; set; } = null!;

    public int Course { get; set; }

    public virtual Course CourseNavigation { get; set; } = null!;

    public virtual Student Enrollments { get; set; } = null!;
}
