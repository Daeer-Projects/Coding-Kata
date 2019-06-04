using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DataMungingCoreV2.Interfaces;
using DataMungingCoreV2.Types;

namespace DataMungingCoreV2.Processors
{
    /// <summary>
    /// The static writer helper.  This is used by the component to write the answer to the components question back to it.
    /// </summary>
    public static class Writer
    {
        /// <summary>
        /// This method does the work defined by the component.
        /// The component writer needs to answer a question based on the data extracted and mapped from a file.
        /// To do that, this method uses the following information:
        /// <list type="number">
        /// <item> The mapped data extracted from the file. </item>
        /// <item> A set of default parameters defined as a tuple. </item>
        /// <item> A function that does most of the work to identify the answer. </item>
        /// </list>
        /// </summary>
        /// <typeparam name="T"> A class.  This is the main type for the component. The type that the data in the file is mapped to. </typeparam>
        /// <typeparam name="TU"> The type of the first item in the tuple for the default parameters. </typeparam>
        /// <typeparam name="TV"> The type of the second item in the tuple for the default parameters. </typeparam>
        /// <param name="data"> The mapped data extracted from the components file. </param>
        /// <param name="defaultParameters"> The default parameters required for the calculation to work. </param>
        /// <param name="evaluateCurrentRange"> The function that does most of the work in the component. </param>
        /// <returns>
        /// The answer to the components question.
        /// </returns>
        public static Task<IReturnType> WriteWork<T, TU, TV>(IEnumerable<IDataType> data,
            (TU, TV) defaultParameters,
            Func<(TU, TV), T, (TU, TV)> evaluateCurrentRange) 
            where T: class
            where TU: object
            where TV: object
        {
            // Contract requirements.
            if (data is null) throw new ArgumentNullException(nameof(data), "The data to be processed must not be null.");
            var dataTypes = data.ToList();
            if (!dataTypes.Any()) throw new ArgumentException(nameof(data), "The data to process must contain data.");
            if (defaultParameters.Item1 is null) throw new ArgumentNullException(nameof(defaultParameters.Item1), "Default Parameters: The first item in the default parameters is null.");
            if (defaultParameters.Item2 is null) throw new ArgumentNullException(nameof(defaultParameters.Item2), "Default Parameters: The second item in the default parameters is null.");
            if (evaluateCurrentRange is null) throw new ArgumentNullException(nameof(evaluateCurrentRange), "The expected function to process the data is null.");

            return Task.Factory.StartNew(() =>
            {
                var dataResults =
                    dataTypes.Aggregate((defaultParameters),
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
