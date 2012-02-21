using System;
using System.Collections.Generic;
using System.Text;

namespace HCSMS.Model
{
    [Serializable]
    public enum RequestType
    {
        ChangeItem,
        OrderItem,
        DeorderItem,
        ChangeTable,
    }
}
