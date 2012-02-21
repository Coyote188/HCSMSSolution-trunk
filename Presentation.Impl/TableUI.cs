using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HCSMS.Model;
using HCSMS.Model.Application;
using System.ServiceModel;

namespace HCSMS.Presentation.Impl
{
    public class TableUI : UserInterface, ITableUI
    {
        

        public Table Table { get; set; }  

        public TableUI(string tableNumber, Session session)
            :this(session)
        {
            
            this.Table.Number = tableNumber;
        }
        public TableUI(Table aTable, Session session)
            : this(session)
        {           
            this.Table = aTable;
        }
        private TableUI(Session session) :base(session)
        {
            Table = new Table();
            
        }

        #region ITableUI 成员
        public void ChangeItem(Dictionary<Item, Item> anItemPair)
        {
            try
            {
              
                using (DinningService.DinningServiceClient proxy = new DinningService.DinningServiceClient())
                {
                    proxy.ChangeItem(Table.Number, anItemPair);
                }
            }
            catch (FaultException<HCSMSException> ex)
            {
                raiseError(ex);
            }
            catch (Exception ex)
            {               
                raiseError(ex);
            }
            
        }

        public void ChangeTableTo(Table aNewTable)
        {  
            //try
            //{ 
            //    dinningProxy.ChangeTable(Table.Number, aNewTable.Number);
            //}  
            //catch (FaultException<HCSMSException> ex)
            //{
            //    raiseError(ex);
            //}
            //catch (Exception ex)
            //{
            //    raiseError(ex);
            //}
        }

        public void MergeTableTo(Table aNewTable)
        {
            //try
            //{
            //    dinningProxy.ChangeTable(Table.Number, aNewTable.Number);
            //}
            //catch (FaultException<HCSMSException> ex)
            //{
            //    raiseError(ex);
            //}
            //catch (Exception ex)
            //{
            //    raiseError(ex);
            //}
        }

        public void DeorderItem(List<Item> itemList)
        {
            //try
            //{
            //    dinningProxy.DeorderItem(Table.Number, itemList);
            //}
            //catch (FaultException<HCSMSException> ex)
            //{
            //    raiseError(ex);
            //}
            //catch (Exception ex)
            //{
            //    raiseError(ex);
            //}
        }

        public List<Table> GetAvailableTable()
        {
            //try
            //{               
            //    return dinningProxy.QueryAvailableTable();
            //}
            //catch (FaultException<HCSMSException> ex)
            //{
            //    raiseError(ex);
            //return new List<Table>();
            //}
            //catch (Exception ex)
            //{
            //    raiseError(ex);
            return new List<Table>();
            //}
        }

        public List<RequestHandleInfo> GetHandleRequest()
        {
            throw new NotImplementedException();
        }

        public List<Item> GetOrderedItem()
        {
            throw new NotImplementedException();
        }

        public Table GetTable()
        {
            throw new NotImplementedException();
        }

        public void HandleRequest(RequestHandleInfo aRequest)
        {
            throw new NotImplementedException();
        }

        public void OrderItem(List<Item> itemList)
        {
            bool success = false;
            DinningService.DinningServiceClient proxy = new HCSMS.Presentation.Impl.DinningService.DinningServiceClient();
            try
            {
                proxy.OrderItem(Table.Number, itemList);
                
                proxy.Close();
                success = true;
            }
            catch (FaultException<HCSMSException> ex)
            {
                raiseError(ex);
            }
            catch (Exception ex)
            {
                raiseError(ex);
            }
            finally
            {
                if (!success)
                {
                    proxy.Abort();
                }
            }
        }

        public void ProritizeItem(Dictionary<Item,PriorityLevel> anItemPair)
        {
            throw new NotImplementedException();
        }

        public void ServeItem(List<Item> anItem)
        {
            throw new NotImplementedException();
        }

        public void SetCriteria(QueryCriteria aCriteria)
        {
            throw new NotImplementedException();
        }

        public void UserTable(Dictionary<Table,DinningTable> aTable)
        {
            //try
            //{
            //    dinningProxy.UseTable(aTable);

            //}
            //catch (FaultException<HCSMSException> ex)
            //{
            //    raiseError(ex);

            //}
            //catch (Exception ex)
            //{
            //    raiseError(ex);

            //}
        }

        #endregion       
    
        
    }
}
