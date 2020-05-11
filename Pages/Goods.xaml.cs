using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using 记账.Model;
using 记账.Service;
using 记账.window;

namespace 记账.Pages
{
    /// <summary>
    /// Page1.xaml 的交互逻辑
    /// </summary>
    public partial class Goods : Page
    {
        public ObservableCollection<T> ToObservableCollection<T>(DataTable dt) where T : class, new()
        {
            Type t = typeof(T);
            PropertyInfo[] propertys = t.GetProperties();
            ObservableCollection<T> lst = new ObservableCollection<T>();
            string typeName = string.Empty;
            foreach (DataRow dr in dt.Rows)
            {
                T entity = new T();
                foreach (PropertyInfo pi in propertys)
                {
                    typeName = pi.Name;
                    if (dt.Columns.Contains(typeName))
                    {
                        if (!pi.CanWrite) continue;
                        object value = dr[typeName];
                        if (value == DBNull.Value) continue;
                        if (pi.PropertyType == typeof(string))
                        {
                            pi.SetValue(entity, value.ToString(), null);
                        }
                        else if (pi.PropertyType == typeof(int) || pi.PropertyType == typeof(int?))
                        {
                            pi.SetValue(entity, int.Parse(value.ToString()), null);
                        }
                        else if (pi.PropertyType == typeof(DateTime?) || pi.PropertyType == typeof(DateTime))
                        {
                            pi.SetValue(entity, DateTime.Parse(value.ToString()), null);
                        }
                        else if (pi.PropertyType == typeof(float))
                        {
                            pi.SetValue(entity, float.Parse(value.ToString()), null);
                        }
                        else if (pi.PropertyType == typeof(double))
                        {
                            pi.SetValue(entity, double.Parse(value.ToString()), null);
                        }
                        else
                        {
                            pi.SetValue(entity, value, null);
                        }
                    }
                }
                lst.Add(entity);
            }
            return lst;
        }

        public T ToObject<T>(DataTable dt) where T : class, new()
        {
            Type t = typeof(T);
            PropertyInfo[] propertys = t.GetProperties();
            List<T> lst = new List<T>();
            string typeName = string.Empty;
            foreach (DataRow dr in dt.Rows)
            {
                T entity = new T();
                foreach (PropertyInfo pi in propertys)
                {
                    typeName = pi.Name;
                    if (dt.Columns.Contains(typeName))
                    {
                        if (!pi.CanWrite) continue;
                        object value = dr[typeName];
                        if (value == DBNull.Value) continue;
                        if (pi.PropertyType == typeof(string))
                        {
                            pi.SetValue(entity, value.ToString(), null);
                        }
                        else if (pi.PropertyType == typeof(int) || pi.PropertyType == typeof(int?))
                        {
                            pi.SetValue(entity, int.Parse(value.ToString()), null);
                        }
                        else if (pi.PropertyType == typeof(DateTime?) || pi.PropertyType == typeof(DateTime))
                        {
                            pi.SetValue(entity, DateTime.Parse(value.ToString()), null);
                        }
                        else if (pi.PropertyType == typeof(float))
                        {
                            pi.SetValue(entity, float.Parse(value.ToString()), null);
                        }
                        else if (pi.PropertyType == typeof(double))
                        {
                            pi.SetValue(entity, double.Parse(value.ToString()), null);
                        }
                        else
                        {
                            pi.SetValue(entity, value, null);
                        }
                    }
                }
                lst.Add(entity);
            }
            return lst[0];
        }

        ObservableCollection<Good> dataList;
        public Goods()
        {
            InitializeComponent();
            refreshGoods();


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Good good = new Good
            {
                GoodName = "",
                Unit = "",
                SellPrice = "",
                InPrice = "",
                Remarks = "",
                IsEnabled = 0
            };
            Window addGoods = new AddGoods(good);
            addGoods.Show();
            addGoods.Closed += AddGoods_Closed;
        }

        private void AddGoods_Closed(object sender, EventArgs e)
        {
            Good NewGood = ((AddGoods)sender).WindowGood;
            if (NewGood != null)
            {
                refreshGoods();
            }

        }

        private void BtnAction_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string goodId = button.Uid;

            Good good = ToObject<Good>(new GoodService().GetGoodById(goodId));

            Window addGoods = new AddGoods(good);
            addGoods.Title = "编辑商品";
            addGoods.Show();
            addGoods.Closed += AddGoods_Closed;
        }

        private void BtnAction1_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string goodId = button.Uid;

            Good good = ToObject<Good>(new GoodService().GetGoodById(goodId));
            if (good.IsEnabled == 1)
            {
                good.IsEnabled = 0;
            }
            else {
                good.IsEnabled = 1;
            }
            new GoodService().UpdateGood(good);
            refreshGoods();
        }
        private void refreshGoods()
        {
            dataList = ToObservableCollection<Good>(new GoodService().GetGoods());
            GoodsGrid.ItemsSource = dataList;
        }

    }
   
}