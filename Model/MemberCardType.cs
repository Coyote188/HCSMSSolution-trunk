using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HCSMS.Model
{
    [Serializable]
    public class MemberCardType
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Discount { get; set; }
        public decimal MaximumConsume { get; set; }
        public decimal MinimumConsume { get; set; }

    }
}
