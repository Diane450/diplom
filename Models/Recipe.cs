using System;
using System.Collections.Generic;

namespace Diplom.Models;

public partial class Recipe
{
    public int Id { get; set; }

    public int DishId { get; set; }

    public int ProductId { get; set; }

    public string ProductQuantity { get; set; } = null!;

    public virtual Dish Dish { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
