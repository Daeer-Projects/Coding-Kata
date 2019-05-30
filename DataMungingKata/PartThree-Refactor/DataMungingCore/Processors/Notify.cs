using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DataMungingCoreV2.Interfaces;
using DataMungingCoreV2.Types;

namespace DataMungingCoreV2.Processors
{
    public static class Notify
    {
        public static Task<IReturnType> NotificationWork<T, TU, TV>(IEnumerable<IDataType> data,
            (TU, TV) defaultParameters,
            Func<(TU, TV), T, (TU, TV)> evaluateCurrentRange) 
            where T: class
            where TU: object
            where TV: object
        {
            return Task.Factory.StartNew(() =>
            {
                var dataResults =
                    data.Aggregate((defaultParameters),
                        (current, type) => SmallestRange<T, TU, TV>(type, current, evaluateCurrentRange));

                IReturnType day = new ContainingResultType {ProcessResult = dataResults.Item2};

                return day;
            });
        }

        private static (TU, TV) SmallestRange<T, TU, TV>(IDataType type, 
            (TU, TV) currentRange,
            Func<(TU, TV), T, (TU, TV)> evaluateCurrentRange) 
            where T : class
            where TU : object
            where TV : object
        {
            if (type.Data is T componentType)
            {
                currentRange = evaluateCurrentRange(currentRange, componentType);
            }

            return currentRange;
        }
    }
}
