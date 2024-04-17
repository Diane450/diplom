using System;
using System.Collections.Generic;

namespace Diplom.Models;

public partial class DishesType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Dish> Dishes { get; set; } = new List<Dish>();
}
