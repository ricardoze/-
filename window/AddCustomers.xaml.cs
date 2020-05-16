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
    public partial class AddCustomers : Window
    {
        private Customer windowCustomer;

        public AddCustomers(Customer customer)
        {
            InitializeComponent();
            WindowCustomer = customer;
            CustomerName.DataContext = WindowCustomer;
            CustomerType.DataContext = WindowCustomer;
            Phone.DataContext = WindowCustomer;
            Address.DataContext = WindowCustomer;
            Remarks.DataContext = WindowCustomer;
            IsEnabled.DataContext = WindowCustomer;
            
        }

        public Customer WindowCustomer { get => windowCustomer; set => windowCustomer = value; }

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
            if (string.IsNullOrEmpty(windowCustomer.CustomerName))
            {
                CustomerName.Text = "名字不能为空";
                return;
            }
            if (string.IsNullOrEmpty(windowCustomer.CustomerType))
            {
                CustomerType.Text = "类型不能为空";
                return;
            }
            if (string.IsNullOrEmpty(windowCustomer.Phone))
            {
                Phone.Text = "手机不能为空";
                return;
            }
            if (string.IsNullOrEmpty(windowCustomer.Address) )
            {
                Address.Text = "地址不能为空";
                return;
            }
            
            

            if (string.IsNullOrEmpty(windowCustomer.Id))
            {
                windowCustomer.Id = Guid.NewGuid().ToString();
                new CustomerService().AddCustomer(windowCustomer);
            }
            else
            {
                new CustomerService().UpdateCustomer(windowCustomer);
            }
           
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            windowCustomer = null;
            this.Close();
        }
    }
}
