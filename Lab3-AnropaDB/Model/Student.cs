using System;
using System.Collections.Generic;

namespace Lab3_AnropaDB.Model;

public partial class Student
{
    public int StudentId { get; set; }

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public string PersonalNumber { get; set; } = null!;

    public string? Class { get; set; }

    public virtual Class? ClassNavigation { get; set; }

    public virtual Enrollment? Enrollment { get; set; }

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
}
