using System;
using System.Collections.Generic;

namespace BusinessObject;

public partial class DiningTable
{
    public int TableId { get; set; }

    public string TableName { get; set; } = null!;

    public int Status { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
