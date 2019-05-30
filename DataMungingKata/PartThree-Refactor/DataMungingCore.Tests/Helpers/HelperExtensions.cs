using System;

using DataMungingCoreV2.Tests.TestTypes;

namespace DataMungingCoreV2.Tests.Helpers
{
    public static class HelperExtensions
    {
        /// <summary>
        /// A simple extractor.  Must get this right, as there is no checking here.
        /// </summary>
        /// <param name="item"> The string being converted. </param>
        /// <returns>
        /// A copy of the football and weather to validator type, but without the checks.
        /// </returns>
        public static TestValidatorType ToTestType(this string item)
        {
            var isTestValidType = new TestValidatorType
            {
                IsValid = true,
                TestType = new TestType
                {
                    TestIdentity = int.Parse(item.Substring(1, 4)),
                    TestName = item.Substring(6, 10).Trim(),
                    TestDateTime = DateTime.Parse(item.Substring(20, 19))
                }
            };

            return isTestValidType;
        }
    }
}
