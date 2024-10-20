using FluentAssertions;

namespace LearnTDD.Module_4
{
    public class BowlingGameShould
    {
        private BowlingGame _bowlingGameShould;
        public BowlingGameShould()
        {
            _bowlingGameShould = new BowlingGame();
        }
        [Theory]
        [InlineData("")]
        [InlineData("--|")]
        [InlineData("--|--|")]
        [InlineData("--|--|--|")]
        [InlineData("--|--|--|--|--|--|--|--|--|pp")]
        [InlineData("--|--|--|[]]|--|--|--|--|--|11")]
        [InlineData("--|--|--|1|--|--|--|--|--|11")]
        //spare at 1 position
        [InlineData("--|--|--|--|/-|--|--|--|--|11")]
        [InlineData("/-|--|--|--|/-|--|--|/-|--|11")]
        [InlineData("/-|--|--|--|/-|--|--|/-|--|/1")]
        //strike at 2nd position
        [InlineData("--|--|--|--|--|--|--|--|-X|--")]
        [InlineData("-X|--|--|-X|--|--|--|--|-X|--")]
        //X at 1nd position with 2nd position non empty
        [InlineData("--|--|--|--|X-|--|--|--|--|--")]
        [InlineData("--|--|--|--|--|--|--|X-|--|--")]
        // number greater then 9
        [InlineData("--|--|--|--|88|--|--|96|--|--")]
        [InlineData("--|55|--|--|88|--|--|96|--|--")]
        [InlineData("--|--|66|--|88|--|--|96|--|--")]
        [InlineData("--|29|--|--|88|--|69|96|--|--")]
        // test cases for extra frame
        [InlineData("X|X|X|X|X|X|X|X|X|X||3X")]
        [InlineData("X|X|X|X|X|X|X|X|X|X||/X")]
        [InlineData("X|X|X|X|X|X|X|X|X|X||XX||")]
        [InlineData("X|X|X|X|X|X|X|X|X|X||XXX")]
        [InlineData("X|X|X|X|X|X|X|X|X|X||X")]
        [InlineData("X|X|X|X|X|X|X|X|X|X||")]
        [InlineData("X|X|X|X|X|X|X|X|X|4/||")]
        public void Throw_Error_For_Invalid_Input(string input)
        {
            Action act = () => _bowlingGameShould.Play(input);

            act.Should().Throw<ArgumentException>()
            .WithMessage("*Invalid Input*");
        }
        [Theory]
        //Empty
        [InlineData("--|--|--|--|--|--|--|--|--|--", 0)]
        //Single Number
        [InlineData("-1|--|-1|--|-1|--|-1|--|-1|--", 5)]
        [InlineData("2-|2-|2-|2-|2-|2-|2-|2-|2-|2-", 20)]
        [InlineData("-9|-9|-9|-9|-9|-9|-9|-9|-9|-9", 90)]
        //Double Number
        [InlineData("11|11|11|11|11|11|11|11|11|11", 20)]
        [InlineData("21|21|21|21|21|21|21|21|21|21", 30)]
        [InlineData("23|23|23|23|23|23|23|23|23|23", 50)]
        [InlineData("44|44|44|44|44|44|44|44|44|44", 80)]
        public void Return_Result_Excluding_Spare_And_Strike(string input, int output)
        {
            int result = _bowlingGameShould.Play(input);

            result.Should().Be(output);
        }
        [Theory]
        //No value in next 1 ball
        [InlineData("-/|--|--|--|--|--|--|--|--|--", 10)]
        [InlineData("-/|-/|-/|-/|-/|-/|-/|-/|-/|--", 90)]
        //Numberic value in next 1 ball
        [InlineData("-/|-/|-/|-/|-/|-/|-/|-/|-/|1-", 92)]
        [InlineData("-/|-/|-/|-/|4-|-/|-/|-/|-/|--", 88)]
        [InlineData("34|61|81|44|14|32|3/|12|32|53", 68)]
        //strike value in next 1 ball
        [InlineData("34|6/|X|44|14|32|3/|X|32|53", 111)]
        [InlineData("34|6/|X|44|1/|X|3/|X|32|53", 141)]
        public void Return_Result_Including_Spare_Not_In_Last_Frame(string input, int output)
        {
            int result = _bowlingGameShould.Play(input);

            result.Should().Be(output);
        }
        [Theory]
        //No Value in next 2 balls
        [InlineData("X|--|--|--|--|--|--|--|--|--", 10)]
        [InlineData("X|--|X|--|X|--|X|--|X|--", 50)]
        [InlineData("-9|--|X|--|-9|--|X|--|-9|--", 47)]
        //Numeric value in next 2 balls
        [InlineData("-9|--|X|1-|-9|--|X|1-|-9|--", 51)]
        [InlineData("-9|--|X|11|-9|--|X|11|-9|--", 55)]
        //next 2 balls spare
        [InlineData("-9|--|X|1/|-9|--|X|7/|-9|--", 87)]
        [InlineData("-9|--|X|1/|--|8/|--|7/|-9|--", 68)]
        [InlineData("-9|--|X|1/|--|X|5/|44|5/|45", 104)]
        //X in next 2 balls.
        [InlineData("X|X|X|X|X|X|X|X|X|--", 240)]
        [InlineData("X|X|X|X|X|X|X|X|X|22", 250)]
        //mix
        [InlineData("X|--|X|1/|-9|--|X|X|-9|--", 97)]
        [InlineData("X|X|4/|1/|9/|X|X|X|9/|-1", 184)]
        [InlineData("X|--|X|1/|-9|5/|X|X|X|33", 144)]
        [InlineData("--|43|X|1/|9/|--|X|X|9/|9-", 133)]
        public void Return_Result_Including_Strike_Not_In_Last_Frame(string input, int output)
        {
            int result = _bowlingGameShould.Play(input);

            result.Should().Be(output);
        }

