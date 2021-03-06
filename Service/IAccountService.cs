﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HCSMS.Model;
using System.ServiceModel;


namespace HCSMS.Service
{
    [ServiceContract]
    public interface IAccountService
    {
        [OperationContract]       
        Session StartSession(Account anAccount);
        [OperationContract]
        void StopSession(Session session);
        [OperationContract]        
        bool IsLogin(Session session);
    }
}
