using System;
using System.Collections.Generic;

namespace Diplom.Models;

public partial class MenuDish
{
    public int Id { get; set; }

    public int MenuId { get; set; }

    public int DishId { get; set; }

    public virtual Dish Dish { get; set; } = null!;

    public virtual Menu Menu { get; set; } = null!;
}
