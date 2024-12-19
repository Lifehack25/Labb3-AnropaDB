using System;
using System.Collections.Generic;

namespace Lab3_AnropaDB.Model;

public partial class Grade
{
    public int GradeId { get; set; }

    public string Grade1 { get; set; } = null!;

    public DateOnly GradeDate { get; set; }

    public int Student { get; set; }

    public int Course { get; set; }

    public int Teacher { get; set; }

    public virtual Course CourseNavigation { get; set; } = null!;

    public virtual Student StudentNavigation { get; set; } = null!;

    public virtual Staff TeacherNavigation { get; set; } = null!;
}
