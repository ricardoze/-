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
    public partial class Customers : Page
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

        ObservableCollection<Customer> dataList;
        ObservableCollection<String> CustomerTypes;
        public Customers()
        {
            InitializeComponent();
            refreshCustomers("全部");
            loadCustomerTypes();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Customer customer = new Customer
            {
               CustomerName ="",
                Phone = "",
                Address = "",
                CustomerType = "",
                Remarks = "",
                IsEnabled = 0
            };
            Window addCustomers = new AddCustomers(customer);
            addCustomers.Show();
            addCustomers.Closed += addCustomers_Closed;
        }

        private void addCustomers_Closed(object sender, EventArgs e)
        {
            Customer NewCustomer = ((AddCustomers)sender).WindowCustomer;
            if (NewCustomer != null)
            {
                refreshCustomers();
            }

        }

        private void BtnAction_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string customerId = button.Uid;

            Customer customer = ToObject<Customer>(new CustomerService().GetCustomerById(customerId));

            Window addCustomer = new AddCustomers(customer);
            addCustomer.Title = "编辑联系人";
            addCustomer.Show();
            addCustomer.Closed += addCustomers_Closed;
        }

        private void Disable_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string customerId = button.Uid;

            Customer customer = ToObject<Customer>(new CustomerService().GetCustomerById(customerId));
            if (customer.IsEnabled == 1)
            {
                customer.IsEnabled = 0;
            }
            else {
                customer.IsEnabled = 1;
            }
            new CustomerService().UpdateCustomer(customer);

            refreshCustomers(keyword.Text);
        }
        private void refreshCustomers(string Customertype = null, string Keyword = null)
        {
            if (Customertype == "全部")
            {
                dataList = ToObservableCollection<Customer>(new CustomerService().GetCustomersByTypeAndKeyword(keyword: Keyword));

            }
            else
            {
                dataList = ToObservableCollection<Customer>(new CustomerService().GetCustomersByTypeAndKeyword(Customertype,Keyword));

            }

            CustomersGrid.ItemsSource = dataList;
          
        }

        private void loadCustomerTypes()
        {
            CustomerTypes = new ObservableCollection<string>();
            CustomerTypes.Add("全部");
            var datat = new CustomerService().GetCustomerTypes();

            var query = from t in datat.AsEnumerable()
                        group t by new { t1 = t.Field<string>("customertype") } into m
                        select new
                        {
                            goodtype = m.Key.t1,
                            house = m.First().Field<string>("customertype"),
                            rowcount = m.Count()
                        };

            foreach (var item in query.ToList())
            {
                if (item.goodtype != null)
                {
                    CustomerTypes.Add(item.goodtype.ToString());
                }
            }
            customersTypes.ItemsSource = CustomerTypes;
            if (customersTypes.SelectedIndex < 0)
            {
                customersTypes.SelectedIndex = 0;
            }

        }


        private void Search_Click(object sender, RoutedEventArgs e)
        {
            refreshCustomers(Customertype: customersTypes.SelectedItem.ToString(), Keyword:keyword.Text);

        }

        private void CustomersType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            refreshCustomers(Customertype: customersTypes.SelectedItem.ToString(), Keyword: keyword.Text);
            CustomersGrid.ItemsSource = dataList;
        }
    }
   
}