using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using DemoStroi.Context;
using DemoStroi.Models;
using MessageBox.Avalonia;
using MessageBox.Avalonia.Enums;

namespace DemoStroi;

public partial class Autorization : Window
{
    public static User003Context _context;
    
    public Autorization()
    {
        InitializeComponent();
        Manager.captcha = true;
        
        _context = User003Context.GetContext();
        
    }
    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        var user = new User();
        try
        {
            user = _context.Users.Where(x => x.Userlogin == TextBoxLogin.Text ).ToList().First();

            if (user.Userpassword == TextBoxPassword.Text && Manager.count == 0 && Manager.captcha == true)
            {
                Manager.idUser = user.Userid; 
                Manager.nameUser = user.Usersurname + " " + user.Username + " " + user.Userpatronymic;
                Manager.idRole = user.Userrole;
                if (Manager.idRole == 1)
                {
                    AdminWindow adminWindow = new AdminWindow();
                    adminWindow.Show();
                    this.Close();
                }
                else
                {
                    UserWindow userWindow = new UserWindow();
                    userWindow.Show();
                    this.Close(); 
                } 
            }
            else if (user.Userpassword == TextBoxPassword.Text &&  (Manager.count != 0 || Manager.captcha != true))
            {
                Manager.captcha = false;
                Manager.count = 1;
                Manager.idUser = user.Userid;
                Manager.nameUser = user.Usersurname + " " + user.Username + " " + user.Userpatronymic;
                Manager.idRole = user.Userrole;
                TextBlockLogin.IsVisible = false;
                TextBlockPassword.IsVisible = false;
                TextBoxLogin.IsVisible = false;
                TextBoxPassword.IsVisible = false;
                ButtonLog.IsVisible = false;
                Captcha.IsVisible = true;
                var abc = MessageBoxManager
                    .GetMessageBoxStandardWindow("Ошибка", "Введите код",
                        ButtonEnum.Ok).Show();
            }
            else
            {
                Manager.captcha = false;
                Manager.count = 1;
                Manager.idUser = user.Userid;
                Manager.nameUser = user.Usersurname + " " + user.Username + " " + user.Userpatronymic;
                Manager.idRole = user.Userrole;
                TextBlockLogin.IsVisible = false;
                TextBlockPassword.IsVisible = false;
                TextBoxLogin.IsVisible = false;
                TextBoxPassword.IsVisible = false;
                ButtonLog.IsVisible = false;
                Captcha.IsVisible = true;
                var abc = MessageBoxManager
                    .GetMessageBoxStandardWindow("Ошибка", "Введите код",
                        ButtonEnum.Ok).Show();
            }
            
            
        }
        catch (Exception exception)
        {
            var box = MessageBoxManager
                .GetMessageBoxStandardWindow("Ошибка", "Неверный логин ",
                    ButtonEnum.Ok).Show();
            if (Manager.count == 1)
            {
                Manager.captcha = false;
                Manager.count = 1;
                Manager.idUser = user.Userid;
                Manager.nameUser = user.Usersurname + " " + user.Username + " " + user.Userpatronymic;
                Manager.idRole = user.Userrole;
                TextBlockLogin.IsVisible = false;
                TextBlockPassword.IsVisible = false;
                TextBoxLogin.IsVisible = false;
                TextBoxPassword.IsVisible = false;
                ButtonLog.IsVisible = false;
                Captcha.IsVisible = true;
                var abc = MessageBoxManager
                    .GetMessageBoxStandardWindow("Ошибка", "Введите код",
                        ButtonEnum.Ok).Show();
            }
            
            Manager.count = 1;
        }
        
        
        
        
        /*try
        {
            if (TextBoxLogin.Text == null || TextBoxLogin.Text  == "" || TextBoxPassword.Text == null || TextBoxPassword.Text == "") 
            {
                Manager.captcha = false;
                var box = MessageBoxManager
                    .GetMessageBoxStandardWindow("Ошибка", "Введите логин и пароль",
                        ButtonEnum.Ok).Show();
                Manager.count = 1; 
            }
            else 
            { 
                if( Manager.count != 0 ) 
                {
                    Manager.captcha = false;
                    Manager.count = 0;
                    Manager.idUser = user.Userid;
                    Manager.nameUser = user.Usersurname + " " + user.Username + " " + user.Userpatronymic;
                    Manager.idRole = user.Userrole;
                    TextBlockLogin.IsVisible = false;
                    TextBlockPassword.IsVisible = false;
                    TextBoxLogin.IsVisible = false;
                    TextBoxPassword.IsVisible = false;
                    ButtonLog.IsVisible = false;
                    Captcha.IsVisible = true;
                    var box = MessageBoxManager
                        .GetMessageBoxStandardWindow("Ошибка", "Введите код",
                            ButtonEnum.Ok).Show(); 
                }
                else if (user.Userpassword == TextBoxPassword.Text && Manager.captcha == true) \
                { 
                    Manager.idUser = user.Userid; 
                    Manager.nameUser = user.Usersurname + " " + user.Username + " " + user.Userpatronymic;
                    Manager.idRole = user.Userrole;
                    if (Manager.idRole == 1)
                    {
                        AdminWindow adminWindow = new AdminWindow();
                        adminWindow.Show();
                        this.Close();
                    }
                    else
                    {
                        UserWindow userWindow = new UserWindow();
                        userWindow.Show();
                        this.Close(); 
                    } 
                }
                
            }
        }
        catch (Exception exception)
        {
            
        }*/
    
        
    }

    private void GuestBut_OnClick(object? sender, RoutedEventArgs e)
    {
        UserWindow userWindow = new UserWindow();
        userWindow.Show();
        this.Close();
    }
}