using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using 记账.Model;
using 记账.Service;

namespace 记账.window
{
    /// <summary>
    /// AddGoods.xaml 的交互逻辑
    /// </summary>
    public partial class AddOrder : Window
    {
        private Order windowOrder;

        public AddOrder(Order order)
        {
            InitializeComponent();
            windowOrder = order;
           
        }

        public Order WindowOrder { get => windowOrder; set => windowOrder = value; }

        private void NumberBox_KeyDown(object sender, KeyEventArgs e)
        {
            //屏蔽非法按键
            if ((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9))
            {
                e.Handled = false;
            }
            else if ((e.Key >= Key.D0 && e.Key <= Key.D9))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
        
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            WindowOrder = null;
            this.Close();
        }

        private void Disable_Click(object sender, RoutedEventArgs e)
        {

        }

        private void customer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

     

        private void AddGood_Click(object sender, RoutedEventArgs e)
        {
            GoodSelect goodSelect = new GoodSelect();
            goodSelect.ShowDialog();


        }
    }
}
