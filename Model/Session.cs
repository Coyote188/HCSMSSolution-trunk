using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HCSMS.Model
{
    [Serializable]
    public class Session
    {
        private SystemAccount account;
        private Guid id;
        private int timeout=30;

        public Session(SystemAccount anAccount)
        {
            account = anAccount;
            id = Guid.NewGuid();
        }

        public SystemAccount Account { get { return account; } }
        public Guid Id { get { return id; } }
        public int TimeOut { get { return timeout; } }
    }
}
