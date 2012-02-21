using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using HCSMS.Model;
using HCSMS.Model.Application;
using HCSMS.Presentation.Impl;

namespace HCSMS.Presentation.WindowsForms
{
    public partial class LoginForm : Form
    {
        private LoginUI aUI;
        private Account account;
        public Session Session { get { return aUI.Session; } }

        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            account = new Account();
            aUI = new LoginUI(account);
            
          
        } 

        private void OnError(object sender, ErrorEventArgs ev)
        {  
            MessageBox.Show("程序发生错误 : " +ev.Exception.Message, "提示");
        }
        private void ShowLoginSuccess()
        {

            string time = DateTime.Now.ToString();
            string msg = "User Name:" + aUI.Session.Account.UserName + "\n"
                                + "Login Id:" + aUI.Session.Id + "\n";
            string roles = null;
            foreach (string s in aUI.Session.Account.UserRole)
            {
                roles += s + "\t";
            }
            msg = msg + "User Roles: " + roles;

            MessageBox.Show("用户登陆成功 : " + msg + time, "提示");
        }
        private void ShowLoginFail()
        {           
            MessageBox.Show("用户名或密码错误！", "提示");
        }
        private void ShowLoginNotValid( )
        {
            MessageBox.Show("用户名或密码无效！", "提示");
        }

        private void LockControl()
        {
            btnCancel.Enabled = false;
            btnLogin.Enabled = false;
            txtUserName.Enabled = false;
            txtUserPwd.Enabled = false;
        }
        private void UnlockControl()
        {
            btnCancel.Enabled = true;
            btnLogin.Enabled = true;
            txtUserName.Enabled = true;
            txtUserPwd.Enabled = true;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            aUI.Session = null;
            Close();
        }        
        private void btnLogin_Click(object sender, EventArgs e)
        {
          
            account.Name = txtUserName.Text.Trim();
            account.Password = txtUserPwd.Text.Trim();
         
            if (aUI.IsValid())
            {
                LockControl();
                backgroundWorker.RunWorkerAsync();
            }
            else
            {
                ShowLoginNotValid();
            }
        }

        private void Login()
        {

            if (aUI.IsValid())
            {
                try
                {
                    aUI.Login();
                }
                catch (Exception ex)
                {
                    OnError(this, new ErrorEventArgs(null, ex));
                }
            }
        }
        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Login();
            e.Result = aUI.Session;
        }
        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {        
            if (e.Result == null)
            {

                UnlockControl();
                ShowLoginFail();
            }
            else
            {
                ShowLoginSuccess();
                Close();
            }
        }
    }
}
