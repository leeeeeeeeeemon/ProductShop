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

        public static User thisUser;
        public ListPage(User usr)
        {
            products = new ObservableCollection<Product>(bd_connection.connection.Product.ToList());
            InitializeComponent();

            var Prod = new Product();
            this.DataContext = this;

            thisUser = usr;

            var allUnit = new ObservableCollection<Unit>(bd_connection.connection.Unit.ToList());
            allUnit.Insert(0, new Unit() { Id = -1, Name = "Все" });

            cb_unit.ItemsSource = allUnit;
            cb_unit.DisplayMemberPath = "Name";

            if (thisUser.RoleId == 3)
            {
                AddBtn.Visibility = Visibility.Hidden;
                DelBtn.Visibility = Visibility.Hidden;
                ChangeBtn.Visibility = Visibility.Hidden;
            }
        }


        private void prod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        public void Filter()
        {
            var filterProd = (IEnumerable<Product>)bd_connection.connection.Product.Where(a => a.IsDeleted != true).ToList();

            if (tb_search.Text != "")
            {
                filterProd = bd_connection.connection.Product.Where(z => (z.Name.Contains(tb_search.Text) || z.Description.Contains(tb_search.Text)));
            }

            if (cb_unit.SelectedIndex > 0)
            {
                filterProd = filterProd.Where(c => c.UnitId == (cb_unit.SelectedItem as Unit).Id || c.UnitId == -1);
            }

            if (cb_alf.SelectedIndex == 1)
            {
                filterProd = filterProd.OrderBy(c => c.Name);
            }
            else if (cb_alf.SelectedIndex == 2)
            {
                filterProd = filterProd.OrderByDescending(c => c.Name);
            }

            if (cb_date.SelectedIndex == 1)
            {
                filterProd = filterProd.OrderBy(c => c.AddDate);
            }
            else if (cb_date.SelectedIndex == 2)
            {
                filterProd = filterProd.OrderByDescending(c => c.AddDate);
            }


            if (cb_mounth.IsChecked.GetValueOrDefault())
            {
                var date = DateTime.Now.Month;
                filterProd = filterProd.Where(c => c.AddDate.Month == date);
            }

            prod.ItemsSource = filterProd;
        }

        private void cb_unit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void tb_search_SelectionChanged(object sender, RoutedEventArgs e)
        {
            Filter();
        }

        private void cb_mounth_Click(object sender, RoutedEventArgs e)
        {
            Filter();
        }

        private void ChangeBtn_Click(object sender, RoutedEventArgs e)
        {
            if(prod.SelectedItem != null)
            {
                var n = prod.SelectedItem as Product;

                NavigationService.Navigate(new RedactionPage(n));
            }
            else
            {
                MessageBox.Show("Выберете продукт");
            }
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {
            if (prod.SelectedItem != null)
            {
                var n = prod.SelectedItem as Product;

                n.IsDeleted = true;

                Filter();
            }
            else
            {
                MessageBox.Show("Выберете продукт");
            }
        }
    }
}
