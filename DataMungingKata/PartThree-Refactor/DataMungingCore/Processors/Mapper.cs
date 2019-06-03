using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using DataMungingCoreV2.Extensions;
using DataMungingCoreV2.Interfaces;
using DataMungingCoreV2.Validators;

namespace DataMungingCoreV2.Processors
{
    /// <summary>
    /// The static mapper helper.  This is used by the component to map the data from the reader.
    /// </summary>
    public static class Mapper
    {
        /// <summary>
        /// This method does the work defined by the component.
        /// It is a template of how the components work will be executed.
        /// The method wraps the work into a task.
        /// The list of data to process is iterated through.
        /// The functions defined by the component will be executed as the relevant time.
        /// <remarks>
        /// The parameters required by this method, and defined by the component are as follows:
        /// <list type="number">
        /// <listheader>
        /// <term> Parameter Name </term>
        /// <description> Details </description>
        /// </listheader>
        /// <item>
        /// <term> fileData </term>
        /// <description> The string array of data to be mapped. </description>
        /// </item>
        /// <item>
        /// <term> checkItemRow </term>
        /// <description> This function is to define the check on the data that is then passed onto the next part, or rejected. </description>
        /// </item>
        /// <item>
        /// <term> addDataItem </term>
        /// <description> If we got through the check, we expect this method to convert the data to a specific class, validate it, and then add it to the final list. </description>
        /// </item>
        /// </list>
        /// </remarks>
        /// </summary>
        /// <param name="fileData"> The data read in by the reader. </param>
        /// <param name="checkItemRow">
        /// This is a function defined by the component.
        /// It is used in an if statement and if the result is true, then the data item is passed to the addDataItem function.
        /// </param>
        /// <param name="addDataItem">
        /// This is a function defined by the component.
        /// This function decides what to do with the data.
        /// It is expected that this function will convert the data item into a specific class, validate it, and then add it to the list
        /// that the writer will process.
        /// </param>
        /// <returns>
        /// The list of valid data ready for the writer to process.
        /// </returns>
        public static Task<IList<IDataType>> MapWork(string[] fileData,
            Func<string, bool> checkItemRow,
            Func<string, IList<IDataType>, IList<IDataType>> addDataItem)
        {
            // Contract requirements.
            if (fileData == null) throw new ArgumentNullException(nameof(fileData), "Invalid Data File.");
            if (!fileData.IsValid(new BaseStringArrayValidator()).IsValid) throw new InvalidDataException("Invalid Data File.");
            if (checkItemRow == null) throw new ArgumentNullException(nameof(checkItemRow), "Invalid CheckItemRow function.");
            if (addDataItem == null) throw new ArgumentNullException(nameof(addDataItem), "Invalid addDataItem function.");

            return Task.Factory.StartNew(() => MapDataToResultsList(fileData, checkItemRow, addDataItem));
        }

        private static IList<IDataType> MapDataToResultsList(string[] fileData,
            Func<string, bool> checkItemRow,
            Func<string, IList<IDataType>, IList<IDataType>> addDataItem)
        {
            IList<IDataType> taskResults = new List<IDataType>();

            return fileData.Aggregate(taskResults, (current, item) => AddData(item, current, checkItemRow, addDataItem));
        }

        private static IList<IDataType> AddData(string item, 
            IList<IDataType> taskResults,
            Func<string, bool> checkItemRow,
            Func<string, IList<IDataType>, IList<IDataType>> addDataItem)
        {
            var results = taskResults;
            if (checkItemRow(item))
            {
                results = addDataItem(item, results);
            }

            return results;
        }
    }
}
