using System;
using System.Collections.Generic;

namespace DemoStroi.Models;

public partial class Productunit
{
    public int Unitproductid { get; set; }

    public string Unitproductname { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
