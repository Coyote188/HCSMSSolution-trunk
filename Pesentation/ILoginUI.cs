using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HCSMS.Model;

namespace HCSMS.Presentation
{
    public interface ILoginUI
    {
        bool IsValid();
        void Login(string userName, string password);
    }
}
