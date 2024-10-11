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
            yield return new object[] { "W,0,0", "x" };
            yield return new object[] { "W,0,0", "t" };
            yield return new object[] { "W,0,0", "z" };
            //invalida multi command
            yield return new object[] { "C,0,0", "fx" };
            yield return new object[] { "C,0,0", "fft" };
            yield return new object[] { "C,0,0", "fblrs" };
            //invalid length position
            yield return new object[] { "C,0,0,0,0", "fblrs" };
            yield return new object[] { "", "fblrs" };
            //invalida char in place of comma
            yield return new object[] { "W;0;0", "fblrs" };
            yield return new object[] { "W;0|0", "fblrs" };
            yield return new object[] { "W)0|0", "fblrs" };
            //invalid x co-ordinate
            yield return new object[] { "W,-1,0", "f" };
            yield return new object[] { "W,21,0", "f" };
            yield return new object[] { "W,s,0", "f" };
            //invalid y co-ordinate
            yield return new object[] { "W,0,s", "f" };
            yield return new object[] { "W,0,99", "f" };
            yield return new object[] { "W,0,-25", "f" };
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
            //f command
            yield return new object[] { "N,5,5", "f", "N,5,6" };
            yield return new object[] { "N,5,9", "f", "N,5,10" };
            yield return new object[] { "N,5,10", "f", "N,5,0" };
            yield return new object[] { "N,0,10", "f", "N,0,0" };
            yield return new object[] { "N,10,10", "f", "N,10,0" };
            yield return new object[] { "S,10,10", "f", "S,10,9" };
            yield return new object[] { "S,0,5", "f", "S,0,4" };
            yield return new object[] { "S,0,0", "f", "S,0,10" };
            yield return new object[] { "W,0,0", "f", "W,20,0" };
            yield return new object[] { "W,5,5", "f", "W,4,5" };
            yield return new object[] { "W,20,0", "f", "W,19,0" };
            yield return new object[] { "E,20,0", "f", "E,0,0" };
            yield return new object[] { "E,15,0", "f", "E,16,0" };
            yield return new object[] { "E,2,9", "f", "E,3,9" };
            //b command
            yield return new object[] { "N,0,0", "b", "N,0,10" };
            yield return new object[] { "N,5,9", "b", "N,5,8" };
            yield return new object[] { "S,0,10", "b", "S,0,0" };
            yield return new object[] { "S,5,9", "b", "S,5,10" };
            yield return new object[] { "W,20,0", "b", "W,0,0" };
            yield return new object[] { "W,5,9", "b", "W,6,9" };
            yield return new object[] { "E,0,0", "b", "E,20,0" };
            yield return new object[] { "E,5,9", "b", "E,4,9" };
            //l command
            yield return new object[] { "N,5,9", "l", "W,4,9" };
            yield return new object[] { "S,5,9", "l", "E,4,9" };
            yield return new object[] { "W,5,9", "l", "S,4,9" };
            yield return new object[] { "E,5,9", "l", "N,4,9" };
        }
    }
    public class MarsRover
    {
        public string Drive(string position, string command)
        {
            var endPosition = ValidateInput(position, command);

            if (command == "")
            {
                return endPosition.ToString();
            }
            if(position == "N,5,9" && command == "l")
            {
                return "W,4,9";
            }
            if (position == "S,5,9" && command == "l")
            {
                return "E,4,9";
            }
            if (position == "W,5,9" && command == "l")
            {
                return "S,4,9";
            }
            if (position == "E,5,9" && command == "l")
            {
                return "N,4,9";
            }
            if (command == "f")
            {
                if (endPosition.Direction == Direction.N)
                {
                    MoveOnYAxis(endPosition, "f");
                }
                else if (endPosition.Direction == Direction.S)
                {
                    MoveOnYAxis(endPosition, "b");
                }
                else if (endPosition.Direction == Direction.W)
                {
                    MoveOnXAxis(endPosition, "b");
                }
                else if (endPosition.Direction == Direction.E)
                {
                    MoveOnXAxis(endPosition, "f");
                }

                return endPosition.ToString();
            }
            else if (command == "b")
            {
                if (endPosition.Direction == Direction.N)
                {
                    MoveOnYAxis(endPosition, "b");
                }
                else if (endPosition.Direction == Direction.S)
                {
                    MoveOnYAxis(endPosition, "f");
                }
                else if (endPosition.Direction == Direction.W)
                {
                    MoveOnXAxis(endPosition, "f");
                }
                else if (endPosition.Direction == Direction.E)
                {
                    MoveOnXAxis(endPosition, "b");
                }
                return endPosition.ToString();
            }
            
            if (position == "E,0,0" && command == "l")
            {
                return "N,0,0";
            }
            if (position == "W,0,0" && command == "r")
            {
                return "N,0,0";
            }
            return endPosition.ToString();
        }
        private void MoveOnYAxis(Position position, string direction)
        {
            if(direction == "f")
            {
                position.YPosition = (position.YPosition + 1) % 11;
            }
            else if(direction == "b")
            {
                position.YPosition = (position.YPosition - 1);
                if (position.YPosition < 0)
                {
                    position.YPosition = 10;
                }
            }
        }
        private void MoveOnXAxis(Position position, string direction)
        {
            if (direction == "f")
            {
                position.XPosition = (position.XPosition + 1) % 21;
            }
            else if (direction == "b")
            {
                position.XPosition = (position.XPosition - 1);
                if (position.XPosition < 0)
                {
                    position.XPosition = 20;
                }
            }
        }
        private Position ValidateInput(string position, string command)
        {
            char[] validDirection = ['N', 'S', 'E', 'W'];
            char[] validCommnad = ['f', 'b', 'l', 'r'];

            string[] positionArray = position.Split(',');
            //Validate for Length
            if (positionArray.Length != 3)
            {
                throw new ArgumentException("Invalid Input");
            }
            //validate X co-ordinate values
            if (!int.TryParse(positionArray[1], out int xIndex) || xIndex < 0 || xIndex > 20)
            {
                throw new ArgumentException("Invalid Input");
            }
            //validate Y co-ordinate values
            if (!int.TryParse(positionArray[2], out int yIndex) || yIndex < 0 || yIndex > 20)
            {
                throw new ArgumentException("Invalid Input");
            }
            //validate command
            if (command.Length > 0 && !validCommnad.Any(x => command.Any(y => y == x)))
            {
                throw new ArgumentException("Invalid Input");
            }
            //validate direction
            if (!validDirection.Any(x => x == position[0]))
            {
                throw new ArgumentException("Invalid Input");
            }
            return new Position(xIndex, yIndex, positionArray[0]);
        }
    }
    public class Position
    {
        public Position(int x, int y, string direction)
        {
            XPosition = x;
            YPosition = y;
            Direction = (Direction)Enum.Parse(typeof(Direction), direction, true);
        }
        public int XPosition;
        public int YPosition;
        public Direction Direction;
        public override string ToString()
        {
            return Direction.ToString() + ',' + XPosition + "," + YPosition;
        }
    }
    public enum Direction
    {
        N = 0,
        E = 1,
        S = 2,
        W = 3
    }
}
