using CaterCommon;
using CaterModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaterDal
{
   public partial class ManagerInfoDal
    {
        // 查询获取结果集
        public List<ManagerInfo> GetList()
        {
            string sql = "select * from ManagerInfo";
            // 使用helper进行查询，得到结果
            DataTable dt = SqliteHelper.GetDataTable(sql);
            List<ManagerInfo> list = new List<ManagerInfo>();
            foreach(DataRow row in dt.Rows)
            {
                list.Add(new ManagerInfo() {
                    MId = Convert.ToInt32(row["mid"]),
                    MName = row["mname"].ToString(),
                    MPwd = row["mpwd"].ToString(),
                    MType = Convert.ToInt32(row["mtype"])
                });
            }
            // 将集合返回
            return list;
        }

        // 插入操作
        public int Insert(ManagerInfo mi)
        {
            // 构造insert语句
            string sql = "insert into ManagerInfo (mname, mpwd, mtype) values (@name, @pwd, @type)";

            // 构造sql语句的参数
            SQLiteParameter[] ps =
            {
                // 使用数组的初始化器
                new SQLiteParameter("@name",mi.MName),
                // 将密码进行MD5加密
                new SQLiteParameter("@pwd",Md5Helper.EncryptString(mi.MPwd)),
                new SQLiteParameter("@type",mi.MType)
            };
            // 执行插入操作
            return SqliteHelper.ExecuteNonQuery(sql, ps);
        }

        // 修改操作
        public int Update(ManagerInfo mi)
        {
            string sql = "update ManagerInfo set mname=@name,mpwd=@pwd, mtype=@type where mid=@id";
            SQLiteParameter[] ps =
            {
                new SQLiteParameter("@name",mi.MName),
                new SQLiteParameter("@pwd",mi.MPwd),
                new SQLiteParameter("@type",mi.MType),
                new SQLiteParameter("@id",mi.MId)
            };
            return SqliteHelper.ExecuteNonQuery(sql, ps);
        }
    }
}
