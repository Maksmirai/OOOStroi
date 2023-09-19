using System;
using System.Collections.Generic;

namespace DemoStroi.Models;

public partial class Pickuppoint
{
    public int Pickeppointid { get; set; }

    public string Pickuppointaddress { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
