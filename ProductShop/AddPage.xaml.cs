using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ProductShop
{
    /// <summary>
    /// Логика взаимодействия для AddPage.xaml
    /// </summary>
    public partial class AddPage : Page
    {
        public static ObservableCollection<Product> products { get; set; }
        public AddPage()
        {
            InitializeComponent();
        }
        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void tb_name_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetter(e.Text, 0) && e.Text != "-")
            {
                e.Handled = true;
            }
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            var new_product = new Product();
            new_product.Name = tb_name.Text;
            new_product.Description = tb_description.Text;
            new_product.AddDate = DateTime.Now;
            if (rb_kg.IsChecked == true)
            {
                new_product.UnitId = 1;
            }
            else if (rb_l.IsChecked == true)
            {
                new_product.UnitId = 3;
            }
            else
            {
                new_product.UnitId = 2;
            }

            bd_connection.connection.Product.Add(new_product);
            bd_connection.connection.SaveChanges();

            NavigationService.GoBack();
        }
    }
}
