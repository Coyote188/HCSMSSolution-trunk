using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using HCSMS.Model;
using HCSMS.Model.Application;

namespace HCSMS.Presentation.WindowsForms
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Session session= null;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (LoginForm login = new LoginForm())
            {
                Application.Run(login);
                if (login.Session == null)
                {
                    Environment.Exit(0);
                }
                else
                {
                    session = login.Session;
                }
            }

            Application.Run(new ChangeItemForm(session));
        }
    }
}
