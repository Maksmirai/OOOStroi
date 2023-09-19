using System;
using System.IO;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using DemoStroi.Context;
using DemoStroi.Models;
using MessageBox.Avalonia;
using MessageBox.Avalonia.Enums;

namespace DemoStroi;

public partial class RedactProductWindow : Window
{
    
    public static User003Context _context;

    public string articulProduct;
    
    public string namePhoto = " ";
    
    public string filePath = " ";
    
    public  int  countAddPhoto ;
   
    public RedactProductWindow(Product product)
    {
        InitializeComponent();
        _context = User003Context.GetContext();
        var a = _context;
        CategoryBox.Items = a.Categoryproducts;
        ManufactureBox.Items = a.Productmanufacturers;
        suplierBox.Items = a.Productsuppliers;
        unitProductBox.Items = a.Productunits;
        countAddPhoto = 0;
        
        DataContext = product;
        var b = _context.Products;

        namePhoto = product.Productphoto;
        articulProduct = product.Productarticlenumber;
        
        CategoryBox.SelectedIndex = product.Productcategory  -1;
        ManufactureBox.SelectedIndex = product.Productmanufacturer - 1;
        suplierBox.SelectedIndex = product.Productsupplier - 1;
        unitProductBox.SelectedIndex = product.Productunit - 1;

        
    }

    public RedactProductWindow()
    {
        
    }
    private async void ButtonAddPhoto_OnClick(object? sender, RoutedEventArgs e)
    {
        OpenFileDialog ofd = new OpenFileDialog();
        ofd.AllowMultiple = false;
        ofd.Filters.Add(new FileDialogFilter() { Name = "Image" , Extensions = { "png" , "jpg"}} );

        var result = await ofd.ShowAsync(this);

        try
        {
            filePath = result[0];
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Image");
            imagePath = imagePath.Replace("bin\\Debug\\net6.0\\", "");
            string fileName = Path.GetFileName(filePath);

            using (var bit= new Bitmap(filePath))
            {
                if (bit.Size.Width <= 300 && bit.Size.Height <= 200)
                {
                    using (var stream = new FileStream(filePath , FileMode.Open))
                    {
                        Bitmap bitmap = new Bitmap(stream);
                        ImageProduct.Source = bitmap;
                        stream.Close();
                    }
                    namePhoto = fileName;
                    imageName.Text = fileName;
                    countAddPhoto = 1;
                }
                else
                {
                    var a = MessageBoxManager.GetMessageBoxStandardWindow("Ошибка",
                        "Ошибка в размере изображения , оно не должно быть больше 300Х200", ButtonEnum.Ok).Show();
                    return;
                    
                }
                     
            }
            
        }
        catch (Exception exception)
        {
            var a = MessageBoxManager.GetMessageBoxStandardWindow("Ошибка",
                "Ошибка в выборе изображения", ButtonEnum.Ok).Show();
        }
    }
    
    private void ButtonBackToMenu_OnClick(object? sender, RoutedEventArgs e)
    {
        AdminWindow adminWindow = new AdminWindow();
        adminWindow.Show();
        this.Close();
    }
    
    private void DelProduct_OnClick(object? sender, RoutedEventArgs e)
    {
        _context.Products.Remove(_context.Products.Where(x => x.Productarticlenumber == articulProduct).First());
        _context.SaveChanges();
        AdminWindow adminWindow = new AdminWindow();
        adminWindow.Show();
        this.Close();
    }

    private void ButtonAddProduct_OnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            if (int.Parse(maxDiscount.Text) <= 99 && int.Parse(maxDiscount.Text) >= 0 && ArticulProdct.Text.Length > 1 &&  NameProduct.Text.Length > 1 &&
                decimal.Parse(priceProduct.Text) >  0 &&  int.Parse(discountNow.Text) <= int.Parse(maxDiscount.Text) && int.Parse(discountNow.Text) >= 0 &&
                int.Parse(countInStock.Text) >= 0  )
            {

                try
                {
                    var aprod = _context.Products.Where(x => x.Productarticlenumber == articulProduct).First();
                    namePhoto = aprod.Productphoto;
                
                    Product product = new Product()
                    {
                        Productarticlenumber = ArticulProdct.Text,
                        Productname = NameProduct.Text,
                        Productcategory = CategoryBox.SelectedIndex +1,
                        Productmanufacturer = ManufactureBox.SelectedIndex +1,
                        Productsupplier = suplierBox.SelectedIndex +1,
                        Productdescription = descriptionProduct.Text,
                        Productunit = unitProductBox.SelectedIndex +1
                    };
                    product.Productmaxdiscount = int.Parse(maxDiscount.Text);
                    product.Productdiscountamount = int.Parse(discountNow.Text);
                    product.Productcost = decimal.Parse(priceProduct.Text);
                    product.Productquantityinstock = int.Parse(countInStock.Text);

                    if (countAddPhoto == 1)
                    {
                        product.Productphoto = namePhoto;
                        var s = Path.Combine(Directory.GetCurrentDirectory(), "Image\\").Replace("bin\\Debug\\net6.0\\", "");
                        File.Copy( filePath , s + namePhoto , true );
                    }
                    
                    _context.SaveChanges();
                    var b = MessageBoxManager.GetMessageBoxStandardWindow("Оповещение", "Данные о товаре обновлены", ButtonEnum.Ok).Show(); 
                }
                catch (Exception exception)
                {
                    var a = MessageBoxManager.GetMessageBoxStandardWindow("Ошибка", "Неверные данные при заполнение полей", ButtonEnum.Ok).Show();
                }
            }
            else
            {
                var a = MessageBoxManager.GetMessageBoxStandardWindow("Ошибка", "Неверные данные при заполнение полей", ButtonEnum.Ok).Show();
            }
        }
        catch (Exception exception)
        {
            var a = MessageBoxManager.GetMessageBoxStandardWindow("Ошибка", "Неверные данные при заполнение полей", ButtonEnum.Ok).Show();
        }
    }

    
}