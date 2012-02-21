using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Security.Principal;
using System.Net;
using System.ServiceModel;

using HCSMS.Model;
using HCSMS.Presentation.Impl;

namespace TestServiceConsole
{
    class ServiceTest
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Dinnig Service Modeling Cosole");
            //InstanceContext context = new InstanceContext(new FrontDeskCallBackUI());
            //using ( DinningService.DinningServiceClient  proxy = new DinningService.DinningServiceClient (context))
            //{
            //    try
            //    {                
            //        Console.WriteLine("Dinnig Service state : " + proxy.State);
            //        proxy.Subscribe();
            //        Console.WriteLine("Dinnig Service Request Change Item :\n ");
            //        Dictionary<TestServiceConsole.DinningService.Item,TestServiceConsole.DinningService.Item> itemList = new Dictionary<TestServiceConsole.DinningService.Item,TestServiceConsole.DinningService.Item>();
            //        itemList.Add(new TestServiceConsole.DinningService.Item(){Idk__BackingField="001"}, new TestServiceConsole.DinningService.Item(){Idk__BackingField="002"});
            //        proxy.ChangeItem("001",itemList);
            //        Console.WriteLine("Finish change");
            //        proxy.Unsubscribe();
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(ex.Message);
            //        Console.ReadKey();
            //    }
            //}
            Console.ReadKey();
        }

    }
    

}
