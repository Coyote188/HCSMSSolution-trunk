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
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class FrontDeskRequest : IFrontDeskRequest
    {
        IFrontDeskCallBack callBack;
        public FrontDeskRequest()
        {
        }
        ~FrontDeskRequest()
        {
            FrontDeskRequestControl.UnregisterService(this);
        }
        #region IFrontDeskRequest 成员
        public void ListenToServer()
        {
            HCSMSLog.OnWarningLog(this, new NotifyEventArgs("Frontdesk request subscribe service"));
            callBack = OperationContext.Current.GetCallbackChannel<IFrontDeskCallBack>();

            OperationContext.Current.Channel.Faulted += new EventHandler(Channel_Faulted);
            OperationContext.Current.Channel.Closing += new EventHandler(Channel_Closing);

            FrontDeskRequestControl.RegisterService(this, OperationContext.Current.SessionId);
        }
        #endregion
        void Channel_Closing(object sender, EventArgs e)
        {
            Unsubscribe();
        }
        void Channel_Faulted(object sender, EventArgs e)
        {
            Unsubscribe();
        }
        public void Unsubscribe()
        {
            FrontDeskRequestControl.UnregisterService(this);
        }

        public void ServeItem(List<RequestHandleInfo> itemList)
        {
            callBack.ServeItem(itemList);
        }
        public void RequestDeny(List<RequestHandleInfo> itemList)
        {
            callBack.RequestDeny(itemList);
        }
        public void StopSupplyItem(string itemId)
        {
            callBack.StopSupplyItem(itemId);
        }
        public void RunOutOfItem(string itemId)
        {
            callBack.RunOutOfItem(itemId);
        }
        public void ErrorMessage(ErrorEventArgs args)
        {
            callBack.ErrorMessage(args);
        }
        public void InformationMessage(NotifyEventArgs args)
        {
            callBack.InformationMessage(args);
        }

       
    }
}
