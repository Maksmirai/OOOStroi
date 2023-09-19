using System;
using System.Collections.Generic;

namespace DemoStroi.Models;

public partial class Product
{
    public string Productarticlenumber { get; set; } = null!;

    public string Productname { get; set; } = null!;

    public int Productunit { get; set; }

    public decimal Productcost { get; set; }

    public int Productmaxdiscount { get; set; }

    public int Productmanufacturer { get; set; }

    public int Productsupplier { get; set; }

    public int Productcategory { get; set; }

    public int? Productdiscountamount { get; set; }

    public int Productquantityinstock { get; set; }

    public string Productdescription { get; set; } = null!;

    public string? Productphoto { get; set; }

    public virtual ICollection<Orderproduct> Orderproducts { get; set; } = new List<Orderproduct>();

    public virtual Categoryproduct ProductcategoryNavigation { get; set; } = null!;

    public virtual Productmanufacturer ProductmanufacturerNavigation { get; set; } = null!;

    public virtual Productsupplier ProductsupplierNavigation { get; set; } = null!;

    public virtual Productunit ProductunitNavigation { get; set; } = null!;
    
}
    