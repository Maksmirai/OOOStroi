using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Avalonia.Shared.PlatformSupport;
using DemoStroi.Context;
using DemoStroi.Models;
using HarfBuzzSharp;
using Bitmap = System.Drawing.Bitmap;

namespace DemoStroi;

public partial class UserWindow : Window
{
    public static User003Context _context;

    public List<Product> productList;

    public UserWindow()
    {
        InitializeComponent();
        _context = User003Context.GetContext();
        nameUser.Text = Manager.nameUser;
        Manager.count = 0;
        ManufactureList();
        ListBoxData.Items = _context.Products.ToList();
        
        
        ComboBoxPrice.SelectedIndex = 0;
    }

    public void ManufactureList()
    {
        List<Productmanufacturer> listMan = new List<Productmanufacturer>();
        Productmanufacturer productmanufacturer = new Productmanufacturer();
        productmanufacturer.Productmanufacturerid = 0;
        productmanufacturer.Productmanufacturername = "Все производители";
        listMan.Add(productmanufacturer);
        var a = _context.Productmanufacturers.ToList();
        foreach (var n in a)
        {
            listMan.Add(n);
        }
        ComboBoxManufacture.Items = listMan;
        ComboBoxManufacture.SelectedIndex = 0;
    }

    private void BackMenu_OnClick(object? sender, RoutedEventArgs e)
    {
        Autorization mainWindow = new Autorization();
        mainWindow.Show();
        this.Close();
        Manager.idRole = 0;
        Manager.nameUser = "";
    }
    
    private void ComboBoxPrice_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        productList = _context.Products.ToList();
        ProvSearch();
        ProvPrice();
        ProvManuf();
        countProduct.Text = productList.Count.ToString() + "/" + _context.Products.Count() ;
        ListBoxData.Items = productList.Select(x => new myProduct()
        {
            Productname = x.Productname,
            Productphoto = x.Productphoto,
            Productdescription = x.Productdescription,
            Productquantityinstock = x.Productquantityinstock,
            Productcost = x.Productcost,
            Productdiscountamount = x.Productdiscountamount,
            CheckPrice = x.Productcost,
            Productmanufacturer = x.ProductmanufacturerNavigation.Productmanufacturername
        });
    }

    private void ComboBoxManufacture_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        productList = _context.Products.ToList();
        ProvSearch();
        ProvPrice();
        ProvManuf();
        countProduct.Text = productList.Count.ToString() + "/" + _context.Products.Count() ;
        ListBoxData.Items = productList.Select(x => new myProduct()
        {
            Productname = x.Productname,
            Productphoto = x.Productphoto,
            Productdescription = x.Productdescription,
            Productquantityinstock = x.Productquantityinstock,
            Productcost = x.Productcost,
            Productdiscountamount = x.Productdiscountamount,
            CheckPrice = x.Productcost,
            Productmanufacturer = x.ProductmanufacturerNavigation.Productmanufacturername
        });
    }

    public void ProvPrice()
    {
        if (ComboBoxPrice.SelectedIndex == 1)
           productList =  productList.OrderBy(x => x.Productcost).ToList();
        else if (ComboBoxPrice.SelectedIndex == 2)
            productList = productList.OrderBy(x => x.Productcost).Reverse().ToList();
    }
    public void ProvManuf()
    {
        if (ComboBoxManufacture.SelectedIndex == 0)
            _context.Products.ToList();
        else
            productList =  productList.Where(x => x.Productmanufacturer == ComboBoxManufacture.SelectedIndex).ToList();
    }
    public void ProvSearch()
    {
        if(searchBox.Text == null || searchBox.Text == "" || searchBox.Text == " ")
        {}
        else
        {
            string s = "";
            s = searchBox.Text;
            productList =  productList.Where(x => x.ProductmanufacturerNavigation.Productmanufacturername.ToLower().Contains(s.ToLower()) ||
                                                  x.Productname.ToLower().Contains(s.ToLower()) || x.Productdescription.ToLower().Contains(s.ToLower()) || x.Productcost.ToString().Contains(s.ToString())
                                                  ).ToList();
        }
            
    }
    private void InputElement_OnKeyUp(object? sender, KeyEventArgs e)
    {
        productList = _context.Products.ToList();
        ProvSearch();
        ProvPrice();
        ProvManuf();
        countProduct.Text = productList.Count.ToString() + "/" + _context.Products.Count() ;
        ListBoxData.Items = productList.Select(x => new myProduct()
        {
            Productname = x.Productname,
            Productphoto = x.Productphoto,
            Productdescription = x.Productdescription,
            Productquantityinstock = x.Productquantityinstock,
            Productcost = x.Productcost,
            Productdiscountamount = x.Productdiscountamount,
            CheckPrice = x.Productcost,
            Productmanufacturer = x.ProductmanufacturerNavigation.Productmanufacturername
        });
    }
}