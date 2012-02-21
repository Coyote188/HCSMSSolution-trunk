using System;
using System.Collections.Generic;
using System.Text;

namespace HCSMS.Model
{
    [Serializable]
    public class Bill
    {
        public DateTime BillTime { get; set; }
        public string Note { get; set; }
        public string Id { get; set; }
        public decimal TotalMoney { get; set; }
        public string Type { get; set; }

        public DinningTable BillingTable { get;  set; }
        public List<BillingInfo> BillingInfo { get; set; }
        
    }
}
