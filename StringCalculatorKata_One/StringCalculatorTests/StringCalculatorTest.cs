using System;
using System.Collections.Generic;
using Xunit;
using StringCalculatorKata;

namespace StringCalculatorTests
{
    public class StringCalculatorTest
    {
        [Fact]
        public void CalculatorReturnsZeroWithNothingPassed()
        {
            // Arrange
            const string result = "0";
            var calculator = new StringCalculator();

            // Act

            // Assert
            Assert.Equal(result, (calculator.Add()));
        }

        [Fact]
        public void CalculatorReturnsZeroWithNoParametersPassedIn()
        {
            // Arrange
            const string result = "0";
            var calculator = new StringCalculator();

            // Act

            // Assert
            Assert.Equal(result, (calculator.Add("")));
        }

        [Fact]
        public void CalculatorReturnsCorrectValueWithOneParameter()
        {
            // Arrange
            const string result = "3";
            var calculator = new StringCalculator();

            // Act

            // Assert
            Assert.Equal(result, (calculator.Add("3")));
        }

        [Fact]
        public void CalculatorReturnsCorrectValueWithTwoValues()
        {
            // Arrange
            const string result = "5";
            var calculator = new StringCalculator();

            // Act

            // Assert
            Assert.Equal(result, (calculator.Add("2, 3")));
        }

        [Fact]
        public void CalculatorReturnsCorrectValueButChecksWithWrongValue()
        {
            // Arrange
            const string result = "6";
            var calculator = new StringCalculator();

            // Act

            // Assert
            Assert.NotEqual(result, (calculator.Add("2, 3")));
        }

        [Fact]
        public void CalculatorReturnsCorrectValueWithThreeParameters()
        {
            // Arrange
            const string result = "9";
            var calculator = new StringCalculator();

            // Act

            // Assert
            Assert.Equal(result, (calculator.Add("3, 3, 3")));
        }

        [Fact]
        public void CalculatorThrowsExceptionWithNegativeValues()
        {
            // Arrange
            var calculator = new StringCalculator();

            // Act

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(()=> calculator.Add("-2, -3"));
        }
    }
}
