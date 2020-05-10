using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Text;
using 记账.Model;

namespace 记账.Service
{
    public class GoodService
    {
        public DataTable GetGoods()
        {
            DataTable dt = new SqlService().ExecuteQuery("select * from Goods order by id;");
            return dt;

        }

        public DataTable GetGoodById(string id)
        {
            DataTable dt = new SqlService().ExecuteQuery("select * from Goods where id = '" +id+"';");
            return dt;

        }

        public void AddGood(Good good)
        {
            new SqlService().ExecuteNonQuery("INSERT INTO 'main'.'Goods'('Id', 'GoodName', 'SellPrice', 'InPrice', 'Remarks', 'IsEnabled', 'Unit')" +
                " VALUES ('"+good.Id+ "', '" + good.GoodName + "', '" + good.SellPrice + "', '" + good.InPrice + "', '" + good.Remarks + "', " + good.IsEnabled + ", '" + good.Unit + "');");

        }

        public void UpdateGood(Good good)
        {
            new SqlService().ExecuteNonQuery("update 'main'.'Goods' " +
                " set  GoodName ='" + good.GoodName + "', SellPrice = '" + good.SellPrice + "', InPrice='" + good.InPrice + "', Remarks='" + good.Remarks + "', IsEnabled=" 
                + good.IsEnabled + ", Unit= '" + good.Unit + "' where id ='"+ good .Id+ "';");

        }
    }
}
