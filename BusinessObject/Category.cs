using System;
using System.Collections.Generic;

namespace BusinessObject;

public partial class Category
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<FoodItem> FoodItems { get; set; } = new List<FoodItem>();
}
