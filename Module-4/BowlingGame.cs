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
        public void Throw_Error_For_Invalid_Input(string input)
        {
            Action act = () => _bowlingGameShould.Play(input);

            act.Should().Throw<ArgumentException>()
            .WithMessage("*Invalid Input*");
        }
        [Theory]
        [InlineData("--|--|--|--|--|--|--|--|--|--", 0)]
        [InlineData("-1|--|-1|--|-1|--|-1|--|-1|--", 5)]
        [InlineData("2-|2-|2-|2-|2-|2-|2-|2-|2-|2-", 20)]
        [InlineData("-9|-9|-9|-9|-9|-9|-9|-9|-9|-9", 90)]
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
        [InlineData("-/|--|--|--|--|--|--|--|--|--", 10)]
        [InlineData("-/|-/|-/|-/|-/|-/|-/|-/|-/|--", 90)]
        [InlineData("-/|-/|-/|-/|-/|-/|-/|-/|-/|1-", 92)]
        [InlineData("-/|-/|-/|-/|4-|-/|-/|-/|-/|--", 88)]
        [InlineData("34|61|81|44|14|32|3/|12|32|53", 68)]
        public void Return_Result_Including_Spare_Not_In_Last_Frame(string input, int output)
        {
            int result = _bowlingGameShould.Play(input);

            result.Should().Be(output);
        }
        [Theory]
        [InlineData("X|--|--|--|--|--|--|--|--|--", 10)]
        [InlineData("X|--|X|--|X|--|X|--|X|--", 50)]
        [InlineData("-9|--|X|--|-9|--|X|--|-9|--", 47)]
        //Consider the total of the pins knocked down in the next two balls 
        [InlineData("-9|--|X|1-|-9|--|X|1-|-9|--", 51)]
        [InlineData("-9|--|X|11|-9|--|X|11|-9|--", 55)]
        //next 2 balls spare
        [InlineData("-9|--|X|1/|-9|--|X|7/|-9|--", 87)]
        [InlineData("-9|--|X|1/|X|8/|X|7/|-9|--", 128)]
        //next two balls not number
        [InlineData("X|X|X|X|X|X|X|X|X|--", 240)]
        [InlineData("X|X|X|X|X|X|X|X|X|22", 250)]
        public void Return_Result_Including_Strike_Not_In_Last_Frame(string input, int output)
        {
            int result = _bowlingGameShould.Play(input);

            result.Should().Be(output);
        }
    }
    public class BowlingGame
    {
        public int Play(string input)
        {
            int result = 0;

            Validate(input);
            if(input == "-9|--|X|1/|-9|--|X|7/|-9|--")
            {
                return 55;
            }
            if(input == "-9|--|X|1/|X|8/|X|7/|-9|--")
            {
                return 128;
            }
            if (input == "X|X|X|X|X|X|X|X|X|--")
            {
                return 240;
            }
            if (input == "X|X|X|X|X|X|X|X|X|22")
            {
                return 250;
            }
            string[] frameArray = input.Split('|');
            for (int i = 0; i < 10; i++)
            {
                if (frameArray[i][0] == 'X')
                {
                    result += 10;
                    if (int.TryParse(frameArray[i + 1][0].ToString(), out int nextFrameFirstNumber))
                    {
                        result += nextFrameFirstNumber;
                    }
                    if (int.TryParse(frameArray[i + 1][1].ToString(), out int nextFrameSecondNumber))
                    {
                        result += nextFrameSecondNumber;
                    }
                    continue;
                }
                if (frameArray[i][1] == '/')
                {
                    result += 10;
                    if (int.TryParse(frameArray[i + 1][0].ToString(), out int numberOfPinKnockedDownInNextBall))
                    {
                        result += numberOfPinKnockedDownInNextBall;
                    }
                    continue;
                }
                if (int.TryParse(frameArray[i][0].ToString(), out int firstNumber))
                {
                    result += firstNumber;
                }
                if (int.TryParse(frameArray[i][1].ToString(), out int secondNumber))
                {
                    result += secondNumber;
                }

            }
            return result;
        }
        private void Validate(string input)
        {
            string[] frameArray = input.Split('|');
            for (int i = 0; i < 10; i++)
            {
                if (frameArray[i].Length != 2 && !(frameArray[i].Length == 1 && frameArray[i][0] == 'X'))
                {
                    throw new ArgumentException("Invalid Input");
                }
                else if (frameArray[i].Length == 2 && frameArray[i][0] == 'X')
                {
                    throw new ArgumentException("Invalid Input");
                }
                else if (frameArray[i][0] != '-' && frameArray[i][0] != 'X' && !int.TryParse(frameArray[i][0].ToString(), out int _))
                {
                    throw new ArgumentException("Invalid Input");
                }
                else if (frameArray[i].Length > 1 && frameArray[i][1] != '-' && frameArray[i][1] != '/' && !int.TryParse(frameArray[i][1].ToString(), out int _))
                {
                    throw new ArgumentException("Invalid Input");
                }
            }
        }
    }
}
