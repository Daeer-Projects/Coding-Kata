using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DataMungingCoreV2.Interfaces;

namespace DataMungingCoreV2.Processors
{
    public static class Mapper
    {
        public static Task<IList<IDataType>> MapWork(string[] fileData,
            Func<string, bool> checkItemRow,
            Func<string, IList<IDataType>, IList<IDataType>> addDataItem)
        {
            // Need to add some contract requirements.

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
