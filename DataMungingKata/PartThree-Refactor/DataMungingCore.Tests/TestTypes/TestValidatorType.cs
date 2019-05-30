using System.Collections.Generic;

namespace DataMungingCoreV2.Tests.TestTypes
{
    public class TestValidatorType
    {
        public TestType TestType { get; set; }
        public bool IsValid { get; set; }
        public List<string> ErrorList { get; set; }

        public TestValidatorType()
        {
            TestType = new TestType();
            IsValid = false;
            ErrorList = new List<string>();
        }
    }
}
