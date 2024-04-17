using System;
using System.Collections.Generic;

namespace Diplom.Models;

public partial class Teacher
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public int UserId { get; set; }

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public virtual User User { get; set; } = null!;
}
