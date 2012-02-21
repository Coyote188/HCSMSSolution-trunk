using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HCSMS.Model;
using System.ServiceModel;

namespace HCSMS.Service
{
  [ServiceContract]
    public interface IDinningService : IDinningItemService, IDinningTableService
    {
      [OperationContract]
      void AcceptServerReply(string callBackId);
    }
}
