using System;
using System.Collections.Generic;

namespace Diplom.Models;

public partial class Class
{
    public int Id { get; set; }

    public string Class1 { get; set; } = null!;

    public int TeacherId { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual Teacher Teacher { get; set; } = null!;
}
