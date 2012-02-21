using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HCSMS.Model;

namespace HCSMS.Presentation
{
    public interface ITableUI
    {
        void ChangeItem(Dictionary<Item, Item> anItemPair);
        void ChangeTableTo(Table aNewTable);
        void MergeTableTo(Table aNewTable);
        void DeorderItem(List<Item> itemList );
        List<Table> GetAvailableTable();
        List<RequestHandleInfo> GetHandleRequest();
        List<Item> GetOrderedItem();
        Table GetTable();
        void HandleRequest(RequestHandleInfo aRequest);
        void OrderItem(List<Item> itemList);
        void ProritizeItem(Dictionary<Item, PriorityLevel> anItemPair);
        void ServeItem(List<Item> anItem);
        void SetCriteria(QueryCriteria aCriteria);
        void UserTable(Dictionary<Table, DinningTable> aTable);
    }
}
