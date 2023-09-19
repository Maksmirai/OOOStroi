using System;
using System.Collections.Generic;

namespace DemoStroi.Models;

public partial class Productmanufacturer
{
    public int Productmanufacturerid { get; set; }

    public string Productmanufacturername { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
