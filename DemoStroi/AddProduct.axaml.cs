using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Castle.Components.DictionaryAdapter.Xml;
using DemoStroi.Context;
using DemoStroi.Models;
using MessageBox.Avalonia;
using MessageBox.Avalonia.Enums;


namespace DemoStroi;

public partial class AddProduct : Window
{
    public static User003Context _context;
    
    public string namePhoto = "";
    
    public string filePath = " ";
    
    public int  countAddPhoto;

    public List<Productmanufacturer> listManufacture;
    
    public List<Productsupplier> listsuplier;
    
    public int manid;
    
    public int supid;

    public AddProduct()
    {
        InitializeComponent();
        _context = User003Context.GetContext();
        var a = _context;
        CategoryBox.Items = a.Categoryproducts;
        unitProductBox.Items = a.Productunits;
        countAddPhoto = 0;

        listManufacture = a.Productmanufacturers.ToList();
        listsuplier = a.Productsuppliers.ToList();
        
        imageName.Text = " ";
        namePhoto = " ";
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
                if (bit.Size.Width <= 300 || bit.Size.Height <= 200)
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
        AdminWindow ad = new AdminWindow();
        ad.Show();
        this.Close();
    }
    private void ButtonAddProduct_OnClick(object? sender, RoutedEventArgs e)
    {

        try
        {
            if (int.Parse(maxDiscount.Text) <= 99 && int.Parse(maxDiscount.Text) >= 0 && ArticulProdct.Text.Length > 1 &&  NameProduct.Text.Length > 1 &&
                int.Parse(priceProduct.Text) > 0 &&  int.Parse(discountNow.Text) <= int.Parse(maxDiscount.Text) && int.Parse(discountNow.Text) >= 0 &&
                int.Parse(countInStock.Text) >= 0  )
            {
                try
                {
                    Product product = new Product()
                    {
                    Productarticlenumber = ArticulProdct.Text,
                    Productname = NameProduct.Text,
                    Productcategory = CategoryBox.SelectedIndex +1,
                    Productdescription = descriptionProduct.Text,
                    Productunit = unitProductBox.SelectedIndex +1
                    };

                    bool bolmanuf;
                    bool bolsupli;

                    
                    int supid;
                    
                    foreach (var i in listManufacture)
                    {
                        if (i.Productmanufacturername.ToLower() == suplierBox.Text.ToLower())
                        {
                            manid = i.Productmanufacturerid;
                            bolmanuf = true;
                        }
                    }
                    foreach (var i in listsuplier)
                    {
                        if (i.Productsuppliername.ToString().ToLower() == suplierBox.Text.ToLower())
                        {
                            supid = i.Productsupplierid;
                            bolsupli = true;
                        }
                    }

                    if (bolsupli = false)
                    {
                        Productsupplier productsupplier = new Productsupplier();
                        productsupplier.Productsuppliername = suplierBox.Text;
                        _context.Productsuppliers.Add(productsupplier);
                        _context.SaveChanges();
                        
                        foreach (var i in listManufacture)
                        {
                            if (i.Productmanufacturername.ToLower() == suplierBox.Text.ToLower())
                            {
                                manid = i.Productmanufacturerid;
                            }
                        }
                        
                    }
                    else
                    {
                        product.Productmanufacturer = manid; 
                    }
                    if (bolmanuf = false)
                    {
                        Productmanufacturer productsupplier = new Productmanufacturer();
                        productsupplier.Productmanufacturername = suplierBox.Text;
                        _context.Productmanufacturers.Add(productsupplier);
                        _context.SaveChanges();
                        foreach (var i in listsuplier)
                        {
                            if (i.Productsuppliername.ToString().ToLower() == suplierBox.Text.ToLower())
                            {
                                supid = i.Productsupplierid;
                            }
                        }
                    }
                    else
                    {
                        product.Productsupplier = this.supid;
                    }
                    
                   
                    

                    
                    
                    
                    
                    
                    product.Productmaxdiscount = int.Parse(maxDiscount.Text);
                    product.Productdiscountamount = int.Parse(discountNow.Text);
                    product.Productcost = decimal.Parse(priceProduct.Text);
                    product.Productquantityinstock = int.Parse(countInStock.Text);
                    
                    
                    if (countAddPhoto == 1)
                    {
                        product.Productphoto = namePhoto;
                        var s = Path.Combine(Directory.GetCurrentDirectory(), "Image\\").Replace("bin\\Debug\\net6.0\\", "");
                        if (s + namePhoto == filePath)
                        {
                            
                        }
                        else
                        {
                            File.Copy( filePath , s + namePhoto ,true);
                        }
                        
                    }
                    else
                    {
                        product.Productphoto = " ";
                    }
                    
                    _context.Products.Add(product);
                    _context.SaveChanges();
                    var a = MessageBoxManager.GetMessageBoxStandardWindow("Оповещение", "Товар добавлен", ButtonEnum.Ok).Show();

                    ArticulProdct.Text = "";
                    NameProduct.Text = "";
                    priceProduct.Text = "";
                    maxDiscount.Text = "";
                    discountNow.Text = "";
                    countInStock.Text = "";
                    descriptionProduct.Text = "";
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