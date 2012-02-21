using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HCSMS.Model
{
    [Serializable]
    public class Account
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
