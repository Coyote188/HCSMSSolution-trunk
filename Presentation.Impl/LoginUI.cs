using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HCSMS.Model;
using HCSMS.Model.Application;
using System.ServiceModel;

namespace HCSMS.Presentation.Impl
{
    public class LoginUI: UserInterface,ILoginUI
    {      
    
        private Account account = new Account();          

      
        public Account Account { get { return account; } set { account = value; } }
        public event NotifyEventHandler ValidateFail;

        public LoginUI(Account acc)
        {
            this.Account = acc;
        }

        public void Login(string userName, string password)
        {
            account.Name = userName;
            account.Password = password;

            Login();

        }
        public void Login()
        {
            if (validate())
            {
                AccountService.AccountServiceClient proxy = new AccountService.AccountServiceClient();

                try
                {
                    Session = proxy.StartSession(account);

                }
                catch (FaultException<HCSMSException> ex)
                {
                    raiseError(ex);
                }
                catch (Exception ex)
                {
                    raiseError(ex);
                    throw ex;
                }

            }
            else
            {
                ProcessNotifyEvent(ValidateFail, new NotifyEventArgs("用户名或密码无效！"));
            }
        }

        public bool IsValid()
        {
            if (!validate())
            {               
                return false;
            }
            else
                return true;
        }
        private bool validate()
        {
            if (account.Name == string.Empty || account.Password == string.Empty)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool IsLogin()
        {
            using ( AccountService.AccountServiceClient proxy = new AccountService.AccountServiceClient())
            {
                try
                {
                    return proxy.IsLogin(Session);                    
                }
                catch (FaultException<HCSMSException> ex)
                {
                    raiseError(ex);
                    return false;
                }
                catch (Exception ex)
                {
                    raiseError(ex);
                    throw ex;
                }              
            }
        }
        
    }
}
