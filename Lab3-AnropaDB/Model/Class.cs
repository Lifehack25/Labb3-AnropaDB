using System;
using System.Collections.Generic;

namespace Lab3_AnropaDB.Model;

public partial class Class
{
    public string ClassName { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
