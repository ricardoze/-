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
            DataTable dt = SqlService.ExecuteQuery("select * from Goods order by id;");
            return dt;

        }
    }
}
