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
    public partial class AddGoods : Window
    {
        private Good windowGood;

        public AddGoods(Good good)
        {
            InitializeComponent();
            WindowGood = good;
            GoodName.DataContext = WindowGood;
            Unit.DataContext = WindowGood;
            SellPrice.DataContext = WindowGood;
            InPrice.DataContext = WindowGood;
            Remarks.DataContext = WindowGood;
            IsEnabled.DataContext = WindowGood;
            SinglePrice.DataContext = WindowGood;
            GoodType.DataContext = WindowGood;
        }

        public Good WindowGood { get => windowGood; set => windowGood = value; }

        private void NumberBox_KeyDown(object sender, KeyEventArgs e)
        {
            //屏蔽非法按键
            if ((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9))
            {
                e.Handled = true;
            }
            else if ((e.Key >= Key.D0 && e.Key <= Key.D9))
            {
                e.Handled = true;
            }
            else if ((e.Key == Key.Back  || e.Key == Key.Delete))
            {
                e.Handled = true;
            } 
            else if ((e.Key == Key.Left  || e.Key == Key.Right))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(WindowGood.GoodName))
            {
                GoodName.Text = "商品名不能为空";
                return;
            }
            if (string.IsNullOrEmpty(WindowGood.Unit))
            {
                Unit.Text = "单位不能为空";
                return;
            }
            if (string.IsNullOrEmpty(WindowGood.SellPrice))
            {
                SellPrice.Text = "售价不能为空";
                return;
            }
            if (string.IsNullOrEmpty(WindowGood.InPrice) )
            {
                InPrice.Text = "进价不能为空";
                return;
            }
            if (string.IsNullOrEmpty(WindowGood.SinglePrice) )
            {
                SinglePrice.Text = "单价不能为空";
                return;
            }
            if (string.IsNullOrEmpty(WindowGood.GoodType) )
            {
                GoodType.Text = "类型不能为空";
                return;
            }

            if (string.IsNullOrEmpty(WindowGood.Id))
            {
                WindowGood.Id = Guid.NewGuid().ToString();
                new GoodService().AddGood(WindowGood);
            }
            else
            {
                new GoodService().UpdateGood(WindowGood);
            }
           
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            WindowGood = null;
            this.Close();
        }
    }
}
