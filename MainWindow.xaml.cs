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
using 记账.Pages;

namespace 记账
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = ((TabControl)sender).SelectedIndex;
            Page page;
            switch (selectedIndex)
            {
                case 0:
                    page = new Goods();
                    content.Content = page;
                    break;
                case 1:
                    page = new Customers();
                    content.Content = page;
                    break;
                case 2:
                    page = new Orders();
                    content.Content = page;
                    break;
            }
            
              


        }
    }
}
