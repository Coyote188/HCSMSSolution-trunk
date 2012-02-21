using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using HCSMS.Model;
using HCSMS.DataAccess;
using HCSMS.Model.Application;
using HCSMS.Service;

namespace HCSMS.Service.Impl
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class DinningService : ServiceImplementation, IDinningService
    {
        private string callBackId=string.Empty;

        public DinningService()
        {
        }

        #region Item Related Operations

        public List<SaleItem> QueryItemList(QueryCriteria queryConditon)
        {
            FrontDeskRequest frontdesk = FrontDeskRequestControl.GetService(callBackId); 
            try
            {
                HCSMSLog.OnWarningLog(this, new NotifyEventArgs("Query ItemList Request"));
                return ItemDao.GetSaleItemByCondition(queryConditon);             
                   
            }
            catch (HCSMSException ex)
            {
                raiseError(ex);
                if (frontdesk != null)
                {
                   frontdesk.ErrorMessage(new ErrorEventArgs("", ex));
                }
                return null;
            }
            catch (Exception ex)
            {
                raiseError(ex);
                return null;
            }

           
        }

        public Item QueryItem(string itemId)
        {
            throw new NotImplementedException();
        }

        public List<SpecialOffer> GetSpecialOfferForItem(Item anItem)
        {
            throw new NotImplementedException();
        }

        public void OrderItem(string tableNumber, List<Item> itemList)
        {
            FrontDeskRequest frontdesk=null;
            try
            {
                HCSMSLog.OnWarningLog(this, new NotifyEventArgs("Recieve Order Request"));
                
                //check if this table is really in use
                DinningTable table = TableDao.GetDinningTable(tableNumber);
                if (table == null)
                {
                    throw  new HCSMSException("餐桌是空的 ！");                    
                }
               
                // constructing order list
                List<RequestHandleInfo> requestList = new List<RequestHandleInfo>();
                foreach (Item ite in itemList)
                {
                    RequestHandleInfo info = new RequestHandleInfo();
                    info.EntityId = tableNumber;
                    info.IsHandled = false;
                    info.RequestType = RequestType.OrderItem;
                    info.SourceId = ite.Id;

                    requestList.Add(info);
                }

                //check for response of cook at the kitchen
                KitchenRequest handler = KitchenRequestControl.GetService();
                if (handler == null)
                {
                    requestList.Clear();
                }
                else
                {
                    requestList = handler.OnOrderItem(requestList);
                }
                HCSMSLog.OnWarningLog(this, new NotifyEventArgs("Recieve Result order item"));

                List<RequestHandleInfo> denyList = new List<RequestHandleInfo>();
                foreach (RequestHandleInfo ite in requestList)
                {
                    if (!ite.IsHandled)
                        denyList.Add(ite);
                }
                if (denyList.Count == 0 && requestList.Count > 0)
                {
                    //save data to database
                    ItemDao.InsertItemOrder(table.Id, itemList);
                }
                else
                {
                    frontdesk = FrontDeskRequestControl.GetService(callBackId);
                    if (frontdesk != null)
                    {
                        //means kitchen service is not up
                        if (requestList.Count == 0)
                        {
                            frontdesk.InformationMessage(new NotifyEventArgs("不存在厨房处理服务！"));
                        }
                        // notify front desk, the request is not satisfied
                        else
                        {
                            frontdesk.RequestDeny(denyList);
                        }
                    }
                }
            }
            catch (HCSMSException ex)
            {
                raiseError(ex);
                if (frontdesk != null)
                {
                    frontdesk.ErrorMessage(new ErrorEventArgs("Order Item Not Success", ex));
                }
            }
            catch (Exception ex)
            {
                raiseError(ex);
            }

        }

        public void DeorderItem(string tableNumber, List<Item> itemList)
        {
            FrontDeskRequest frontdesk = null;
            try
            {
                HCSMSLog.OnWarningLog(this, new NotifyEventArgs("Recieve Deorder Request"));

                //check if this table is really in use
                DinningTable table = TableDao.GetDinningTable(tableNumber);
                if (table == null)
                {
                    throw new HCSMSException("餐桌是空的 ！");
                }

                // constructing request list
                List<RequestHandleInfo> requestList = new List<RequestHandleInfo>();
                foreach (Item ite in itemList)
                {
                    RequestHandleInfo info = new RequestHandleInfo();
                    info.EntityId = tableNumber;
                    info.IsHandled = false;
                    info.RequestType = RequestType.DeorderItem;
                    info.SourceId = ite.Id;

                    requestList.Add(info);
                }

                //check for response of cook at the kitchen
                KitchenRequest handler = KitchenRequestControl.GetService();
                if (handler == null)
                {
                    requestList.Clear();
                }
                else
                {
                    requestList = handler.OnDeorderItem(requestList);
                }
                HCSMSLog.OnWarningLog(this, new NotifyEventArgs("Recieve Result order item"));

                List<RequestHandleInfo> denyList = new List<RequestHandleInfo>();
                foreach (RequestHandleInfo ite in requestList)
                {
                    if (!ite.IsHandled)
                        denyList.Add(ite);
                }
                if (denyList.Count == 0 && requestList.Count > 0)
                {
                    //build data for calling data access servcie
                    List<string> itemIdList = new List<string>();
                    foreach (var ite in itemList)
                    {
                        itemIdList.Add(ite.Id);
                    }
                    //save data to database
                    ItemDao.DeorderItem(table.Id, itemIdList);
                }
                else
                {
                    frontdesk = FrontDeskRequestControl.GetService(callBackId);
                    if (frontdesk != null)
                    {
                        //means kitchen service is not up
                        if (requestList.Count == 0)
                        {
                            frontdesk.InformationMessage( new NotifyEventArgs("不存在厨房处理服务！"));
                        }
                        // notify front desk, the request is not satisfied
                        else
                        {
                            frontdesk.RequestDeny(denyList);
                        }
                    }
                }
            }
            catch (HCSMSException ex)
            {
                raiseError(ex);
                if (frontdesk != null)
                {
                    frontdesk.ErrorMessage(new ErrorEventArgs("", ex));
                }
            }
            catch (Exception ex)
            {
                raiseError(ex);
            }           
         
        }

        public void ChangeItem(string tableNumber, Dictionary<Item, Item> itemPair)
        {
            FrontDeskRequest frontdesk = null;
            try
            {
                HCSMSLog.OnWarningLog(this, new NotifyEventArgs("Recieve change order Request"));

                //check if this table is really in use
                DinningTable table = TableDao.GetDinningTable(tableNumber);
                if (table == null)
                {
                    throw new HCSMSException("餐桌是空的 ！");
                }

                // constructing request list
                List<RequestHandleInfo> requestList = new List<RequestHandleInfo>();
                foreach (KeyValuePair<Item, Item> ite in itemPair)
                {
                    RequestHandleInfo info = new RequestHandleInfo();
                    info.EntityId = tableNumber;
                    info.IsHandled = false;
                    info.RequestType = RequestType.ChangeItem;
                    info.SourceId = ite.Key.Id;
                    info.TargetId = ite.Value.Id;

                    requestList.Add(info);
                }

                //check for response of cook at the kitchen
                KitchenRequest handler = KitchenRequestControl.GetService();
                if (handler == null)
                {
                    requestList.Clear();
                }
                else
                {
                    requestList = handler.OnChangeItem(requestList);
                }
                HCSMSLog.OnWarningLog(this, new NotifyEventArgs("Recieve Result order item"));

                List<RequestHandleInfo> denyList = new List<RequestHandleInfo>();
                foreach (RequestHandleInfo ite in requestList)
                {
                    if (!ite.IsHandled)
                        denyList.Add(ite);
                }
                if (denyList.Count == 0 && requestList.Count > 0)
                {
                    //build data for calling data access servcie
                    Dictionary<string, string> itemIdList = new Dictionary<string, string>();
                    foreach (var ite in itemPair)
                    {
                        itemIdList.Add(ite.Key.Id, ite.Value.Id);
                    }
                    //save data to database
                    ItemDao.ChangeItem(table.Id, itemIdList);
                }
                else
                {
                    frontdesk = FrontDeskRequestControl.GetService(callBackId);
                    if (frontdesk != null)
                    {
                        //means kitchen service is not up
                        if (requestList.Count == 0)
                        {
                            frontdesk.InformationMessage( new NotifyEventArgs("不存在厨房处理服务！"));
                        }
                        // notify front desk, the request is not satisfied
                        else
                        {
                            frontdesk.RequestDeny(denyList);
                        }
                    }
                }
            }
            catch (HCSMSException ex)
            {
                raiseError(ex);
                if (frontdesk != null)
                {
                   frontdesk.ErrorMessage(new ErrorEventArgs("", ex));
                }
            }
            catch (Exception ex)
            {
                raiseError(ex);
            }           
        }

        public void SetSpecialOfferForItem(Item anItem, List<SpecialOffer> offerList)
        {
            FrontDeskRequest frontdesk = FrontDeskRequestControl.GetService(callBackId);
            try
            {
                //save record to database
                SpecialOfferDao.InsertSpecialSale(anItem.Id, offerList);
            }
            catch (HCSMSException ex)
            {
                raiseError(ex);
                if (frontdesk != null)
                {
                   frontdesk.ErrorMessage(new ErrorEventArgs("", ex));
                }
            }
            catch (Exception ex)
            {
                raiseError(ex);
            }                 
        }

        public void SetItem(List<SaleItem> itemList)
        {
            FrontDeskRequest frontdesk = FrontDeskRequestControl.GetService(callBackId);
            try
            {
                //save record to database
                ItemDao.InsertSaleItem(itemList);
            }
            catch (HCSMSException ex)
            {
                raiseError(ex);
                if (frontdesk != null)
                {
                   frontdesk.ErrorMessage(new ErrorEventArgs("", ex));
                }
            }
            catch (Exception ex)
            {
                raiseError(ex);
            }              
        }

        public void UpdateItem(List<Item> itemList)
        {
            FrontDeskRequest frontdesk = FrontDeskRequestControl.GetService(callBackId);
            try
            {
                //save record to database
                ItemDao.UpdateItem(itemList);
            }
            catch (HCSMSException ex)
            {
                raiseError(ex);
                if (frontdesk != null)
                {
                   frontdesk.ErrorMessage(new ErrorEventArgs("", ex));
                }
            }
            catch (Exception ex)
            {
                raiseError(ex);
            }                   
        }
        public List<SaleItem> GetRecommendItem()
        {
            FrontDeskRequest frontdesk = FrontDeskRequestControl.GetService(callBackId);
            try
            {
                return ItemDao.GetRecommendSaleItem();
            }
            catch (HCSMSException ex)
            {
                raiseError(ex);
                if (frontdesk != null)
                {
                   frontdesk.ErrorMessage(new ErrorEventArgs("", ex));
                }
                return null;
            }
            catch (Exception ex)
            {
                raiseError(ex);
                return null;
            }  
        }

        #endregion

        #region Table Related Operations

        public Table QueryTable(string tableNumber)
        {
            FrontDeskRequest frontdesk = FrontDeskRequestControl.GetService(callBackId);
            try
            {
                Table aTable = null;

                aTable = TableDao.GetTable(tableNumber);
                return aTable;
            }
            catch (HCSMSException ex)
            {
                raiseError(ex);
                if (frontdesk != null)
                {
                   frontdesk.ErrorMessage(new ErrorEventArgs("", ex));
                }
                return null;
            }
            catch (Exception ex)
            {
                raiseError(ex);
                return null;
            }                        

        }

        public List<Table> QueryTableList(QueryCriteria condition)
        {
            FrontDeskRequest frontdesk = FrontDeskRequestControl.GetService(callBackId);
            try
            {
                return TableDao.GetTable(condition);
            }
            catch (HCSMSException ex)
            {
                raiseError(ex);
                if (frontdesk != null)
                {
                   frontdesk.ErrorMessage(new ErrorEventArgs("", ex));
                }
                return null;
            }
            catch (Exception ex)
            {
                raiseError(ex);
                return null;
            }                
        }

        public List<Table> QueryAvailableTable()
        {
            FrontDeskRequest frontdesk = FrontDeskRequestControl.GetService(callBackId);
            try
            {
                return TableDao.GetAvailableTable();
            }
            catch (HCSMSException ex)
            {
                raiseError(ex);
                if (frontdesk != null)
                {
                   frontdesk.ErrorMessage(new ErrorEventArgs("", ex));
                }
                return null;
            }
            catch (Exception ex)
            {
                raiseError(ex);
                return null;
            }             
        }

        public List<Table> GetTable(DateTime date)
        {
            FrontDeskRequest frontdesk = FrontDeskRequestControl.GetService(callBackId);
            try
            {             
                return TableDao.GetAvailablePreorderTable(date);
            }
            catch (HCSMSException ex)
            {
                raiseError(ex);
                if (frontdesk != null)
                {
                   frontdesk.ErrorMessage(new ErrorEventArgs("", ex));
                }
                return null;
            }
            catch (Exception ex)
            {
                raiseError(ex);
                return null;
            }  
          
        }


        public void UseTable(Dictionary<Table, DinningTable> tables)
        {
            FrontDeskRequest frontdesk = FrontDeskRequestControl.GetService(callBackId);
            try
            {
                HCSMSLog.OnWarningLog(this, new NotifyEventArgs("Recieve use table Request"));

                foreach (KeyValuePair<Table, DinningTable> pair in tables)
                {
                    TableDao.InsertDinningTable(pair.Key.Number, pair.Value);
                }
            }
            catch (HCSMSException ex)
            {
                raiseError(ex);
                if (frontdesk != null)
                {
                   frontdesk.ErrorMessage(new ErrorEventArgs("", ex));
                }
            }
            catch (Exception ex)
            {
                raiseError(ex);
            }                   
        }

        public void ChangeTable(string oldTableNumber, string newTableNumber)
        {
            FrontDeskRequest frontdesk=FrontDeskRequestControl.GetService(callBackId);
            try
            {
                HCSMSLog.OnWarningLog(this, new NotifyEventArgs("Recieve change table Request"));

                List<Table> tableList = TableDao.GetAvailableTable();
                bool tableExist = false;
                foreach (Table table in tableList)
                {
                    if (table.Number == oldTableNumber)
                    {
                        tableExist = true;
                        break;
                    }
                }
                if (tableExist)
                {
                    TableDao.ChangeTable(((DinningTable)TableDao.GetDinningTable(oldTableNumber)).Id, oldTableNumber, newTableNumber);
                }
                else
                {
                    TableDao.MergeTable(((DinningTable)TableDao.GetDinningTable(oldTableNumber)).Id, ((DinningTable)TableDao.GetDinningTable(newTableNumber)).Id);
                }               
            }
            catch (HCSMSException ex)
            {
                raiseError(ex);
                if (frontdesk != null)
                {
                   frontdesk.ErrorMessage(new ErrorEventArgs("", ex));
                }
            }
            catch (Exception ex)
            {
                raiseError(ex);
            }           
         
        }
        #endregion

        #region IDinningService 成员

        public void AcceptServerReply(string callBackId)
        {
            this.callBackId = callBackId;
        }

        #endregion
    }
}
