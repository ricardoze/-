using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using 记账.Model;

namespace 记账.Service
{
    public class OrderService : SqlService
    {
        public DataTable GetOrders()
        {
            DataTable dt =  ExecuteQuery("select * from Orders order by id;");
            return dt;

        }

        public DataTable GetOrdersByType(string Ordertype)
        {
            DataTable dt =  ExecuteQuery("select * from Orders where Ordertype = '"+ Ordertype + "'order by id;");
            return dt;

        }

        public DataTable GetOrderTypes()
        {
            DataTable dt =  ExecuteQuery("SELECT Ordertype FROM Orders;");
            return dt;

        }
        public ObservableCollection<string> loadOrderTypes()
        {
            ObservableCollection<String>  OrderTypes = new ObservableCollection<string>();
            OrderTypes.Add("全部");
            var datat = GetOrderTypes();

            var query = from t in datat.AsEnumerable()
                        group t by new { t1 = t.Field<string>("Ordertype") } into m
                        select new
                        {
                            Ordertype = m.Key.t1,
                            house = m.First().Field<string>("Ordertype"),
                            rowcount = m.Count()
                        };

            foreach (var item in query.ToList())
            {
                if (item.Ordertype != null)
                {
                    OrderTypes.Add(item.Ordertype.ToString());
                }
            }
            return OrderTypes;
          


        }

        public DataTable GetOrderById(string id)
        {
            DataTable dt =  ExecuteQuery("select * from Orders where id = '" +id+"';");
            return dt;

        }

        public void AddOrder(Order Order)
        {
             ExecuteNonQuery("INSERT INTO Order('Id', 'OrderTime', 'customerId', 'Remarks', 'IsPayed', 'OrderType', 'TotalPrice') VALUES ('"+Order.Id+ "', '" + Order.OrderTime.Date + "', '" 
                 + Order.CustomerId + "', '" + Order.Remarks + "', " + Order.IsPayed + ", '" + Order.OrderType + "', '" + Order.TotalPrice + "');");

        }

        public void UpdateOrder(Order Order)
        {
             ExecuteNonQuery("UPDATE Order SET 'OrderTime' = '" + Order.OrderTime.Date + "', 'customerId' = '"
                 + Order.CustomerId + "', 'Remarks' = '" + Order.Remarks + "', 'IsPayed' =" + Order.IsPayed + ", 'OrderType' = '" + Order.OrderType + "', 'TotalPrice' = '" + Order.TotalPrice + "' WHERE Id = '" + Order.Id + "';");

        }
    }
}
