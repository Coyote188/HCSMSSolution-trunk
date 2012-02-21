using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HCSMS.Model;
using HCSMS.Model.Application;

using System.ServiceModel;
using System.Threading;

namespace HCSMS.Presentation.Impl
{
    public class KitchenCallBackUI: UserInterface
    {
        private KitchenCallBack kitchenCallBack;
        private object lockObj;

        public event HandleEventHandler RequestForChangeItemEvent
        {
            add
            {
                kitchenCallBack.ChangeItemEvent += value;
            }
            remove
            {
                kitchenCallBack.ChangeItemEvent -= value;
            }
        }
        public event HandleEventHandler RequestForChangeTableEvent
        {
            add
            {
                kitchenCallBack.ChangeTableEvent += value;
            }
            remove
            {
                kitchenCallBack.ChangeTableEvent -= value;
            }
        }
        public event HandleEventHandler RequestForOrderItemEvent
        {
            add
            {
                kitchenCallBack.OrderItemEvent += value;
            }
            remove
            {
                kitchenCallBack.OrderItemEvent -= value;
            }
        }
        public event HandleEventHandler RequestForDeorderItemEvent
        {
            add
            {
                kitchenCallBack.DeorderItemEvent += value;
            }
            remove
            {
                kitchenCallBack.DeorderItemEvent -= value;
            }
        }
        public event DishPrioritizeHandler ProritizeDishEvent
        {
            add
            {
                kitchenCallBack.PrioritizeDishEvent += value;
            }
            remove
            {
                kitchenCallBack.PrioritizeDishEvent -= value;
            }
        }

        private Thread t;
        private bool isFinish;
        public KitchenCallBackUI()
        {
            kitchenCallBack = new KitchenCallBack();
            lockObj = new object();
            t = new Thread(listenToServer);
            isFinish = false;

            //kitchenCallBack.ChangeItemEvent += kitchenCallBack_ChangeItemEvent;
            //kitchenCallBack.ChangeTableEvent += new HandleEvent(kitchenCallBack_ChangeTableEvent);
            //kitchenCallBack.DeorderItemEvent += new HandleEvent(kitchenCallBack_DeorderItemEvent);
        }

        

        public void Start()
        {
            lock (lockObj)
            {
                Monitor.Enter(lockObj);
            }
            t.Start();
        }
        public void Stop()
        {
            lock (lockObj)
            {
                isFinish = true;
                Monitor.Exit(lockObj);
            }
          //  t.Join();
        }
        private void listenToServer()
        {
            InstanceContext context = new InstanceContext(kitchenCallBack);

            using (KitchenRequest.KitchenRequestClient proxy = new KitchenRequest.KitchenRequestClient(context))
            {
                try
                {
                    proxy.ListenToServer();
                    lock (lockObj)
                    {
                        while (!isFinish)
                        {
                            Monitor.Wait(lockObj);
                        }
                    }
                }
                catch (FaultException<Exception> ex)
                {
                    raiseError(ex);
                }
                catch (Exception ex)
                {
                    raiseError(ex);
                }
            }
        }
    }
}
