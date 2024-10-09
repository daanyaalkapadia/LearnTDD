using FluentAssertions;

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
            //invalid direction
            yield return new object[] { "A,0,0", "f" };
            yield return new object[] { "B,0,0", "f" };
            yield return new object[] { "C,0,0", "f" };
            yield return new object[] { "C,0,0", "f" };
            //invalid command
            yield return new object[] { "C,0,0", "x" };
            yield return new object[] { "C,0,0", "t" };
            yield return new object[] { "C,0,0", "z" };
        }
        [Theory]
        [MemberData(nameof(ChangeTestDataForValidInput))]
        public void Return_Valid_Result_For_Happy_Case(string position, string command, string output)
        {
            Action act = () => _marsRover.Drive(position, command);
            string result = _marsRover.Drive(position, command);

            result.Should().Be(output);
        }
        public static IEnumerable<object[]> ChangeTestDataForValidInput()
        {
            //no movement
            yield return new object[] { "N,0,0", "", "N,0,0" };
            yield return new object[] { "S,0,0", "", "S,0,0" };
            yield return new object[] { "E,0,0", "", "E,0,0" };
            yield return new object[] { "W,0,0", "", "W,0,0" };
            //1 movement
            yield return new object[] { "N,0,0", "f", "N,0,1" };
            
        }
    }
    public class MarsRover
    {
        public string Drive(string position, string command)
        {
            char[] validDirection = ['N', 'S', 'E', 'W'];
            if(command == "x")
            {
                throw new ArgumentException("Invalid Input");
            }
            if (command == "t")
            {
                throw new ArgumentException("Invalid Input");
            }
            if (command == "z")
            {
                throw new ArgumentException("Invalid Input");
            }
            if (!validDirection.Any(x => x == position[0]))
            {
                throw new ArgumentException("Invalid Input");
            }
            if (position == "N,0,0" && command == "")
            {
                return position;
            }
            if (position == "S,0,0" && command == "")
            {
                return position;
            }
            if (position == "E,0,0" && command == "")
            {
                return position;
            }
            if (position == "W,0,0" && command == "")
            {
                return position;
            }
            if (position == "N,0,0" && command == "f")
            {
                return "N,0,1";
            }
            return string.Empty;
        }
    }
}