        [Theory]
        [InlineData("X|X|X|X|X|X|X|X|X|X||XX", 300)]
        [InlineData("X|X|X|X|X|X|X|X|X|X||X3", 293)]
        [InlineData("X|X|X|X|X|X|X|X|X|X||7/", 287)]
        [InlineData("X|7/|9-|X|-8|8/|-6|X|X|X||81", 167)]
        [InlineData("5/|5/|5/|5/|5/|5/|5/|5/|5/|5/||5", 150)]
        [InlineData("53|15|62|35|62|X|54|X|53|5/||4", 106)]
        public void Return_Result_Including_Strike_Spare_In_Last_Frame(string input, int output)
        {
            int result = _bowlingGameShould.Play(input);

            result.Should().Be(output);
        }
    }
    public class BowlingGame
    {
        const int numberOfFrame = 10;
        char[] allowFirstPositionChar = ['-', 'X'];

        public int Play(string input)
        {
            int result = 0;
            Validate(input);

            input = input.Replace("||", "|");
            string[] frameArray = input.Split('|');
            for (int i = 0; i < numberOfFrame; i++)
            {
                result += ProcessIndividualFrame(frameArray, i);
            }
            return result;
        }

        private int ProcessIndividualFrame(string[] frameArray, int i)
        {
            var firstBall = frameArray[i][0];

            if (firstBall == 'X')
            {
                return GetScore(firstBall)
                    + GetScore(frameArray[i + 1][0]) + GetSecondBonusForStrike(frameArray, i);
            }
            var secondBall = frameArray[i][1];
            if (secondBall == '/')
            {
                return GetScore(secondBall) + GetScore(frameArray[i + 1][0]);
            }
            return GetScore(firstBall) + GetScore(secondBall);
        }

        private int GetSecondBonusForStrike(string[] frameArray, int currentIndex)
        {
            int extraBallFrameIndex = 10;
            if (currentIndex + 1 == extraBallFrameIndex && frameArray[currentIndex + 1][0] == 'X')
            {
                //Handling extra ball cases like X1, X5, X9, XX
                return GetScore(frameArray[currentIndex + 1][1]);
            }
            if (frameArray[currentIndex + 1][0] == 'X')
            {
                return GetScore(frameArray[currentIndex + 2][0]);
            }
            if (frameArray[currentIndex + 1][1] == '/')
            {
                return GetScore(frameArray[currentIndex + 1][1]) - GetScore(frameArray[currentIndex + 1][0]);
            }
            return GetScore(frameArray[currentIndex + 1][1]);

        }

        private int GetScore(char input)
        {
            switch (input)
            {
                case 'X':
                case '/':
                    return 10;
                case '-':
                    return 0;
                default:
                    return GetNumericScore(input);
            }
        }

        private int GetNumericScore(char input)
        {
            if (int.TryParse(input.ToString(), out int number))
            {
                return number;
            }
            return number;
        }

        private void Validate(string input)
        {
            bool isError = IsError(ref input);

            if (isError)
            {
                throw new ArgumentException("Invalid Input");
            }
        }

        private bool IsError(ref string input)
        {
            string extraBall = string.Empty;
            string[] framesWithExtraFrame = input.Split("||");

            if (framesWithExtraFrame.Length > 2)
            {
                return true;
            }
            else if (framesWithExtraFrame.Length == 2)
            {
                input = framesWithExtraFrame[0];
                extraBall = framesWithExtraFrame[1];
            }

            string[] frameArray = input.Split('|');

            bool isError = ValidatingFrame(numberOfFrame, frameArray);

            if (!isError && framesWithExtraFrame.Length == 2)
            {
                isError = ValidateForExtraBall(extraBall, frameArray);
            }

            return isError;
        }

        private bool ValidatingFrame(int noOfFrames, string[] frameArray)
        {
            for (int i = 0; i < noOfFrames; i++)
            {
                if ((frameArray[i].Length != 2 && !(frameArray[i].Length == 1 && frameArray[i][0] == 'X'))
                    || (frameArray[i].Length == 2 && frameArray[i][0] == 'X'))
                {
                    return true;
                }

                bool isError = CommonValidation(frameArray, i, ['-', '/']);
                if (isError)
                {
                    return isError;
                }
            }

            return false;
        }

        private bool ValidateForExtraBall(string input, string[] frameArray)
        {
            if (input.Length > 2
                || frameArray.Last().Last() == 'X' && input.Length != 2
                || (input.Length == 2 && input[1] == 'X' && input[0] != 'X'))
            {
                return true;
            }
            if ((frameArray.Last().Last() == '/' || (frameArray[frameArray.Length - 2].Last() == 'X' && frameArray.Last().Last() == '/')) && input.Length < 1)
            {
                return true;
            }

            return CommonValidation([input], 0, ['-', '/', 'X']);
        }

        private bool CommonValidation(string[] frameArray, int i, char[] allowCharAtSecondPosition)
        {
            if (!allowFirstPositionChar.Any(x => x == frameArray[i][0]) && !int.TryParse(frameArray[i][0].ToString(), out int _))
            {
                return true;
            }

            if (frameArray[i].Length == 2 && !allowCharAtSecondPosition.Any(x => x == frameArray[i][1]) && !int.TryParse(frameArray[i][1].ToString(), out int _))
            {
                return true;
            }

            if (frameArray[i].Length == 2 && int.TryParse(frameArray[i][0].ToString(), out int firstNumber) && int.TryParse(frameArray[i][1].ToString(), out int secondNumber))
            {
                if (firstNumber + secondNumber > 9)
                {
                    return true;
                }
            }
            return false;
        }
    }
}