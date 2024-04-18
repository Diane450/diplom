using System;
using System.Collections.Generic;

namespace Diplom.Models;

public partial class MenuSchedule
{
    public int Id { get; set; }

    public int MenuId { get; set; }

    public DateOnly Date { get; set; }

    public virtual Menu Menu { get; set; } = null!;
}
