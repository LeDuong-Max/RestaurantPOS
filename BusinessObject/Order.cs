using System;
using System.Collections.Generic;

namespace BusinessObject;

public partial class Order
{
    public int OrderId { get; set; }

    public int TableId { get; set; }

    public int AccountId { get; set; }

    public DateTime OrderDate { get; set; }

    public decimal? TotalPrice { get; set; }

    public DateTime? CheckoutDate { get; set; }
    
    public decimal Discount { get; set; }

    public int Status { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual DiningTable Table { get; set; } = null!;
}
