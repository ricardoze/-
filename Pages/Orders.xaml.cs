using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.Linq;
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
    public partial class Orders : Page
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

        ObservableCollection<Order> dataList;
        ObservableCollection<String> orderTypes;
        public Orders()
        {
            InitializeComponent();
            loadorderTypes();
            refreshorders("全部");


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Order order = new Order();
        }

        private void Addorders_Closed(object sender, EventArgs e)
        {
            Order Neworder = ((AddOrder)sender).WindowOrder;
           
            refreshorders();

        }

        private void BtnAction_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string orderId = button.Uid;

            Order order = ToObject<Order>(new OrderService().GetOrderById(orderId));

            Window addorders = new AddOrder(order);
            addorders.Title = "编辑商品";
            addorders.Show();
            addorders.Closed += Addorders_Closed;
        }

        private void Disable_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string orderId = button.Uid;

            Order order = ToObject<Order>(new OrderService().GetOrderById(orderId));
            if (order!=null)
            {
                new OrderService().DeleteOrder(order);

            }


        }
        private void refreshorders(string orderType=null)
        {
            if (orderType == "全部" || string.IsNullOrEmpty(orderType))
            {
                dataList = ToObservableCollection<Order>(new OrderService().GetOrders());

            }
            else
            {
                dataList = ToObservableCollection<Order>(new OrderService().GetOrdersByType(orderType));

            }
           


            OrdersGrid.ItemsSource = dataList;


          
        }

        private void loadorderTypes()
        {
            orderTypes = new ObservableCollection<string>();
            orderTypes.Add("全部");
            var datat = new OrderService().GetOrderTypes();

            var query = from t in datat.AsEnumerable()
                        group t by new { t1 = t.Field<string>("ordertype") } into m
                        select new
                        {
                            ordertype = m.Key.t1,
                            house = m.First().Field<string>("ordertype"),
                            rowcount = m.Count()
                        };

            foreach (var item in query.ToList())
            {
                if (item.ordertype != null)
                {
                    orderTypes.Add(item.ordertype.ToString());
                }
            }
            
            
        }

        private void orderTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OrdersGrid.ItemsSource = dataList;
        }

        private void customers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AddOrder_Click(object sender, RoutedEventArgs e)
        {
            Order order = new Order();
            Window addOrder = new AddOrder(order);
            addOrder.ShowDialog();
            addOrder.Closed += Addorders_Closed;
        }
    }
   
}