using System;

namespace DemoStroi;

public class myProduct
{
    public string Productarticlenumber { get; set; } = null!;

    public string Productname { get; set; } = null!;

    public int Productunit { get; set; }

    public decimal Productcost { get; set; }

    public int Productmaxdiscount { get; set; }

    public string Productmanufacturer { get; set; }

    public int Productsupplier { get; set; }

    public int Productcategory { get; set; }

    public int? Productdiscountamount { get; set; }

    public int Productquantityinstock { get; set; }

    public string Productdescription { get; set; } = null!;

    public string? Productphoto { get; set; }

    public decimal CheckPrice
    {
        get
        {
            decimal g = Convert.ToDecimal(100 - Productdiscountamount) / 100 ;
            decimal s = decimal.Multiply(Productcost , g);
            return Math.Round(s,2); 
        }
        set
        {
            
        }
    }
    
}