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

namespace ProductShop
{
    /// <summary>
    /// Логика взаимодействия для RedactionPage.xaml
    /// </summary>
    public partial class RedactionPage : Page
    {
        public RedactionPage(Product n)
        {
            InitializeComponent();

            tb_id.Text = n.Id.ToString();
            tb_name.Text = n.Name;
            tb_description.Text = n.Description;

            if(n.UnitId == 1)
            {
                rb_kg.IsChecked = true;
            }
            else if(n.UnitId == 2)
            {
                rb_st.IsChecked = true;
            }
            else if(n.UnitId == 3)
            {
                rb_l.IsChecked = true;
            }
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
