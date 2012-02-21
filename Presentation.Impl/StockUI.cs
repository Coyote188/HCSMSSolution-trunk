using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HCSMS.Model;

namespace HCSMS.Presentation.Impl
{
    public class StockUI:UserInterface,IStockUI
    {
        public StockUI(Session session)
            : base(session)
        {
        }
        #region IStockUI 成员

        public List<StockItem> GetStockItem()
        {
            throw new NotImplementedException();
        }

        public void LayInStockItem(List<StockItem> anItemList)
        {
            throw new NotImplementedException();
        }

        public void RunOutOfSupply(List<Item> anItemList)
        {
            throw new NotImplementedException();
        }

        public void SetCriteria(QueryCriteria aCriteria)
        {
            throw new NotImplementedException();
        }

        public void UseStockItem(List<StockItem> anItemList)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
