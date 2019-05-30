using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using DataMungingCoreV2.Interfaces;
using DataMungingCoreV2.Processors;
using DataMungingCoreV2.Tests.Helpers;
using DataMungingCoreV2.Tests.TestTypes;
using DataMungingCoreV2.Types;
using FluentAssertions;
using Xunit;

namespace DataMungingCoreV2.Tests.Processors
{
    public class MapperTests
    {
        [Fact]
        public async Task Test_map_work_returns_expected_list()
        {
            // Arrange.
            // Act.
            var results = await Mapper.MapWork(GetData(), CheckItemRow, AddDataItem).ConfigureAwait(true);

            // Assert.
            results.Should().BeEquivalentTo(GetExpectedData());
        }

        #region Testing Methods
        
        /// <summary>
        /// The test version of the row checker.
        /// </summary>
        /// <param name="item"> The string we are checking. </param>
        /// <returns>
        /// If the string is OK to map.
        /// </returns>
        private static bool CheckItemRow(string item)
        {
            return !item.Equals(string.Empty) &&
                   !item.Equals("banana") &&
                   !item.Equals("-----------------------------------------");
        }

        /// <summary>
        /// The test version of the add data item.
        /// </summary>
        /// <param name="item"> The string we are adding. </param>
        /// <param name="results"> The current result list. </param>
        /// <returns>
        /// The updated results list.
        /// </returns>
        private IList<IDataType> AddDataItem(string item, IList<IDataType> results)
        {
            var dataResults = results;
            var data = item.ToTestType();
            if (data.IsValid)
            {
                dataResults.Add(new ContainingDataType { Data = data.TestType });
            }

            return dataResults;
        }

        #endregion Testing Methods

        #region Test Data

        private string[] GetData()
        {
            return new[]
            {
                "   1  Pooh          2016-01-23 09:00:12",
                "   2  Piglet        2017-02-23 10:30:45",
                "   3  Tigger        2018-03-23 11:06:27",
                "   4  Eeyore        2019-04-23 12:12:57"
            };
        }

        private List<IDataType> GetExpectedData()
        {
            var expectedList = new List<IDataType>
            {
                new ContainingDataType
                {
                    Data = new TestType
                    {
                        TestIdentity = 1,
                        TestName = "Pooh",
                        TestDateTime = new DateTime(2016, 01, 23, 09, 00, 12)
                    }
                },
                new ContainingDataType
                {
                    Data = new TestType
                    {
                        TestIdentity = 2,
                        TestName = "Piglet",
                        TestDateTime = new DateTime(2017, 02, 23, 10, 30, 45)
                    }
                },
                new ContainingDataType
                {
                    Data = new TestType
                    {
                        TestIdentity = 3,
                        TestName = "Tigger",
                        TestDateTime = new DateTime(2018, 03, 23, 11, 06, 27)
                    }
                },
                new ContainingDataType
                {
                    Data = new TestType
                    {
                        TestIdentity = 4,
                        TestName = "Eeyore",
                        TestDateTime = new DateTime(2019, 04, 23, 12, 12, 57)
                    }
                }
            };

            return expectedList;
        }

        #endregion Test Data
    }
}
