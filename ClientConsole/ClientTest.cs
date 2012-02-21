using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Security.Principal;
using System.Net;
using System.ServiceModel;

using HCSMS.Model;
using HCSMS.Presentation.Impl;


namespace ClientConsole
{
    class ClientTest
    {
        static void Main(string[] args)
        {
            KitchenCallBackUI ui = new KitchenCallBackUI();
            ui.RequestForChangeItemEvent +=new UserInterface.HandleEventHandler(ui_RequestForChangeItemEvent);
            ui.RequestForOrderItemEvent += new UserInterface.HandleEventHandler(ui_RequestForOrderItemEvent);
            try
            {
                Console.WriteLine("Prepare for handle request: ");
                ui.Start();
                while (Console.ReadKey().Key != ConsoleKey.Enter)
                {

                }
                ui.Stop();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }           
        }

        static List<RequestHandleInfo> ui_RequestForOrderItemEvent(object sender, List<RequestHandleInfo> requestList)
        {
            foreach (var info in requestList)
            {
                OrderItem(info.EntityId, info.SourceId);
            }
            return requestList;
        }

        static List<RequestHandleInfo> ui_RequestForChangeItemEvent(object sender, List<RequestHandleInfo> requestList)
        {
            foreach(var info in requestList)
            {
                ChangeItem(info.EntityId, info.SourceId, info.TargetId);
            }
            return new List<RequestHandleInfo>();
        }

       
        public static string ChangeItem(string tableNumber, string oldItemId, string newItemId)
        {
            Console.WriteLine(DateTime.Now.ToString() + " Table Number: " + tableNumber + " " + " Change Item: " + oldItemId + "\t" + newItemId);
            return null;
        }

        public string ChangeTable(string tableNumber, string oldTableId, string newTableId)
        {
            Console.WriteLine(DateTime.Now.ToString() + " Table Number: " + tableNumber + " " + " Change Table: " + oldTableId + "\t" + newTableId);
            return null;
        }

        public static string OrderItem(string tableNumber, string itemId)
        {
            Console.WriteLine(DateTime.Now.ToString() + " Table Number: " + tableNumber + " " + " Order Item: " + itemId);
            return null;
        }

        public string DeorderItem(string tableNumber, string itemId)
        {
            Console.WriteLine(DateTime.Now.ToString() + " Table Number: " + tableNumber + " " + " Deorder Item: " + itemId);
            return null;
        }

        public void PrioritizeDish(string tableNumber, string itemId, PriorityLevel level)
        {
            throw new NotImplementedException();
        }

     

    }
}
