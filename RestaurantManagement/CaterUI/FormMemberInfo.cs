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
    public partial class FormMemberInfo : Form
    {
        public FormMemberInfo()
        {
            InitializeComponent();
        }
        MemberInfoBll miBll = new MemberInfoBll();
        private void FormMemberInfo_Load(object sender, EventArgs e)
        {
            // 加载会员信息
            LoadList();
            // 加载会员分类信息
            LoadTypeList();
        }

        private void LoadList()
        {
            // 使用键值对存储条件
            Dictionary<string, string> dic = new Dictionary<string, string>();

            // 收集用户名信息
            if (txtNameSearch.Text != "")
            {
                // 需要根据名称搜索
                dic.Add("mname", txtNameSearch.Text);
                
            }

            // 收集电话信息
            if (txtPhoneSearch.Text != "")
            {
                dic.Add("MPhone", txtPhoneSearch.Text);
            }

            dgvList.AutoGenerateColumns = false;
            dgvList.DataSource = miBll.GetList(dic);
        }

        private void LoadTypeList()
        {
            MemberTypeInfoBll mtiBll = new MemberTypeInfoBll();
            List<MemberTypeInfo> list =  mtiBll.GetList();
            ddlType.DataSource = list;
            // 设置显示值
            ddlType.DataSource = list;
            ddlType.DisplayMember = "mtitle";
            ddlType.ValueMember = "mid";
        }

        private void txtNameSearch_TextChanged(object sender, EventArgs e)
        {
            // 内容改变事件
            LoadList();
        }

        private void txtPhoneSearch_Leave(object sender, EventArgs e)
        {
            // 失去焦点事件
            LoadList();
        }

        private void btnSearchAll_Click(object sender, EventArgs e)
        {
            txtNameSearch.Text = "";
            txtPhoneSearch.Text = "";
            LoadList();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // 值的有效性判断
            if (txtNameAdd.Text == "")
            {
                MessageBox.Show("Please enter the name of the member!");
                txtNameAdd.Focus();
                return;
            }

            // 接收用户输入的数据
            MemberInfo mi = new MemberInfo() {

                MName = txtNameAdd.Text,
                MPhone = txtPhoneAdd.Text,
                MMoney = Convert.ToDecimal(txtMoney.Text),
                MTypeId =Convert.ToInt32( ddlType.SelectedValue)
            };

            if (txtId.Text.Equals("No ID When Adding"))
            {
                // 添加
                if (miBll.Add(mi))
                {
                    LoadList();
                }
                else
                {
                    MessageBox.Show("Add failed, please try again later!");
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
                else
                {
                    MessageBox.Show("Updating failed, please try again later!");
                }

            }

            // 恢复控件的值
            txtId.Text = "No ID When Adding";
            txtNameAdd.Text = "";
            txtPhoneAdd.Text = "";
            txtMoney.Text = "";
            ddlType.SelectedIndex = 0;
            btnSave.Text = "Add";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // 恢复控件的值
            txtId.Text = "No ID When Adding";
            txtNameAdd.Text = "";
            txtPhoneAdd.Text = "";
            txtMoney.Text = "";
            ddlType.SelectedIndex = 0;
            btnSave.Text = "Add";
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // 获取点击的行
            var row = dgvList.Rows[e.RowIndex];
            // 将行中的数据显示到控件上
            txtId.Text = row.Cells[0].Value.ToString();
            txtNameAdd.Text = row.Cells[1].Value.ToString();
            txtPhoneAdd.Text = row.Cells[3].Value.ToString();
            ddlType.Text = row.Cells[2].Value.ToString();
            txtMoney.Text = row.Cells[4].Value.ToString();
            btnSave.Text = "Update";
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            // 获取选中项的编号
            int id = Convert.ToInt32(dgvList.SelectedRows[0].Cells[0].Value);
            // 先提示确认
            DialogResult result = MessageBox.Show("Are you sure you want to delete this row? ", "Delete Warning", MessageBoxButtons.OKCancel);
            if (result == DialogResult.Cancel) return;
            if (miBll.Remove(id))
            {
                LoadList();
            }
            else
            {
                MessageBox.Show("Failed to delete, please try again later!");
            }
        }
    }
}
