using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HCSMS.Model;

namespace HCSMS.Presentation
{
    public interface IStockUI
    {
        List<StockItem> GetStockItem();
        void LayInStockItem(List<StockItem> anItemList);
        void RunOutOfSupply(List<Item> anItemList);
        void SetCriteria(QueryCriteria aCriteria);
        void UseStockItem(List<StockItem> anItemList);
    }
}
