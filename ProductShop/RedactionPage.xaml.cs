using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using ProductShop.BD;
namespace ProductShop
{
    /// <summary>
    /// Логика взаимодействия для RedactionPage.xaml
    /// </summary>
    public partial class RedactionPage : Page
    {
        public Product contProduct { get; set; }
        public RedactionPage(Product n)
        {
           
            InitializeComponent();
            contProduct = n;
            try
            {
                tb_id.Text = n.Id.ToString();
                tb_name.Text = n.Name;
                tb_description.Text = n.Description;
                //ImgProduct.Source = new BitmapImage(new Uri(contProduct.Photo));

                if (n.UnitId == 1)
                {
                    rb_kg.IsChecked = true;
                }
                else if (n.UnitId == 2)
                {
                    rb_st.IsChecked = true;
                }
                else if (n.UnitId == 3)
                {
                    rb_l.IsChecked = true;
                }
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            CountryCb.ItemsSource = bd_connection.connection.Country.ToList();
            CountryCb.DisplayMemberPath = "Name";
            UpdateCountry();

            this.DataContext = contProduct;
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            contProduct.Name = tb_name.Text;
            contProduct.Description = tb_description.Text;
            contProduct.AddDate = DateTime.Now;
            if (rb_kg.IsChecked == true)
            {
                contProduct.UnitId = 1;
            }
            else if (rb_l.IsChecked == true)
            {
                contProduct.UnitId = 3;
            }
            else
            {
                contProduct.UnitId = 2;
            }
            bd_connection.connection.SaveChanges();
            NavigationService.Navigate(new ListPage(ListPage.thisUser));
        }
        
        private void DelCountryBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddCountryBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CountryCb.SelectedIndex >= 0)
            {
                var countryProd = new ProductCountry();
                var selectCountry = CountryCb.SelectedItem as Country;
                countryProd.ProductId = contProduct.Id;
                countryProd.CountryId = selectCountry.Id;
                var isCountry = bd_connection.connection.ProductCountry.Where(a => a.CountryId == selectCountry.Id && a.ProductId == contProduct.Id).Count();
                if (isCountry == 0)
                {
                    bd_connection.connection.ProductCountry.Add(countryProd);
                    bd_connection.connection.SaveChanges();
                    UpdateCountry();
                }
            }
        }

        public void UpdateCountry()
        {
            CountryLv.ItemsSource = bd_connection.connection.ProductCountry.Where(a => a.ProductId == contProduct.Id).ToList();
        }

        private void btn_newphoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog()
            {
                Filter = "*.jpg|*.jpg|*.png|*.png"
            };
            if (openFile.ShowDialog().GetValueOrDefault())
            {
                contProduct.Photo = File.ReadAllBytes(openFile.FileName);
                ImgProduct.Source = new BitmapImage(new Uri(openFile.FileName));
            }
        }
    }
}
