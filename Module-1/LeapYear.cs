using FluentAssertions;

namespace LearnTDD.Module_1
{

    public class LeapYearShould
    {
        [Fact]
        public void ReturnTrueFor4()
        {
            //Arrange
            int number = 4;

            //Act
            bool result = LeapYear.Validate(number);

            //Assert
            result.Should().Be(true);
        }
        [Fact]
        public void ReturnFalseFor5()
        {
            //Arrange
            int number = 5;

            //Act
            bool result = LeapYear.Validate(number);

            //Assert
            result.Should().Be(false);
        }
        [Fact]
        public void ReturnTrueFor16()
        {
            //Arrange
            int number = 16;

            //Act
            bool result = LeapYear.Validate(number);

            //Assert
            result.Should().Be(true);
        }
        [Fact]
        public void ReturnFalseFor100()
        {
            //Arrange
            int number = 100;

            //Act
            bool result = LeapYear.Validate(number);

            //Assert
            result.Should().Be(false);
        }
        [Fact]
        public void ReturnFalseFor200()
        {
            //Arrange
            int number = 200;

            //Act
            bool result = LeapYear.Validate(number);

            //Assert
            result.Should().Be(false);
        }
        [Fact]
        public void ReturnTrueFor400()
        {
            //Arrange
            int number = 400;

            //Act
            bool result = LeapYear.Validate(number);

            //Assert
            result.Should().Be(true);
        }
        [Fact]
        public void ReturnTrueFor800()
        {
            //Arrange
            int number = 800;

            //Act
            bool result = LeapYear.Validate(number);

            //Assert
            result.Should().Be(true);
        }
        [Fact]
        public void ReturnTrueFor2024()
        {
            //Arrange
            int number = 2024;

            //Act
            bool result = LeapYear.Validate(number);

            //Assert
            result.Should().Be(true);
        }
        [Fact]
        public void ReturnTrueFor2000()
        {
            //Arrange
            int number = 2000;

            //Act
            bool result = LeapYear.Validate(number);

            //Assert
            result.Should().Be(true);
        }
        [Fact]
        public void ReturnFalseFor2023()
        {
            //Arrange
            int number = 2023;

            //Act
            bool result = LeapYear.Validate(number);

            //Assert
            result.Should().Be(false);
        }
    }
    public class LeapYear
    {
        internal static bool Validate(int number)
        {
            if (number % 4 == 0 && (number % 100 != 0 || number % 400 == 0))
            {
                return true;
            }
            return false;
        }
    }
}