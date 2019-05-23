using DataMungingCore.Interfaces;
using Easy.MessageHub;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace FootballComponent.Tests
{
    public class FootballComponentCreatorTests
    {

        private readonly IMessageHub _messageHub;
        private readonly IComponentCreator _componentCreator;

        public FootballComponentCreatorTests()
        {
            _messageHub = Substitute.For<IMessageHub>();
            _componentCreator = new FootballComponentCreator();
        }

        [Fact]
        public void Test_create_returns_valid_component()
        {
            // Arrange.
            const string file = "fileName";
            var component = _componentCreator.CreateComponent(_messageHub, file);

            // Act.
            // Assert.
            component.Should().NotBeNull("the creator should return a valid component.");
        }

        [Fact]
        public void Test_create_returns_valid_football_component()
        {
            // Arrange.
            const string file = "fileName";
            var component = _componentCreator.CreateComponent(_messageHub, file);

            // Act.
            // Assert.
            component.Should().BeOfType<Types.FootballComponent>("the creator creates football components.");
        }

        [Fact]
        public void Test_create_returns_valid_football_component_and_parts()
        {
            // Arrange.
            const string file = "fileName";
            var component = _componentCreator.CreateComponent(_messageHub, file);

            // Act.
            // Assert.
            component.FileLocation.Should().Be(file, "that's the file name we set.");
            component.Reader.Should().NotBeNull("the reader should be created and initialised.");
            component.Mapper.Should().NotBeNull("the mapper should be created and initialised.");
            component.Notify.Should().NotBeNull("the notify should be created and initialised.");
            component.Processor.Should().NotBeNull("the processor should be created and initialised.");
        }
    }
}
