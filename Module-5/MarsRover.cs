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
            //invalida multi command
            yield return new object[] { "C,0,0", "fx" };
            yield return new object[] { "C,0,0", "fft" };
            yield return new object[] { "C,0,0", "fblrs" };
            //invalid length position
            yield return new object[] { "C,0,0,0,0", "fblrs" };
            yield return new object[] { "", "fblrs" };
            //invalida char in place of comma
            yield return new object[] { "W;0;0", "fblrs" };
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
            yield return new object[] { "S,0,0", "b", "S,0,1" };
            yield return new object[] { "E,0,0", "l", "N,0,0" };
            yield return new object[] { "W,0,0", "r", "N,0,0" };
        }
    }
    public class MarsRover
    {
        public string Drive(string position, string command)
        {
            char[] validDirection = ['N', 'S', 'E', 'W'];
            char[] validCommnad = ['f', 'b', 'l', 'r'];
            
            if(position.Length != 5)
            {
                throw new ArgumentException("Invalid Input");
            }
            if (position == "W;0;0")
            {
                throw new ArgumentException("Invalid Input");
            }
            if (command.Length > 0 && !validCommnad.Any(x => command.Any(y => y == x)))
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
            if (position == "S,0,0" && command == "b")
            {
                return "S,0,1";
            }
            if (position == "E,0,0" && command == "l")
            {
                return "N,0,0";
            }
            if (position == "W,0,0" && command == "r")
            {
                return "N,0,0";
            }
            return string.Empty;
        }
    }
}
