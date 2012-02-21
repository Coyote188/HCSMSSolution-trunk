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


namespace HCSMS.Service.Impl
{
    public static class FrontDeskRequestControl 
    {
        private static Dictionary<FrontDeskRequest, string> aService = new Dictionary<FrontDeskRequest, string>();

        public static FrontDeskRequest GetService()
        {
            if (aService.Count == 0)
                return null;
            else
            {
                Random ran = new Random();
                return aService.ElementAt(ran.Next(aService.Count)).Key;
            }
        }
        public static FrontDeskRequest GetService(string sessionId)
        {
            if (aService.Count == 0)
                return null;
            else
            {
                return (from k in aService
                        where string.Compare(k.Value, sessionId, true) == 0
                        select k.Key).First<FrontDeskRequest>();
            }
        }
        public static void RegisterService(FrontDeskRequest service, string sessionId)
        {
            if (!aService.ContainsKey(service))
            {
                aService.Add(service,sessionId);
            }
            HCSMSLog.OnWarningLog(service, new NotifyEventArgs("Register Front Desk Request Service Amount " + aService.Count));
        }
        public static void UnregisterService(FrontDeskRequest service)
        {
            aService.Remove(service);
            HCSMSLog.OnWarningLog(service, new NotifyEventArgs("Unregister  Front Desk Request Service Amount " + aService.Count));
        }     
      
    }
}
