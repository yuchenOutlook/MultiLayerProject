using CaterBll;
using CaterModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaterUI
{
    public partial class FormManagerInfo : Form
    {
        public FormManagerInfo()
        {
            InitializeComponent();
        }

        // 创建业务层逻辑对象
        ManagerInfoBll miBll = new ManagerInfoBll();

        private void FormManagerInfo_Load(object sender, EventArgs e)
        {
            LoadList();
        }

        private void LoadList()
        {
            // 创建业务逻辑层对象
            ManagerInfoBll miBll = new ManagerInfoBll();

            // 禁用列表的自动生成
            dgvList.AutoGenerateColumns = false;

            // 把数据加入到DataGridView中
            dgvList.DataSource = miBll.GetList();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // 接受用户输入
            ManagerInfo mi = new ManagerInfo()
            {
                MName = txtName.Text.Trim(),
                MPwd = txtPwd.Text.Trim(),
                MType = Convert.ToInt32(rb1.Checked ? 1 : 0)  //经理值为1，店员值为0
            };

            if (txtId.Text.Equals("添加时无编号"))
            {
                // 添加
                if (miBll.Add(mi))
                {
                    // 如果加载成功，返回数据
                    LoadList();

                }
                else
                {
                    // 如果添加失败，告诉用户
                    MessageBox.Show("添加失败，请稍后重试！");
                }
            }
            else
            {
                // 修改
                mi.MId = int.Parse(txtId.Text);
                if (miBll.Edit(mi))
                {
                    LoadList();
                }
            }



            // 清除文本框中的值
            txtName.Text = "";
            txtPwd.Text = "";
            rb2.Checked = true;
            btnSave.Text = "添加";
            txtId.Text = "添加时无编号";
        }

        private void dgvList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //对类型列进行格式化处理
            if(e.ColumnIndex == 2)
            {
                // 根据类型判断内容
                e.Value = Convert.ToInt32(e.Value) == 1 ? "Manager" : "Employee";
            }
        }

        private void dgvList_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // 根据当前点击的单元格，找到行与列并进行赋值
            // 根据索引找到行
            DataGridViewRow row = dgvList.Rows[e.RowIndex];
            // 找到对应的列
            txtId.Text = row.Cells[0].Value.ToString();
            txtName.Text = row.Cells[1].Value.ToString();
            if (row.Cells[2].Value.ToString().Equals("1"))
            {
                rb1.Checked = true; // 值为1，则经理选中
            }
            else
            {
                rb2.Checked = true; // 值为0，则店员选中
            }

            // 指定密码的值
            txtPwd.Text = "这是原来的密码么";

            btnSave.Text = "Change Info";
        }
    }
}
