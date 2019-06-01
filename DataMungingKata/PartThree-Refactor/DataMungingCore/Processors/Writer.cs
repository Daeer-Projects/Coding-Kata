using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DataMungingCoreV2.Interfaces;
using DataMungingCoreV2.Types;

namespace DataMungingCoreV2.Processors
{
    public static class Writer
    {
        public static Task<IReturnType> WriteWork<T, TU, TV>(IEnumerable<IDataType> data,
            (TU, TV) defaultParameters,
            Func<(TU, TV), T, (TU, TV)> evaluateCurrentRange) 
            where T: class
            where TU: object
            where TV: object
        {
            // Contract requirements.
            if (data is null) throw new ArgumentNullException(nameof(data), "The data to be processed must not be null.");
            if (!data.Any()) throw new ArgumentException(nameof(data), "The data to process must contain data.");
            if (defaultParameters.Item1 is null) throw new ArgumentNullException(nameof(defaultParameters.Item1), "Default Parameters: The first item in the default parameters is null.");
            if (defaultParameters.Item2 is null) throw new ArgumentNullException(nameof(defaultParameters.Item2), "Default Parameters: The second item in the default parameters is null.");
            if (evaluateCurrentRange is null) throw new ArgumentNullException(nameof(evaluateCurrentRange), "The expected function to process the data is null.");

            return Task.Factory.StartNew(() =>
            {
                var dataResults =
                    data.Aggregate((defaultParameters),
                        (current, type) => SmallestRange<T, TU, TV>(type, current, evaluateCurrentRange));

                IReturnType answer = new ContainingResultType {ProcessResult = dataResults.Item2};

                return answer;
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
