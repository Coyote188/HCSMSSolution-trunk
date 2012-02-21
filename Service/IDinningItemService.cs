using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HCSMS.Model;
using System.ServiceModel;

namespace HCSMS.Service
{
    [ServiceContract]
    public interface IDinningItemService
    {
        [OperationContract]
        List<SaleItem> QueryItemList(QueryCriteria queryConditon);
        [OperationContract]
        Item QueryItem(string itemId);
        [OperationContract]
        List<SpecialOffer> GetSpecialOfferForItem(Item anItem);
        [OperationContract]
        List<SaleItem> GetRecommendItem();


        [OperationContract]
        void OrderItem(string tableNumber, List<Item> itemList);
        [OperationContract]
        void DeorderItem(string tableNumber, List<Item> itemList);
        [OperationContract]
        void ChangeItem(string tableNumber, Dictionary<Item, Item> itemPair);
        //[OperationContract]
        //void ServeItem(string tableNumber, List<Item> itemList);
        [OperationContract]
        void SetSpecialOfferForItem(Item anItem, List<SpecialOffer> offerList);
        [OperationContract]
        void SetItem(List<SaleItem> itemList);
        [OperationContract]
        void UpdateItem(List<Item> itemList);
     

    }
}
