using System;
using System.Collections.Generic;
using System.Linq;
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

        public static IList<IDataType> ExperimentalMapWork(string[] fileData,
            Func<string, bool> checkItemRow,
            Func<IList<IDataType>, string, IList<IDataType>> addDataItem)
        {
            var taskResults = MapDataToResults(fileData, checkItemRow, addDataItem);
            return taskResults;
        }

        private static IList<IDataType> MapDataToResults(string[] fileData,
            Func<string, bool> checkItemRow,
            Func<IList<IDataType>, string, IList<IDataType>> addDataItem)
        {
            IList<IDataType> taskResults = new List<IDataType>();

            return fileData.Aggregate(taskResults, (current, item) => AddData(item, current, checkItemRow, addDataItem));
        }

        private static IList<IDataType> AddData(string item, 
            IList<IDataType> taskResults,
            Func<string, bool> checkItemRow,
            Func<IList<IDataType>, string, IList<IDataType>> addDataItem)
        {
            var results = taskResults;
            if (checkItemRow(item))
            {
                results = addDataItem(results, item);
            }

            return results;
        }
    }
}
