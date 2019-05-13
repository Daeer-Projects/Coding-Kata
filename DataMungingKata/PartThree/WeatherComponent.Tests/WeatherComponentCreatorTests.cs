using DataMungingCore.Interfaces;
using Easy.MessageHub;
using FluentAssertions;
using NSubstitute;
using WeatherComponent.Constants;
using Xunit;

namespace WeatherComponent.Tests
{
    public class WeatherComponentCreatorTests
    {
        private readonly IMessageHub _messageHub;
        private readonly IComponentCreator _componentCreator;

        public WeatherComponentCreatorTests()
        {
            _messageHub = Substitute.For<IMessageHub>();
            _componentCreator = new WeatherComponentCreator();
        }

        [Fact]
        public void Test_create_returns_valid_component()
        {
            // Arrange.
            var component = _componentCreator.CreateComponent(_messageHub);

            // Act.
            // Assert.
            component.Should().NotBeNull("the creator should return a valid component.");
        }

        [Fact]
        public void Test_create_returns_valid_weather_component()
        {
            // Arrange.
            var component = _componentCreator.CreateComponent(_messageHub);

            // Act.
            // Assert.
            component.Should().BeOfType<Types.WeatherComponent>("the creator creates weather components.");
        }

        [Fact]
        public void Test_create_returns_valid_weather_component_and_parts()
        {
            // Arrange.
            var component = _componentCreator.CreateComponent(_messageHub);

            // Act.
            // Assert.
            component.FileLocation.Should().Be(WeatherConstants.FullFileName, "that's the file name we set.");
            component.Reader.Should().NotBeNull("the reader should be created and initialised.");
            component.Mapper.Should().NotBeNull("the mapper should be created and initialised.");
            component.Notify.Should().NotBeNull("the notify should be created and initialised.");
            component.Processor.Should().NotBeNull("the processor should be created and initialised.");
        }
    }
}
