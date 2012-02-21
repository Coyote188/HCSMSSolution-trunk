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
    public class FrontDeskCallBackUI : UserInterface
    {
      
        private FrontDeskCallBack frontDeskCallBack;
        private object lockObj;

        public event NotifyEventHandler StopSupplyItemEvent
        {
            add
            {
                frontDeskCallBack.StopSupplyItemEvent += value;
            }
            remove
            {
                frontDeskCallBack.StopSupplyItemEvent -= value;
            }
        }
        public event NotifyEventHandler RunOutOfItemEvent
        {
            add
            {
                frontDeskCallBack.RunOutOfItemEvent += value;
            }
            remove
            {
                frontDeskCallBack.RunOutOfItemEvent -= value;
            }
        }
        public event NotifyListEventHandler ServeItemEvent
        {
            add
            {
                frontDeskCallBack.ServeItemEvent += value;
            }
            remove
            {
                frontDeskCallBack.ServeItemEvent -= value;
            }
        }
        public event NotifyListEventHandler RequestItemEvent
        {
            add
            {
                frontDeskCallBack.RequestItemEvent += value;
            }
            remove
            {
                frontDeskCallBack.RequestItemEvent -= value;
            }
        }
        public event ErrorEventHandler ErrorEvent
        {
            add
            {
                frontDeskCallBack.ErrorEvent += value;
            }
            remove
            {
                frontDeskCallBack.ErrorEvent -= value;
            }
        }
        public event NotifyEventHandler ServerReplyEvent
        {
            add
            {
                frontDeskCallBack.ServerReplyEvent += value;
            }
            remove
            {
                frontDeskCallBack.ServerReplyEvent -= value;
            }
        }

        public string CallBackId { get { return OperationContext.Current.SessionId; } }

        private Thread t;
        private bool isFinish;
        public FrontDeskCallBackUI()
        {
            frontDeskCallBack = new FrontDeskCallBack();
            lockObj = new object();
            t = new Thread(listenToServer);
            isFinish = false;
            
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
            //t.Join();
        }
        private void listenToServer()
        {
            InstanceContext context = new InstanceContext(frontDeskCallBack);

            using (FrontDeskRequest.FrontDeskRequestClient proxy = new FrontDeskRequest.FrontDeskRequestClient(context))
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
