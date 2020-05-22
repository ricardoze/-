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
            firstRatio.IsChecked = true;
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
         


        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            string selectedIndex = ((RadioButton)sender).Uid;
            if (contentFrame == null)
            {
                contentFrame = new Frame();
            }
            Page page;
            switch (selectedIndex)
            {
                case "1":
                    page = new Goods();
                    contentFrame.Content = page;
                    break;
                case "2":
                    page = new Customers();
                    contentFrame.Content = page;
                    break;
                case "3":
                    page = new Orders();
                    contentFrame.Content = page;
                    break;
            }


        }
    }
}
