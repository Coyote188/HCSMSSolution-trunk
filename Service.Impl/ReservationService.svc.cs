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
    public class ReservationService : ServiceImplementation, IReservationService
    {
        public ReservationService()
            : base()
        {
            
        }
        

        #region IReservationService 成员
        [OperationBehavior(Impersonation = ImpersonationOption.Required)]      
        public void MakeReservation(PreorderTable table)
        {
            try
            {
                //save record to database
                ReservationDao.InsertCustomerReservation(table);
                    

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
        [OperationBehavior(Impersonation = ImpersonationOption.Required)]      
        public void CancelReservation(PreorderTable table)
        {
            try
            {
                //save record to database
                ReservationDao.DeleteReservationTable(table.Id);


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
        [OperationBehavior(Impersonation = ImpersonationOption.Required)]      
        public List<PreorderTable> GetReservationList()
        {
            try
            {
                return ReservationDao.GetReservation();
            }
            catch (HCSMSException ex)
            {
                raiseError(ex);
                return null;
            }
        }
        [OperationBehavior(Impersonation = ImpersonationOption.Required)]      
        public void ChangeReservation(PreorderTable table)
        {
            try
            {
                //save record to database
                ReservationDao.UpdateCustomerReservaition(table);


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
