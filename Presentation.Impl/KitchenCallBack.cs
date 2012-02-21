using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HCSMS.Model;
using HCSMS.Model.Application;
using System.ServiceModel;


namespace HCSMS.Presentation.Impl
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, UseSynchronizationContext = false)]
    class KitchenCallBack: UserInterface,KitchenRequest.IKitchenRequestCallback
    {
        

        public event HandleEventHandler ChangeItemEvent;
        public event HandleEventHandler ChangeTableEvent;
        public event HandleEventHandler OrderItemEvent;
        public event HandleEventHandler DeorderItemEvent;
        public event DishPrioritizeHandler PrioritizeDishEvent;

        public KitchenCallBack()
        {   }   

        #region IKitchenServiceCallback 成员

        public List<RequestHandleInfo> ChangeItem(List<RequestHandleInfo> itemList)
        {  
            return  ProcessHandleEvent(ChangeItemEvent, new HandleEventArgs("Change Item",itemList));
        }

        public List<RequestHandleInfo> ChangeTable(List<RequestHandleInfo> itemList)
        {
            return ProcessHandleEvent(ChangeTableEvent, new HandleEventArgs("Change Table", itemList));
        }

        public List<RequestHandleInfo> OrderItem(List<RequestHandleInfo> itemList)
        {
            return ProcessHandleEvent(OrderItemEvent, new HandleEventArgs("Order Item", itemList));
        }

        public List<RequestHandleInfo> DeorderItem(List<RequestHandleInfo> itemList)
        {
            return ProcessHandleEvent(DeorderItemEvent, new HandleEventArgs("Deorder Item", itemList));
        }   


        public void PrioritizeDish(string tableNumber, string itemId, PriorityLevel level)
        {
            if (PrioritizeDishEvent != null)
            {
                PrioritizeDishEvent(tableNumber, itemId, level);
            }
        }

        #endregion

       
    }
}
