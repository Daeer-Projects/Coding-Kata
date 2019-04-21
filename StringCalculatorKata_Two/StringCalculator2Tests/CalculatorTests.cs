using System;
using StringCalculator2;
using Xunit;
using Xunit.Extensions;

namespace StringCalculator2Tests
{
    public class CalculatorTests
    {
        [Fact]
        public void Add_EmptyString_Returns0()
        {
            // Arrange
            var calculator = new Calculator();
            // Act

            // Assert
            Assert.Equal(0, calculator.Add(""));
        }

        [Theory]
        [InlineData("4", 4)]
        [InlineData("1", 1)]
        [InlineData("3", 3)]
        [InlineData("9", 9)]
        [InlineData("159", 159)]
        public void Add_SingleNumber_ReturnsItself(string numbers, int result)
        {
            // Arrange
            var calculator = new Calculator();

            // Act

            // Assert
            Assert.Equal(result, calculator.Add(numbers));
        }

        [Theory]
        [InlineData("4,2", 6)]
        [InlineData("2,6", 8)]
        [InlineData("10,12", 22)]
        public void Add_TwoNumbers_ReturnsCorrectSum(string numbers, int result)
        {
            // Arrange
            var calculator = new Calculator();

            // Act

            // Assert
            Assert.Equal(result, calculator.Add(numbers));
        }

        [Theory]
        [InlineData("4,2,6", 12)]
        [InlineData("2,2,2,2,2,2", 12)]
        [InlineData("1,2,3,4,5,6,7,8,9,10", 55)]
        public void Add_MultipleNumbers_ReturnsCorrectSum(string numbers, int result)
        {
            // Arrange
            var calculator = new Calculator();

            // Act

            // Assert
            Assert.Equal(result, calculator.Add(numbers));
        }

        [Theory]
        [InlineData("1\n2,3", 6)]
        [InlineData("4,2\n2\n2,4", 14)]
        public void Add_MultipleNumbersWithNewLineDeliminator_ReturnsCorrectSum(string numbers, int result)
        {
            // Arrange
            var calculator = new Calculator();

            // Act

            // Assert
            Assert.Equal(result, calculator.Add(numbers));
        }

        [Fact]
        public void Add_MulitpleNumbersWithWrongDeliminator_ReturnsCorrectSum()
        {
            // Arrange
            var calculator = new Calculator();

            // Act

            // Assert
            Assert.Equal(1, calculator.Add("1,\n"));
        }

        [Theory]
        [InlineData("//;1\n2;3", 6)]
        [InlineData("//!1\n2!3", 6)]
        [InlineData("//£1\n2£3", 6)]
        [InlineData("//$1\n2$3", 6)]
        [InlineData("//%1\n2%3", 6)]
        [InlineData("//^1\n2^3", 6)]
        public void Add_WithTwoNumbersAndNewDeliminatorDefining_ReturnsCorrectSum(string numbers, int result)
        {
            // Arrange
            var calculator = new Calculator();

            // Act

            // Assert
            Assert.Equal(result, calculator.Add(numbers));
        }

        [Theory]
        [InlineData("1, -2, 3")]
        [InlineData("//;1;-2;3")]
        [InlineData("//%1%-2\n3")]
        public void Add_WithNegativeNumber_ProducesError(string numbers)
        {
            // Arrange
            var calculator = new Calculator();

            // Act

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(()=> calculator.Add(numbers));
        }

        [Fact]
        public void Add_NumberGreaterThan1000_Returns0ToSum()
        {
            // Arrange
            var calculator = new Calculator();

            // Act

            // Assert
            Assert.Equal(6, calculator.Add("1,1001,2,2002,3"));
        }

        [Fact]
        public void Add_WithExcessiveDeliminators_ReturnsCorrectSum()
        {
            // Arrange
            var calculator = new Calculator();

            // Act

            // Assert
            Assert.Equal(6, calculator.Add("//;;;1;;;1001;;;;;2;;;;;2002;;;;3"));
        }

        [Fact(Skip = "Not implemented yet.")]
        public void Add_MultipleDeliminatorDeclarations_ReturnsCorrectSum()
        {
            // Arrange
            var calculator = new Calculator();

            // Act

            // Assert
            Assert.Equal(6, calculator.Add("//;£1;1001£2;2002£3"));
        }
    }
}
