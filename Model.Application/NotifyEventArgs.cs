using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.Runtime.Serialization;

namespace HCSMS.Model.Application
{
    [DataContract]
    public class NotifyEventArgs: EventArgs
    {
     

        public NotifyEventArgs(string msg)
        {
            Message = msg;
        }

        [DataMember]
        public string Message { get; set; }

    }
}
