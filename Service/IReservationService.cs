using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HCSMS.Model;
using System.ServiceModel;

namespace HCSMS.Service
{
    [ServiceContract]
    public interface IReservationService
    {
        [OperationContract]
        void MakeReservation(PreorderTable table);
        [OperationContract]
        void CancelReservation(PreorderTable table);
        [OperationContract]
        List<PreorderTable> GetReservationList();
        [OperationContract]
        void ChangeReservation(PreorderTable table);
        
    }
}
