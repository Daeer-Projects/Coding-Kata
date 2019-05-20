using System;
using System.Collections.Generic;

using DataMungingCore.Interfaces;
using Easy.MessageHub;
using FluentAssertions;
using NSubstitute;
using Serilog;
using Xunit;

namespace DataMungingCore.Tests
{
    public class ComponentRegisterTests
    {
        private readonly IMessageHub _hub;
        private readonly ILogger _logger;
        private ComponentRegister _componentRegister;

        public ComponentRegisterTests()
        {
            _hub = Substitute.For<IMessageHub>();
            _logger = Substitute.For<ILogger>();

            _componentRegister = new ComponentRegister(_hub, _logger);
        }

        [Theory]
        [MemberData(nameof(GetMixedConstructorParameters))]
        public void Test_construction_with_mixed_null_parameters_throws_null_exception(IMessageHub hub, ILogger logger)
        {
            // Arrange.
            // Act.
            // Assert.
            Assert.Throws<ArgumentNullException>(() => _componentRegister = new ComponentRegister(hub, logger));
        }

        [Theory]
        [MemberData(nameof(GetMixedParameters))]
        public void Test_register_with_mixed_null_parameters_returns_false(IComponentCreator creator, string fileName)
        {
            // Arrange.
            // Act.
            var actual = _componentRegister.RegisterComponent(creator, fileName);

            // Assert.
            actual.Should().BeFalse("if one of the components used is null (or empty), then we will return false.");
        }

        [Fact]
        public void Test_register_with_valid_parameters_is_received_by_the_hub()
        {
            // Arrange.
            var creator = Substitute.For<IComponentCreator>();
            const string input = "fileName";

            // Act.
            _componentRegister.RegisterComponent(creator, input);

            // Assert.
            _hub.Received(1).Subscribe(Arg.Any<Action<string>>());
        }

        [Fact]
        public void Test_register_with_valid_parameters_returns_true()
        {
            // Arrange.
            var creator = Substitute.For<IComponentCreator>();
            const string input = "fileName";

            // Act.
            var result = _componentRegister.RegisterComponent(creator, input);

            // Assert.
            result.Should()
                .BeTrue("we have a creator and we have a file, so we should be able to register the component.");
        }

        [Fact]
        public void Test_register_with_valid_parameters_adds_component_to_list()
        {
            // Arrange.
            var creator = Substitute.For<IComponentCreator>();
            const string input = "fileName";

            // Act.
            _componentRegister.RegisterComponent(creator, input);

            // Assert.
            _componentRegister.Components.Count.Should().BeGreaterThan(0, "we have just added a new component to it.");
        }

        [Fact]
        public void Test_subscribe_returns_true()
        {
            // Arrange.
            // Act.
            var result = _componentRegister.RegisterSubscriptions();

            // Assert.
            result.Should().BeTrue("if it didn't raise an exception, then the hub is ready.");
        }

        [Fact]
        public void Test_subscribe_is_received_by_the_hub()
        {
            // Arrange.
            // Act.
            _componentRegister.RegisterSubscriptions();

            // Assert.
            _hub.Received(1).Subscribe(Arg.Any<Action<IReturnType>>());
        }

        #region Test Data.

        public static IEnumerable<object[]> GetMixedConstructorParameters
        {
            get
            {
                yield return new object[]
                {
                    null,
                    Substitute.For<ILogger>()
                };
                yield return new object[]
                {
                    Substitute.For<IMessageHub>(),
                    null
                };
                yield return new object[]
                {
                    null,
                    null
                };
            }
        }

        public static IEnumerable<object[]> GetMixedParameters
        {
            get
            {
                yield return new object[]
                {
                    null,
                    "FileName"
                };
                yield return new object[]
                {
                    Substitute.For<IComponentCreator>(),
                    null
                };
                yield return new object[]
                {
                    Substitute.For<IComponentCreator>(),
                    string.Empty
                };
                yield return new object[]
                {
                    Substitute.For<IComponentCreator>(),
                    "    "
                };
                yield return new object[]
                {
                    null,
                    null
                };
            }
        }

        #endregion Test Data.
    }
}
