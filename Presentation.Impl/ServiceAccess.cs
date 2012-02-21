using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;

namespace HCSMS.Presentation.Impl
{
    public delegate void UseServiceDelegate<T>(T proxy);

    public static class ServiceAccess<T>
    {
        public static ChannelFactory<T> _channelFactory = new ChannelFactory<T>("");

        public static void Use(UseServiceDelegate<T> codeBlock)
        {
            IClientChannel proxy = (IClientChannel)_channelFactory.CreateChannel();
            bool success = false;
            try
            {
                codeBlock((T)proxy);
                proxy.Close();
                success = true;
            }
            finally
            {
                if (!success)
                {
                    proxy.Abort();
                }
            }
        }
    }
}
