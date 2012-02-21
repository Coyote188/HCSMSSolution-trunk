using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.Runtime.Serialization;

namespace HCSMS.Model.Application
{
    [DataContract]
   public class ErrorEventArgs: EventArgs
    {
 
        public ErrorEventArgs(string msg,Exception ex)
        {
            Message = msg;
            Exception = ex;
        }

        [DataMember]
        public string Message { get; set; }


        [DataMember]
        public Exception Exception { get; set; }
    }
}
