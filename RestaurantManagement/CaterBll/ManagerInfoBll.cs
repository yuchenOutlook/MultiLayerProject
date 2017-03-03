using CaterDal;
using CaterModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaterBll
{
    public partial class ManagerInfoBll
    {
        ManagerInfoDal miDal = new ManagerInfoDal();

        // 查询
        public List<ManagerInfo> GetList()
        {
            return miDal.GetList();
        }

        // 添加
        public bool Add(ManagerInfo mi)
        {
            return miDal.Insert(mi) > 0;
        }

        // 修改
        public bool Edit(ManagerInfo mi)
        {
            return miDal.Update(mi)>0;
        }
    }
}
