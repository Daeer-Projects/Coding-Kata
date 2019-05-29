using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using DataMungingCoreV2.Interfaces;

namespace DataMungingCoreV2.Processors
{
    public static class Mapper
    {
        public static Task<IList<IDataType>> MapWork(Func<IList<IDataType>> mapResults)
        {
            return Task.Factory.StartNew(mapResults);
        }
    }
}
