using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 复习
{
    public partial class SqliteHelper
    {
        // 从配置文本中读取连接字符串
        private static string connStr = ConfigurationManager.ConnectionStrings["itCaster"].ConnectionString;

        // 执行命令的方法：insert/delete/update
        public static int ExecuteNonQuery(string sql, params SQLiteParameter[]  ps)
        {
            using(SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddRange(ps);
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        // 获取首行首列值的方法
        public static object ExecuteScalar(string sql, params SQLiteParameter[] ps)
        {
            using(SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddRange(ps);
                conn.Open();
                // 执行命令，获取查询结果中的首行首列的值，返回
                return cmd.ExecuteScalar();
            }
        }

        // 获取结果集
        public static DataTable GetDataTable(string sql, params SQLiteParameter[] ps)
        {
            using(SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                // 构造适配器对象
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, conn);
                // 构造数据表，用于接收查询结果
                DataTable dt = new DataTable();

                // 添加参数
                adapter.SelectCommand.Parameters.AddRange(ps);
                // 执行结果
                adapter.Fill(dt);
                // 返回结果集
                return dt;
            }
        }

    }
}
