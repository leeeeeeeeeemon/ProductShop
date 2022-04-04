using System;
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
    /// Логика взаимодействия для ListPage.xaml
    /// </summary>
    public partial class ListPage : Page
    {
        public static ObservableCollection<Product> products { get; set; }
        public ListPage()
        {
            products = new ObservableCollection<Product>(bd_connection.connection.Product.ToList());
            InitializeComponent();

            var Prod = new Product();
            this.DataContext = this;
        }

        private void tb_search_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (tb_search.Text != "")
            {
                prod.SelectedItem = null;
                prod.ItemsSource = new ObservableCollection<Product>(bd_connection.connection.Product.Where(z => (z.Name.Contains(tb_search.Text) || z.Description.Contains(tb_search.Text))).ToList());
            }
            else
            {
                this.DataContext = this;
            }
        }

        private void prod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var n = (sender as ListView).SelectedItem as Product;

            NavigationService.Navigate(new RedactionPage(n));
        }
    }
}
