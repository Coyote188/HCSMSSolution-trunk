using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using HCSMS.Model;
using HCSMS.Model.Application;

namespace HCSMS.Service.Impl
{
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerSession)]
    public class KitchenRequest : IKitchenRequest
    {
        IKitchenCallBack callBack;

        public KitchenRequest()
        {           
        }
        ~KitchenRequest()
        {
            KitchenRequestControl.UnregisterService(this);
        }
        public List<RequestHandleInfo> OnChangeItem(List<RequestHandleInfo> itemList)
        {            
            return callBack.ChangeItem(itemList);
        }

        public List<RequestHandleInfo> OnChangeTable(List<RequestHandleInfo> itemList)
        {
            return callBack.ChangeTable(itemList);
        }

        public List<RequestHandleInfo> OnDeorderItem(List<RequestHandleInfo> itemList)
        {
            return callBack.DeorderItem(itemList);
        }

        public List<RequestHandleInfo> OnOrderItem(List<RequestHandleInfo> itemList)
        {
            return callBack.OrderItem(itemList);
        }
        #region IKitchenRequest 成员

        public void ListenToServer()
        {
            HCSMSLog.OnWarningLog(this, new NotifyEventArgs("kitchen request subscribe service"));
            callBack = OperationContext.Current.GetCallbackChannel<IKitchenCallBack>();

            OperationContext.Current.Channel.Closing += new EventHandler(Channel_Closing);
            OperationContext.Current.Channel.Faulted += new EventHandler(Channel_Faulted);
           
            KitchenRequestControl.RegisterService(this);
        }
        #endregion
        void Channel_Faulted(object sender, EventArgs e)
        {
            Unsubscribe();
        }

        void Channel_Closing(object sender, EventArgs e)
        {
            Unsubscribe();
        }

        public void Unsubscribe()
        {         
            KitchenRequestControl.UnregisterService(this);
        }

    
    }
}
