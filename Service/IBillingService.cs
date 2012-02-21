using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HCSMS.Model;
using System.ServiceModel;

namespace HCSMS.Service
{
    [ServiceContract]
    public interface IBillingService
    {
        [OperationContract]      
        Bill QueryBill(string tableNumber);

        [OperationContract]        
        void PayBill(Bill aBill);
        

    }
}
