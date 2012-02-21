using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HCSMS.Model;

namespace HCSMS.Presentation
{
    public interface IBillingUI
    {
        Bill GetBill(string tableNumber);
        void PayBill( Bill aBill, BillingInfo billingInfo);
        void PrintReciept();
    }
}
