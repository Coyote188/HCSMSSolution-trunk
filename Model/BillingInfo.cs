using System;
using System.Collections.Generic;
using System.Text;

namespace HCSMS.Model
{
    [Serializable]
    public class BillingInfo
    {
        public decimal Money { get; set; }
        

        public MemberCard Card {get;set;}

    }
}
