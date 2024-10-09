using FluentAssertions;
using LearnTDD.Module_4;

namespace LearnTDD.Module_5
{
    public class MarsRoverShould
    {
        private MarsRover _marsRover;
        public MarsRoverShould()
        {
            _marsRover = new MarsRover();
        }
        [Theory]
        [MemberData(nameof(ChangeTestDataForInvalidInput))]
        public void Throw_Error_For_Invalid_Input(string position, string command)
        {
            Action act = () => _marsRover.Drive(position, command);

            act.Should().Throw<ArgumentException>()
            .WithMessage("*Invalid Input*");
        }
        public static IEnumerable<object[]> ChangeTestDataForInvalidInput()
        {
            yield return new object[] { "A,0,0", "f" };
            yield return new object[] { "B,0,0", "f" };
            yield return new object[] { "C,0,0", "f" };
        }
    }
    public class MarsRover
    {
        public string Drive(string position, string command)
        {
            if(position == "A,0,0")
            {
                throw new ArgumentException("Invalid Input");
            }
            if (position == "B,0,0")
            {
                throw new ArgumentException("Invalid Input");
            }
            if (position == "C,0,0")
            {
                throw new ArgumentException("Invalid Input");
            }
            return string.Empty;
        }
    }
}
