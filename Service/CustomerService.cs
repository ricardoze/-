using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Text;
using 记账.Model;

namespace 记账.Service
{
    public class CustomerService : SqlService
    {
        public DataTable GetCustomers()
        {
            DataTable dt = ExecuteQuery("select * from Customers order by id;");
            return dt;

        }

        public DataTable GetCustomersByTypeAndKeyword(string Customertype = null, string keyword = null)
        {
            Dictionary<string, object> equalkv = new Dictionary<string, object>();
            Dictionary<string, object> likekv = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(Customertype))
            {
                equalkv["Customertype"]= Customertype;
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                likekv["CustomerName"] = keyword;

                likekv["phone"] = keyword;
            }
            DataTable dt = ExecuteKV2Where("select * from Customers ", equalkv, likekv);
            return dt;

        }
        
        public DataTable GetCustomersByKeyword(string keyword)
        {
            DataTable dt = ExecuteQuery("SELECT * FROM Customer where CustomerName like '%"+ keyword + "%' or phone like '%"+ keyword + "%';");
            return dt;

        }

        public DataTable GetCustomerTypes()
        {
            DataTable dt = ExecuteQuery("SELECT Customertype FROM Customers;");
            return dt;

        }

        public DataTable GetCustomerById(string id)
        {
            DataTable dt = ExecuteQuery("select * from Customers where id = '" +id+"';");
            return dt;

        }

        public void AddCustomer(Customer Customer)
        {
           ExecuteNonQuery("INSERT INTO 'main'.'Customers'('Id', 'CustomerName', 'Phone', 'Address', 'Remarks', 'IsEnabled', 'CustomerType')" +
                " VALUES ('"+Customer.Id+ "', '" + Customer.CustomerName + "', '" + Customer.Phone + "', '" + Customer.Address + "', '" + Customer.Remarks + "', "
                + Customer.IsEnabled + ", '" + Customer.CustomerType + "');");

        }

        public void UpdateCustomer(Customer Customer)
        {
            ExecuteNonQuery("update 'main'.'Customers' " +
                " set  CustomerName ='" + Customer.CustomerName + "', CustomerType = '" + Customer.CustomerType + "', Phone = '" + Customer.Phone + "', Remarks='" + Customer.Remarks + "', IsEnabled=" 
                + Customer.IsEnabled + ", Address= '" + Customer.Address + "' where id ='"+ Customer .Id+ "';");

        }
    }
}
