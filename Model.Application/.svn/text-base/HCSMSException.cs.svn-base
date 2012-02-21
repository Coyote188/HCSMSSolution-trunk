using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.Runtime.Serialization;

namespace HCSMS.Model.Application
{
    [DataContract]
    public class HCSMSException:ApplicationException
    {
        public HCSMSException()
        {
        }
        public HCSMSException(string errorCode):base(errorCode)
        {            
        }
        public HCSMSException(string errorCode, Exception inner): base(errorCode, inner)
        {                
        }

        [DataMember]
        public string ErrorCode { get { return Message; } }
      
    }
}
