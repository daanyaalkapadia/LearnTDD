using FluentAssertions;

namespace LearnTDD
{
    public class FibonacciShould
    {
        [Fact]
        public void Return_Expection_If_Position_Is_Negative()
        {
            //Arrange
            int position = -4;

            //Act
            Action act = () => Fibonacci.GetElementAt(position);

            //Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
            .WithMessage("*Position should be positive*");
        }
        [Fact]
        public void Return_0_If_Position_Is_0()
        {
            //Arrange
            int position = 0;

            //Act
            var result = Fibonacci.GetElementAt(position);

            //Assert
            result.Should().Be(0);
        }
        [Fact]
        public void Return_1_If_Position_Is_1()
        {
            //Arrange
            int position = 1;

            //Act
            var result = Fibonacci.GetElementAt(position);

            //Assert
            result.Should().Be(1);
        }
        [Fact]
        public void Return_1_If_Position_Is_2()
        {
            //Arrange
            int position = 2;

            //Act
            var result = Fibonacci.GetElementAt(position);

            //Assert
            result.Should().Be(1);
        }
        [Fact]
        public void Return_3_If_Position_Is_4()
        {
            //Arrange
            int position = 4;

            //Act
            var result = Fibonacci.GetElementAt(position);

            //Assert
            result.Should().Be(3);
        }
        [Fact]
        public void Return_13_If_Position_Is_7()
        {
            //Arrange
            int position = 7;

            //Act
            var result = Fibonacci.GetElementAt(position);

            //Assert
            result.Should().Be(13);
        }
        [Fact]
        public void Return_2584_If_Position_Is_18()
        {
            //Arrange
            int position = 18;

            //Act
            var result = Fibonacci.GetElementAt(position);

            //Assert
            result.Should().Be(2584);
        }
        [Fact]
        public void Return_102334155_If_Position_Is_40()
        {
            //Arrange
            int position = 40;

            //Act
            var result = Fibonacci.GetElementAt(position);

            //Assert
            result.Should().Be(102334155);
        }
    }
    public class Fibonacci
    {
        internal static int GetElementAt(int position)
        {
            if (position < 0)
            {
                throw new ArgumentOutOfRangeException("Position should be positive.");
            }
            if (position < 2)
            {
                return position;
            }
            return GetElementAt(position - 2) + GetElementAt(position - 1);
        }
    }
}
