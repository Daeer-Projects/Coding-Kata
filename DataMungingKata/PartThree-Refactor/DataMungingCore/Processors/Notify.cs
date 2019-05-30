using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataMungingCoreV2.Interfaces;
using DataMungingCoreV2.Types;

namespace DataMungingCoreV2.Processors
{
    public static class Notify
    {
        public static Task<IReturnType> NotificationWork(IList<IDataType> data)
        {
            return Task.Factory.StartNew(() =>
            {
                IReturnType result = new ContainingResultType();
                return result;
                /* do stuff*/
            });
        }
    }
}
