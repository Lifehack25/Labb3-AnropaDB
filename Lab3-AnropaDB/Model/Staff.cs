using System;
using System.Collections.Generic;

namespace Lab3_AnropaDB.Model;

public partial class Staff
{
    public int StaffId { get; set; }

    public string FullName { get; set; } = null!;

    public string PersonalNumber { get; set; } = null!;

    public string Position { get; set; } = null!;

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
}
