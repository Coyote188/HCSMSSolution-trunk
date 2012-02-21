using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HCSMS.Model
{
    [Serializable]
    public class SaleItem : Item
    {
        public decimal SalePricePerUnit { get; set; }
        public bool IsRecommended { get; set; }

        public List<SpecialOffer> SpecialOffer { get; set; }
    }
}
