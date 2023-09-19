using System;
using System.Collections.Generic;

namespace DemoStroi.Models;

public partial class Categoryproduct
{
    public int Categoryproductid { get; set; }

    public string Categoryproductname { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
