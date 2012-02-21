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
    public class BillingService : ServiceImplementation, IBillingService
    {

        #region IBillingService 成员
        [OperationBehavior(Impersonation = ImpersonationOption.Required)]      
        public Bill QueryBill(string tableNumber)
        {
            try
            {
                return BillingDao.GetBill(TableDao.GetDinningTable(tableNumber).Id);
            }
            catch (HCSMSException ex)
            {
                raiseError(ex);
                return null;
            }
        }
        [OperationBehavior(Impersonation = ImpersonationOption.Required)]      
        public void PayBill(Bill aBill)
        {
            try
            {
                //save record to database
                BillingDao.PayBill(aBill);               

            }
            catch (HCSMSException ex)
            {
                raiseError(ex);
                throw new FaultException<HCSMSException>(ex);
            }
            catch (Exception ex)
            {
                raiseError(ex);
            }
        }

        #endregion
    }
}
