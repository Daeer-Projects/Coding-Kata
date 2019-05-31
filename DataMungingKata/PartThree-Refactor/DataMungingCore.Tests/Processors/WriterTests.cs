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
    public class WriterTests
    {
        [Theory]
        [MemberData(nameof(GetValidTestData))]
        public async Task Test_write_work_returns_expected_result(int expected, IList<IDataType> data)
        {
            // Arrange.
            // Act.
            var results = await Writer.WriteWork<TestType, int?, int?>(data, (int.MaxValue, 0), CurrentRange)
                .ConfigureAwait(true);

            // Assert.
            results.ProcessResult.Should().Be(expected, "the data matches the identity to the expected.");
        }

        [Fact]
        public async Task Test_write_work_with_null_data_throws_exception()
        {
            // Arrange.
            // Act.
            // Assert.
            await Assert
                .ThrowsAsync<ArgumentNullException>(() =>
                    Writer.WriteWork<TestType, int?, int?>(null, (int.MaxValue, 0), CurrentRange)).ConfigureAwait(true);
        }

        [Fact]
        public async Task Test_write_work_with_null_default_parameters_throws_exception()
        {
            // Arrange.
            var data = new List<IDataType>
            {
                new ContainingDataType
                    {Data = new TestType {TestIdentity = 1, TestName = "Pooh", TestDateTime = DateTime.Now.AddDays(-1)}}
            };

            // Act.
            // Assert.
            await Assert
                .ThrowsAsync<ArgumentNullException>(() =>
                    Writer.WriteWork<TestType, int?, int?>(data, (null, null), CurrentRange)).ConfigureAwait(true);
        }

        [Fact]
        public async Task Test_write_work_with_null_function_throws_exception()
        {
            // Arrange.
            var data = new List<IDataType>
            {
                new ContainingDataType
                    {Data = new TestType {TestIdentity = 1, TestName = "Pooh", TestDateTime = DateTime.Now.AddDays(-1)}}
            };

            // Act.
            // Assert.
            await Assert
                .ThrowsAsync<ArgumentNullException>(() =>
                    Writer.WriteWork<TestType, int?, int?>(data, (int.MaxValue, 0), null)).ConfigureAwait(true);
        }

        #region Testing Methods

        private (int?, int?) CurrentRange<T>((int?, int?) currentRange, T componentType) where T : class
        {
            // Casting to expected type.
            var specificType = componentType as TestType;
            var calculation = specificType.CalculateTestIdentity();

            if (calculation < currentRange.Item1)
            {
                currentRange.Item1 = calculation;
                currentRange.Item2 = specificType.TestIdentity;
            }

            return currentRange;
        }

        #endregion Testing Methods

        #region Test Data

        public static IEnumerable<object[]> GetValidTestData
        {
            get
            {
                yield return new object[]
                {
                    1,
                    new List<IDataType>
                    {
                        new ContainingDataType
                            {Data = new TestType {TestIdentity = 1, TestName = "Pooh", TestDateTime = DateTime.Now.AddDays(-1)}}
                    }
                };
                yield return new object[]
                {
                    1,
                    new List<IDataType>
                    {
                        new ContainingDataType
                            {Data = new TestType {TestIdentity = 1, TestName = "Pooh", TestDateTime = DateTime.Now.AddDays(-1)}},
                        new ContainingDataType
                            {Data = new TestType {TestIdentity = 2, TestName = "Tigger", TestDateTime = DateTime.Now.AddDays(-10)}}
                    }
                };
                yield return new object[]
                {
                    2,
                    new List<IDataType>
                    {
                        new ContainingDataType
                            {Data = new TestType {TestIdentity = 1, TestName = "Pooh", TestDateTime = DateTime.Now.AddDays(-100)}},
                        new ContainingDataType
                            {Data = new TestType {TestIdentity = 2, TestName = "Tigger", TestDateTime = DateTime.Now.AddDays(-5)}},
                        new ContainingDataType
                            {Data = new TestType {TestIdentity = 3, TestName = "Roo", TestDateTime = DateTime.Now.AddDays(-15)}}
                    }
                };
            }
        }

        #endregion Test Data
    }
}
