﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace ProductShop
{
    /// <summary>
    /// Логика взаимодействия для AutorizationPage.xaml
    /// </summary>
    public partial class AutorizationPage : Page
    {
        public static ObservableCollection<User> users { get; set; }
        public AutorizationPage()
        {
            InitializeComponent();
            tb_login.Text = Properties.Settings.Default.Login;
        }

        private void btn_reg_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegistrationPage());
        }

        private void btn_authorization_Click(object sender, RoutedEventArgs e)
        {
            users = new ObservableCollection<User>(bd_connection.connection.User.ToList());
            var z = users.Where(a => a.Login == tb_login.Text && a.Password == tb_password.Text).FirstOrDefault();
            if (z != null)
            {
                if(cb_save.IsChecked.GetValueOrDefault())
                {
                    Properties.Settings.Default.Login = tb_login.Text;
                    Properties.Settings.Default.Save();
                }
                else
                {
                    Properties.Settings.Default.Login = null;
                    Properties.Settings.Default.Save();
                }
                NavigationService.Navigate(new ListPage());
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль", "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
