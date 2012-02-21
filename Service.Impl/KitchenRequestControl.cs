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
    public static class KitchenRequestControl
    {
        private static List<KitchenRequest> aService;

        static KitchenRequestControl()
        {
            aService = new List<KitchenRequest>();
        }

        public static KitchenRequest GetService()
        {
            Random ran = new Random();
            if (aService.Count == 0)
            {
                return null;
            }
            else
            {
                return aService.ElementAt(ran.Next(aService.Count));
            }
        }
        public static void RegisterService(KitchenRequest service)
        {
            if (!aService.Contains(service))
            {
                aService.Add(service);
            }
            HCSMSLog.OnWarningLog(service, new NotifyEventArgs("Register Kitchen Request Service Amount " + aService.Count));
        }

        public static void UnregisterService(KitchenRequest service)
        {
            aService.Remove(service);
            HCSMSLog.OnWarningLog(service, new NotifyEventArgs("Unregister Kitchen Request Service Amount " + aService.Count));
        }
     
    }
}
