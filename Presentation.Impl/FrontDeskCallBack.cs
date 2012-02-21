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
    public class FrontDeskCallBack : UserInterface, FrontDeskRequest.IFrontDeskRequestCallback
    {
       

        public event NotifyEventHandler StopSupplyItemEvent;
        public event NotifyEventHandler RunOutOfItemEvent;
        public event NotifyListEventHandler ServeItemEvent;
        public event NotifyListEventHandler RequestItemEvent;
        public event ErrorEventHandler ErrorEvent;
        public event NotifyEventHandler ServerReplyEvent;

        public FrontDeskCallBack()
        {
        }
        #region IFrontDeskRequestCallback 成员

        public void ServeItem(List<RequestHandleInfo> itemList)
        {
            if (ServeItemEvent != null)
            {
                ServeItemEvent(itemList);
            }
        }

        public void RequestDeny(List<RequestHandleInfo> itemList)
        {
            if (ServeItemEvent != null)
            {
                ServeItemEvent(itemList);
            }
        }

        public void StopSupplyItem(string itemId)
        {
            ProcessNotifyEvent(StopSupplyItemEvent, new NotifyEventArgs(itemId));        
        }

        public void RunOutOfItem(string itemId)
        {
            ProcessNotifyEvent(RunOutOfItemEvent, new NotifyEventArgs(itemId));
        }    

        public void ErrorMessage(ErrorEventArgs error)
        {
            ProcessErrorEvent(ErrorEvent, error);
        }

        public void InformationMessage(NotifyEventArgs msg)
        {
            ProcessNotifyEvent(ServerReplyEvent, msg);
        }

        #endregion
    }
}
