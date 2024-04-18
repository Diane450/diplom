using System;
using System.Collections.Generic;

namespace Diplom.Models;

public partial class Menu
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<MenuDish> MenuDishes { get; set; } = new List<MenuDish>();

    public virtual ICollection<MenuSchedule> MenuSchedules { get; set; } = new List<MenuSchedule>();
}
