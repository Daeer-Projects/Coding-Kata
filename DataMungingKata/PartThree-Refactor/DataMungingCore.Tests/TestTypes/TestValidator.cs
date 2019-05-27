using System;

using FluentValidation;

namespace DataMungingCoreV2.Tests.TestTypes
{
    public class TestValidator : AbstractValidator<TestType>
    {
        public TestValidator()
        {
            RuleFor(test => test).NotNull();
            RuleFor(test => test.TestIdentity).GreaterThan(0);
            RuleFor(test => test.TestName).Must(TestNameMustNotBeNullOrWhiteSpace);
            RuleFor(test => test.TestDateTime).InclusiveBetween(new DateTime(2010, 01, 01), new DateTime(2030, 12, 31));
        }

        private bool TestNameMustNotBeNullOrWhiteSpace(string testName)
        {
            return !string.IsNullOrWhiteSpace(testName);
        }
    }
}
