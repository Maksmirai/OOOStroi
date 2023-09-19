using System;
using System.Collections;
using System.Collections.Generic;

namespace DemoStroi.Models;

public partial class Order
{
    public int Orderid { get; set; }

    public DateOnly Orderdate { get; set; }

    public DateOnly Orderdeliverydate { get; set; }

    public int Orderpickuppoint { get; set; }

    public int? Orderusername { get; set; }

    public int Ordercode { get; set; }

    public BitArray Orderstatus { get; set; } = null!;

    public virtual Pickuppoint OrderpickuppointNavigation { get; set; } = null!;

    public virtual ICollection<Orderproduct> Orderproducts { get; set; } = new List<Orderproduct>();

    public virtual User? OrderusernameNavigation { get; set; }
}
