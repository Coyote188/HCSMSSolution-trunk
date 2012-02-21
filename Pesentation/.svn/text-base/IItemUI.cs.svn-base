using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HCSMS.Model;

namespace HCSMS.Presentation
{
    public interface IItemUI
    {
        void CleanItem(List<Item> anItemList);
        List<SaleItem> GetItem();
        List<SaleItem> GetRecommendItem();
        List<SpecialOffer> GetSpecialOfferItem(Item anItem);
        List<Item> GetUnavailableItem();
        void MakeSpecialOffer(Dictionary<Item, List<SpecialOffer>> anOffer);
        void RunOutOfItem(List<Item> anItemList);
        void SetCriteria(QueryCriteria aCriteria);
        void SetItem(List<SaleItem> anItem);
        void StopSupplyItem(List<Item> anItemList);
        void UpdateItem(List<Item> anItem);
    }
}
