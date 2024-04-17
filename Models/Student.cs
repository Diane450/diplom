using System;
using System.Collections.Generic;

namespace Diplom.Models;

public partial class Student
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public int ClassId { get; set; }

    public int StatusId { get; set; }

    public virtual Class Status { get; set; } = null!;

    public virtual Status StatusNavigation { get; set; } = null!;
}
