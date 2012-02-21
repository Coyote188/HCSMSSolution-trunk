using System;
using System.Collections.Generic;
using System.Text;

namespace HCSMS.Model
{
    [Serializable]
    public class MemberCard
    {      
        public decimal CurrentMoney { get; set; }
        public DateTime ExpiredDate { get; set; }
        public DateTime CreateDate { get; set; }
        public string Id { get; set; }
        public decimal MoneyConsumed { get; set; }
        public string Password { get; set; }
      

        public MemberCardType Type { get; set; }

    }
}
