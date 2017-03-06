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
    public partial class MemberTypeInfoDal
    {
        public List<MemberTypeInfo> GetList()
        {
            string sql = "select * from MemberTypeInfo where mIsDelete = 0";
            DataTable dt = SqliteHelper.GetDataTable(sql);
            List<MemberTypeInfo> list = new List<MemberTypeInfo>();
            foreach(DataRow row in dt.Rows)
            {
                list.Add(new MemberTypeInfo() {
                    MId = Convert.ToInt32(row["mid"]),
                    MTitle = row["mtitle"].ToString(),
                    MDiscount = Convert.ToDecimal(row["mdiscount"])
                });
            }
            return list;
        }

        //添加
        public int Insert(MemberTypeInfo mti)
        {
            string sql = "insert into MemberTypeInfo (mtitle, mdiscount, misdelete) values (@title, @discount, 0)";
            // 为sql构造参数
            SQLiteParameter[] ps =
            {
                new SQLiteParameter("@title",mti.MTitle),
                new SQLiteParameter("@discount", mti.MDiscount)
            };
            return SqliteHelper.ExecuteNonQuery(sql, ps);
        }

        // 修改
        public int Update(MemberTypeInfo mti)
        {
            // 构造update语句
            string sql = "update MemberTypeInfo set mtitle = @title, mdiscount=@discount where mid=@id";
            // 为Sql 构造参数
            SQLiteParameter[] ps = 
            {
                new SQLiteParameter("@title",mti.MTitle),
                new SQLiteParameter("@discount",mti.MDiscount),
                new SQLiteParameter("@id",mti.MId)
            };
            return SqliteHelper.ExecuteNonQuery(sql, ps);
        }

        public int Delete(int id)
        {
            // 进行逻辑删除的sql语句
            string sql = "update MemberTypeInfo set mIsDelete =1 where mid=@id";
            SQLiteParameter ps = new SQLiteParameter("@id",id);
            // 返回受影响的行数
            return SqliteHelper.ExecuteNonQuery(sql, ps);
        }
    }
}
