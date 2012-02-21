using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HCSMS.Model;
using System.ServiceModel;


namespace HCSMS.Service
{
    [ServiceContract(CallbackContract = typeof(IKitchenCallBack))]
    public interface IKitchenRequest
    {
        [OperationContract]
        void ListenToServer();
    }
}
