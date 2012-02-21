using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using HCSMS.Model;
using HCSMS.DataAccess;
using HCSMS.Model.Application;
using HCSMS.Service;
using System.Security.Principal;
using System.Security.Permissions;


namespace HCSMS.Service.Impl
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class AccountService : ServiceImplementation, IAccountService
    {    

        public AccountService()
        {                       
        }     
    
        public Session StartSession(Account anAccount)
        {
            if (anAccount == null)
            {
                throw new ArgumentNullException("Account can not be null!");
            }
            else
            {
              return   AccountServiceCore.NewSession(anAccount);
            }
        }

        public void StopSession(Session session)
        {
            if (session == null)
            {
                throw new ArgumentNullException("Session can not be null!");
            }
            else
            {
                AccountServiceCore.Logout(session.Id.ToString());
            }
        }
        public bool IsLogin(Session session)
        {           
            return AccountServiceCore.IsLogin(session.Id.ToString());
        }
      
    }
}
