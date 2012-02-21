using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HCSMS.Model;
using HCSMS.Model.Application;
using System.ServiceModel;

namespace HCSMS.Service
{
    [ServiceContract]
    public interface IFrontDeskCallBack
    {
        [OperationContract]
        void ServeItem(List<RequestHandleInfo> itemList);
        [OperationContract]
        void RequestDeny(List<RequestHandleInfo> itemList);
        [OperationContract]
        void StopSupplyItem(string itemId);
        [OperationContract]
        void RunOutOfItem(string itemId);
        [OperationContract]
        void ErrorMessage(ErrorEventArgs error);
        [OperationContract]
        void InformationMessage(NotifyEventArgs msg);
    }
}
