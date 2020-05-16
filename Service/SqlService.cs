using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Text;

namespace 记账.Service
{
    public class SqlService
    {
        public static SQLiteHelper sh;
        // 数据库文件夹
        static string DbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database");

        //与指定的数据库(实际上就是一个文件)建立连接
        private static SQLiteConnection CreateDatabaseConnection(string dbName = null)
        {
            if (!string.IsNullOrEmpty(DbPath) && !Directory.Exists(DbPath))
                Directory.CreateDirectory(DbPath);
            dbName = dbName == null ? "database.db" : dbName;
            var dbFilePath = Path.Combine(DbPath, dbName);
            return new SQLiteConnection("DataSource = " + dbFilePath);
        }

        // 使用全局静态变量保存连接
        private SQLiteConnection connection = CreateDatabaseConnection();

        // 判断连接是否处于打开状态
        private static void Open(SQLiteConnection connection)
        {
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }
        }

        public  void ExecuteNonQuery(string sql)
        {
            using (SQLiteConnection conn = connection)
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    connection.Open();
                    sh = new SQLiteHelper(cmd);
                    sh.Execute(sql);
                    connection.Dispose();
                }
            }
        }

        public  DataTable ExecuteQuery(string sql)
        {
            DataTable dataTable;
            using (SQLiteConnection conn =connection)
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    connection.Open();
                    sh = new SQLiteHelper(cmd);
                     dataTable =  sh.Select(sql);
                    connection.Dispose();
                    // do something...
                }
            }
            return dataTable;
        }

        public DataTable ExecuteKV2Where(string headsql, Dictionary<string, object> equalDic, Dictionary<string, object> likeDic)
        {
            StringBuilder stringBuilder = new StringBuilder(headsql);
            bool firstRecord = true;


            if (equalDic.Count > 0 || likeDic.Count > 0)
            {
                stringBuilder.Append(" where ");
            }

            foreach (KeyValuePair<string, object> kv in equalDic)
            {
                if (firstRecord)
                    firstRecord = false;
                else
                {
                    stringBuilder.Append(" and ");
                }

                stringBuilder.Append(" ");
                stringBuilder.Append(kv.Key);
                stringBuilder.Append(" = '");
                stringBuilder.Append(kv.Value);
                stringBuilder.Append("' ");

            }
            firstRecord = true;
            foreach (KeyValuePair<string, object> kv in likeDic)
            {
                if (firstRecord)
                    firstRecord = false;
                else
                {
                    stringBuilder.Append(" or ");
                }

                stringBuilder.Append(" ");
                stringBuilder.Append(kv.Key);
                stringBuilder.Append(" like '%");
                stringBuilder.Append(kv.Value);
                stringBuilder.Append("%' ");
            }

            stringBuilder.Append(";");

            DataTable dataTable;
            using (SQLiteConnection conn = connection)
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    connection.Open();
                    sh = new SQLiteHelper(cmd);
                    dataTable = sh.Select(stringBuilder.ToString());
                    connection.Dispose();
                    // do something...
                }
            }
            return dataTable;
        }

    }
}
