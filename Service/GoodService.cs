using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Text;
using 记账.Model;

namespace 记账.Service
{
    public class GoodService : SqlService
    {
        public DataTable GetGoods()
        {
            DataTable dt =  ExecuteQuery("select * from Goods order by id;");
            return dt;

        }

        public DataTable GetGoodsByType(string goodtype)
        {
            DataTable dt =  ExecuteQuery("select * from Goods where goodtype = '"+ goodtype + "'order by id;");
            return dt;

        }

        public DataTable GetGoodTypes()
        {
            DataTable dt =  ExecuteQuery("SELECT goodtype FROM Goods;");
            return dt;

        }

        public DataTable GetGoodById(string id)
        {
            DataTable dt =  ExecuteQuery("select * from Goods where id = '" +id+"';");
            return dt;

        }

        public void AddGood(Good good)
        {
             ExecuteNonQuery("INSERT INTO 'main'.'Goods'('Id', 'GoodName', 'SellPrice', 'InPrice', 'Remarks', 'IsEnabled', 'Unit', 'SinglePrice', 'GoodType')" +
                " VALUES ('"+good.Id+ "', '" + good.GoodName + "', '" + good.SellPrice + "', '" + good.InPrice + "', '" + good.Remarks + "', " + good.IsEnabled + ", '" + good.Unit + "', '" + good.SinglePrice + "', '" + good.GoodType + "');");

        }

        public void UpdateGood(Good good)
        {
             ExecuteNonQuery("update 'main'.'Goods' " +
                " set  GoodName ='" + good.GoodName + "', SellPrice = '" + good.SellPrice + "', SinglePrice = '" + good.SinglePrice + "', GoodType = '" + good.GoodType + "', InPrice='" + good.InPrice + "', Remarks='" + good.Remarks + "', IsEnabled=" 
                + good.IsEnabled + ", Unit= '" + good.Unit + "' where id ='"+ good .Id+ "';");

        }
    }
}
