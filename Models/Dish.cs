using System;
using System.Collections.Generic;

namespace Diplom.Models;

public partial class Dish
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int DishesTypeId { get; set; }

    public virtual DishesType DishesType { get; set; } = null!;

    public virtual ICollection<MenuDish> MenuDishes { get; set; } = new List<MenuDish>();

    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
}
