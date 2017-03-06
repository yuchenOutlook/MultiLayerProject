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
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 将当前应用程序退出，而不仅是关闭当前窗体
            Application.Exit();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            // 判断进入主界面的员工级别，以确定是否显示menuManager 图标
            int type = Convert.ToInt32(this.Tag);
            if (type == 1)
            {
                // 这是manager

            }
            else
            {
                // 普通员工，管理员菜单不需要显示
                menuManagerInfo.Visible = false;
            }
        }

        private void menuManagerInfo_Click(object sender, EventArgs e)
        {
            FormManagerInfo formManagerInfo = new FormManagerInfo();
            formManagerInfo.Show();
        }
    }
}
