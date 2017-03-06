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
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // 获取用户输入的信息
            string name = txtName.Text;
            string pwd = txtPwd.Text;
            // 调用bll
            int type;
            ManagerInfoBll miBll = new ManagerInfoBll();
            // 设置type传值
            LoginState loginState = miBll.Login(name, pwd, out type);
            switch (loginState)
            {
                case LoginState.Ok:
                    FormMain main = new FormMain();
                    main.Tag = type;//将员工级别传递过去
                    main.Show();
                    //FormManagerInfo minfo = new FormManagerInfo();
                    //minfo.Show();
                    this.Hide();
                    break;
                case LoginState.NameError:
                    MessageBox.Show("Wrong user name !");
                    break;
                case LoginState.PwdError:
                    MessageBox.Show("Your password doesn't match with user name!");
                    break;
            }
        }
    }
}
