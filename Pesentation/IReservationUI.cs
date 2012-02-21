using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HCSMS.Model;

namespace HCSMS.Presentation
{
    public interface IReservationUI
    {
        List<Table> GetAvailableTable(DateTime date);
        List<PreorderTable> GetReservation();

        void MakeReservation(List<PreorderTable> tableList);
        void CancelReservation(List<PreorderTable> tableList);     
        void ChangeReservation(List<PreorderTable> tableList);
      
    }
}
