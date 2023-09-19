using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MessageBox.Avalonia;
using MessageBox.Avalonia.Enums;

namespace DemoStroi;

public partial class CaptchaControlWin : UserControl
{
    private string textCapt = "";
    
    
    public CaptchaControlWin()
    {
        InitializeComponent();
        CreateText();
        PrintCapt();
    }
    
    private void ButtonEnterCaptcha_OnClick(object? sender, RoutedEventArgs e)
    {
        if (textCapt == TextBoxCaptcha.Text)
        {
            if (Manager.idRole == 0)
            {
                Autorization autorization = new Autorization();
                autorization.Show();
                Manager.count = 1;
            }
            else if (Manager.idRole == 1)
            {
                AdminWindow adminWindow = new AdminWindow();
                adminWindow.Show();
            }
            else
            {
                Manager.captcha = true;
                UserWindow userWindow = new UserWindow();
                userWindow.Show();
            }
            if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desltop)
            {
                desltop.Windows.OfType<Autorization>().First().Close();
            }
            
        }
        else
        {
            var box = MessageBoxManager
                .GetMessageBoxStandardWindow("Ошибка", "При вводе кода вы допустили ошибку",
                    ButtonEnum.Ok).Show();
            
            CreateText();
            PrintCapt();
        }

        TextBoxCaptcha.Text = null;
    }

    public void PrintCapt()
    {
        Random random = new Random();
        int x1 = random.Next(18, 26);
        int x2 = random.Next(18, 26);
        int x3 = random.Next(18, 26);
        int x4 = random.Next(18, 26);
        int x5 = random.Next(18, 26);
        TextBlock1.Text = textCapt[0].ToString();
        TextBlock1.FontSize = x1;
        TextBlock2.Text = textCapt[1].ToString();
        TextBlock2.FontSize = x2;
        TextBlock3.Text = textCapt[2].ToString();
        TextBlock3.FontSize = x3;
        TextBlock4.Text = textCapt[3].ToString();
        TextBlock4.FontSize = x4;
        TextBlock5.Text = textCapt[4].ToString();
        TextBlock5.FontSize = x5;
    }

    public void CreateText()
    {
        List<char> list = new List<char>(){'a','b','c','d','e','f','g','h','i','k',
            'l','m','n','o','p','q','r','s','t','v',
            'x','z','y','u','w','1','2','3','4','5','6','7','8','9'};
        Random random = new Random();
        String str = "";
        for (int i = 0; i < 5; i++)
        {
            int a = random.Next(0, list.Count - 1);
            str = str + list[a];
            
        }

        textCapt = str;
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        CreateText();
        PrintCapt();
    }

    private void ButtonChangeAcc_OnClick(object? sender, RoutedEventArgs e)
    {
        Autorization mainWindow = new Autorization();
        mainWindow.Show();
        Manager.count = 1;

        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime descktop)
        {
            descktop.Windows.OfType<Autorization>().First().Close();
        }
        
        
        
    }
}