using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;


using System.Security;
using System.Security.Principal;
using System.Security.Permissions;

using HCSMS.Model;
using HCSMS.Model.Application;
using HCSMS.Service;
using HCSMS.DataAccess;

using System.Web.Security;


namespace HCSMS.Service.Impl
{
   

    public static class AccountServiceCore
    {
        private static Dictionary<Session,DateTime> sessionList;

        static AccountServiceCore()
        {
            sessionList = new Dictionary<Session, DateTime>();
        }

        public static Session NewSession(Account anAccount)
        {
            if (SystemAccountDao.IsExist(anAccount.Name))
            {
                SystemAccount sysAccount = SystemAccountDao.Login(anAccount);
                Session session = new Session(sysAccount);

                sessionList.Add(session, DateTime.Now);
                return session;
            }
            else
            {
                return null;
            }
           
        }
        public static bool IsLogin(string sessionToken)
        {
            bool isLogin = false;

            foreach (KeyValuePair<Session, DateTime> users in sessionList)
            {
                if (users.Key.Id.ToString() == sessionToken)
                {
                    if (users.Value.AddMinutes(users.Key.TimeOut) >= DateTime.Now)
                    {
                        sessionList[users.Key] = DateTime.Now;
                        isLogin = true;
                    }
                    else
                    {
                        sessionList.Remove(users.Key);
                        throw new HCSMSException("用户会话已过时");
                    }
                   
                    break;
                }
            }
            return isLogin;
        }
        public static void Logout(string sessionToken)
        {           
            foreach (KeyValuePair<Session, DateTime> user in sessionList)
            {
                if (user.Key.Id.ToString() == sessionToken)
                    sessionList.Remove(user.Key);
            }
        }

    }
}