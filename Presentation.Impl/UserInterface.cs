using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HCSMS.Model;
using HCSMS.Model.Application;
using System.ServiceModel;

namespace HCSMS.Presentation.Impl
{
    public abstract class UserInterface
    {
        private Stack<Exception> exception;
        private List<string> replyMessage;
        private Session session = null;

        public bool IsUsingErrorContainer { get; set; }
        public bool IsUsingReplyContainer{get;set;}
        public Session Session { get { return session; } set { session = value; } }

        public UserInterface(Session session):this()
        {         
            this.session = session;
            if (!IsAuthorize(this.session))
            {
                throw new Exception("请登陆!");
            }
        }
        public UserInterface()
        {
            exception = new Stack<Exception>();

            IsUsingErrorContainer = false;
            IsUsingReplyContainer = false;                   
        }

        //check for user authentication
        private bool IsAuthorize(Session session)
        {
            using (AccountService.AccountServiceClient proxy = new AccountService.AccountServiceClient())
            {
                try
                {
                  
                    return proxy.IsLogin(session);

                }
                catch (FaultException<HCSMSException> ex)
                {
                    raiseError(ex);
                    return false;
                }
                catch (Exception ex)
                {
                    raiseError(ex);
                    return false;
                }
              
            }
        }


#region Definition of Events for use of all class in Presentation Layer

        public delegate void NotifyEventHandler(object sender, NotifyEventArgs args);
        public delegate void NotifyListEventHandler(List<RequestHandleInfo> itemList);
        public delegate List<RequestHandleInfo> HandleEventHandler(object sender, List<RequestHandleInfo> requestList);
        public delegate void ErrorEventHandler(object sender, ErrorEventArgs args);
        public delegate void DishPrioritizeHandler(string tableNumber, string itemId, PriorityLevel level);

       
#endregion

 # region Error management block

        public List<Exception> ErrorContainer { get { return exception.ToList(); } }
        protected virtual void raiseError(Exception ex)
        {
            ErrorEventArgs args = new ErrorEventArgs("Application Errors!", ex);
            if (IsUsingErrorContainer)
            {
                exception.Push(ex);
            }
        }
        public Exception HandleException()
        {
            if (IsUsingErrorContainer)
            {
                try
                {
                    return exception.Pop();
                }
                catch (ArgumentNullException ex)
                {
                    raiseError(ex);
                    return null;
                }
            }
            else return null;
        }
        public void ClearException()
        {
            exception.Clear();
        }

# endregion



#region    Generic Method for handling event

        protected virtual void ProcessNotifyEvent(NotifyEventHandler handler, NotifyEventArgs args)
        {
            if (handler != null)
            {
                handler(this, args);               
            }
        }
        protected virtual List<RequestHandleInfo> ProcessHandleEvent(HandleEventHandler handler, HandleEventArgs args)
        {
            if (handler != null)
            {
                return handler(this, args.RequestInfo);
            }
            else
            {
                return new List<RequestHandleInfo>();
            }
        }
        protected virtual void ProcessErrorEvent(ErrorEventHandler handler, ErrorEventArgs args)
        {
            if (handler != null)
            {
                handler(this, args);
                HandleException();
            }
        }

#endregion

    }
}
