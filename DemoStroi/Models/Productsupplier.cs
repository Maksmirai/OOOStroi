using System;
using System.Collections.Generic;

namespace DemoStroi.Models;

public partial class Productsupplier
{
    public int Productsupplierid { get; set; }

    public string Productsuppliername { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
