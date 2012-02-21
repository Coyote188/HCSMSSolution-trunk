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
    public class KitchenService : ServiceImplementation, IKitchenService
    {      
        public KitchenService():base()
        {            
           
        }
        ~KitchenService()
        {
           
        }               
      

        #region IKitchenService 成员
       
        public void StopSupplyItem(List<Item> itemList)
        {
            try
            {
                //save record to database

                ItemDao.StopSupplyItem(itemList);
                List<RequestHandleInfo> requestList = new List<RequestHandleInfo>();
                foreach (Item item in itemList)
                {
                    RequestHandleInfo info = new RequestHandleInfo();
                    info.RequestType = RequestType.DeorderItem;
                    info.SourceId = item.Id;
                    requestList.Add(info);
                }
                //HandleEventHandler(StopSupplyItemEvent, new HandleEventArgs("stop supply item", requestList));
            }
            catch (HCSMSException ex)
            {
                raiseError(ex);
                throw new FaultException<HCSMSException>(ex);
            }
            catch (Exception ex)
            {
                raiseError(ex);
            }
        }

        #endregion

       
    }
}
