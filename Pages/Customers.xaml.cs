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
       

        ObservableCollection<Customer> dataList;
        ObservableCollection<string> CustomerTypes;
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
           
            refreshCustomers();

        }

        private void BtnAction_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string customerId = button.Uid;

            Customer customer = Util.ToObject<Customer>(new CustomerService().GetCustomerById(customerId));

            Window addCustomer = new AddCustomers(customer);
            addCustomer.Title = "编辑联系人";
            addCustomer.Show();
            addCustomer.Closed += addCustomers_Closed;
        }

        private void Disable_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string customerId = button.Uid;

            Customer customer = Util.ToObject<Customer>(new CustomerService().GetCustomerById(customerId));
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
            if (Customertype == "全部" ||(string.IsNullOrEmpty(Customertype)&& string.IsNullOrEmpty(Keyword)))
            {
                dataList = Util.ToObservableCollection<Customer>(new CustomerService().GetCustomersByTypeAndKeyword(keyword: Keyword));

            }
            else
            {
                dataList = Util.ToObservableCollection<Customer>(new CustomerService().GetCustomersByTypeAndKeyword(Customertype,Keyword));

            }
           
            CustomersGrid.ItemsSource = dataList;
          
        }

        private void loadCustomerTypes()
        {
            CustomerTypes = new ObservableCollection<string>();
            CustomerTypes.Add("全部");
            var datat = new CustomerService().GetCustomerTypes();

          

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