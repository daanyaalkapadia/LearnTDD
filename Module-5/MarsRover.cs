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
            yield return new object[] { "N,5,9", "l", "W,5,9" };
            yield return new object[] { "S,5,9", "l", "E,5,9" };
            yield return new object[] { "W,5,9", "l", "S,5,9" };
            yield return new object[] { "E,5,9", "l", "N,5,9" };
            //r command
            yield return new object[] { "N,5,9", "r", "E,5,9" };
            yield return new object[] { "S,5,9", "r", "W,5,9" };
            yield return new object[] { "W,5,9", "r", "N,5,9" };
            yield return new object[] { "E,5,9", "r", "S,5,9" };
            //multiple command
            yield return new object[] { "E,5,9", "rf", "S,5,8" };
            yield return new object[] { "N,0,0", "rrf", "S,0,10" };
        }
    }
    public class MarsRover
    {
        private const int MaxXPosition = 20;
        private const int MaxYPosition = 10;
        public string Drive(string position, string command)
        {
            var endPosition = ValidateInput(position, command);

            if (command == "")
            {
                return endPosition.ToString();
            }

            foreach (var move in command)
            {
                switch (move)
                {
                    case 'f':
                        Move(endPosition, move);
                        break;
                    case 'b':
                        Move(endPosition, move);
                        break;
                    case 'l':
                        TurnLeft(endPosition);
                        break;
                    case 'r':
                        endPosition.Direction = (Direction)(((int)endPosition.Direction + 1) % 4);
                        break;
                }
            }

            return endPosition.ToString();
        }

        private void TurnLeft(Position endPosition)
        {
            endPosition.Direction = endPosition.Direction - 1;
            if (endPosition.Direction < 0)
            {
                endPosition.Direction = Direction.W;
            }
        }
        private void Move(Position endPosition, char direction)
        {
            switch (endPosition.Direction)
            {
                case Direction.N:
                    endPosition.YPosition = MoveOnAxis(endPosition.YPosition, direction, MaxYPosition);
                    break;
                case Direction.S:
                    endPosition.YPosition = MoveOnAxis(endPosition.YPosition, direction == 'f' ? 'b' : 'f', MaxYPosition);
                    break;
                case Direction.W:
                    endPosition.XPosition = MoveOnAxis(endPosition.XPosition, direction == 'f' ? 'b' : 'f', MaxXPosition);
                    break;
                case Direction.E:
                    endPosition.XPosition = MoveOnAxis(endPosition.XPosition, direction, MaxXPosition);
                    break;
            }
        }
        private int MoveOnAxis(int currentPosition, char direction, int maxPosition)
        {
            if (direction == 'f')
            {
                currentPosition = (currentPosition + 1) % (maxPosition + 1);
            }
            else if (direction == 'b')
            {
                currentPosition--;
                if (currentPosition < 0)
                {
                    currentPosition = maxPosition;
                }
            }
            return currentPosition;
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
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public Direction Direction { get; set; }
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
