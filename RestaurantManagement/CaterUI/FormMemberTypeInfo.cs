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
    public partial class FormMemberTypeInfo : Form
    {
        public FormMemberTypeInfo()
        {
            InitializeComponent();
        }

        MemberTypeInfoBll mtiBll = new MemberTypeInfoBll();
        private DialogResult result = DialogResult.Cancel;

        private void FormMemberTypeInfo_Load(object sender, EventArgs e)
        {
            LoadList();
        }

        private void LoadList()
        {
          
            dgvList.AutoGenerateColumns = false; // 防止自动生成列
            dgvList.DataSource = mtiBll.GetList();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Add 操作
            // 接收用户输入值，构造对象
            MemberTypeInfo mti = new MemberTypeInfo()
            {
                MTitle = txtTitle.Text,
                MDiscount = Convert.ToDecimal(txtDiscount.Text)
            };

            if (txtId.Text.Trim().Equals("No ID Number when adding"))
            {
  
                if (mtiBll.Add(mti))
                {
                    LoadList();
                }
                else
                {
                    MessageBox.Show("Adding Failed, Please try it again!");
                }
            }
            else
            {
                // 修改
                mti.MId = int.Parse(txtId.Text);
                // 调用修改的方法
                if (mtiBll.Edit(mti))
                {
                    LoadList();
                }
                else
                {
                    MessageBox.Show("Editing failed, please try again!");
                }
                
            }
            // 将控件值还原
            txtId.Text = "No ID Number when adding";
            txtTitle.Text = "";
            txtDiscount.Text = "";
            btnSave.Text = "Add";
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // 将控件值还原
            txtId.Text = "No ID Number when adding";
            txtTitle.Text = "";
            txtDiscount.Text = "";
            btnSave.Text = "Add";
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // 取到双击行
            var row = dgvList.Rows[e.RowIndex];
            // 将行中列的值赋给文本框
            txtId.Text = row.Cells[0].Value.ToString();
            txtTitle.Text = row.Cells[1].Value.ToString();
            txtDiscount.Text = row.Cells[2].Value.ToString();
            btnSave.Text = "Update";
            result = DialogResult.OK;
            
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            // 获取选中行的编号
            var row = dgvList.SelectedRows[0];
            int id = Convert.ToInt32(row.Cells[0].Value);
            DialogResult result = MessageBox.Show("Are you sure you want to delete this row?", "Warning", MessageBoxButtons.OKCancel);
            if (result == DialogResult.Cancel)
            {
                return;
            }
            // 进行删除
            if (mtiBll.Remove(id))
            {
                LoadList();
            }
            else
            {
                MessageBox.Show("Deleting Failed, please try again!");
            }
            result = DialogResult.OK;
        }

        private void FormMemberTypeInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = result;
        }
    }
}
