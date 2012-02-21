using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HCSMS.Model;

using System.ServiceModel;

namespace HCSMS.Service
{
    [ServiceContract]
    public interface IKitchenCallBack
    {
        [OperationContract]
        List<RequestHandleInfo> ChangeItem(List<RequestHandleInfo> itemList);
        [OperationContract]
        List<RequestHandleInfo> ChangeTable(List<RequestHandleInfo> itemList);
        [OperationContract]
        List<RequestHandleInfo> OrderItem(List<RequestHandleInfo> itemList);
        [OperationContract]
        List<RequestHandleInfo> DeorderItem(List<RequestHandleInfo> itemList);
        [OperationContract]
        void PrioritizeDish(string tableNumber, string itemId, PriorityLevel level);
    }
}
