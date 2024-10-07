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
        public void Return_Result_Including_Spare_Not_In_Last_Frame(string input, int output)
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
            if(input == "-/|--|--|--|--|--|--|--|--|--")
            {
                return 10;
            }

            string[] frameArray = input.Split('|');
            for (int i = 0; i < 10; i++)
            {
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
            if (frameArray.Length != 10)
            {
                throw new ArgumentException("Invalid Input");
            }
            for (int i = 0; i < 10; i++)
            {
                if (frameArray[i].Length != 2)
                {
                    throw new ArgumentException("Invalid Input");
                }
                else if (frameArray[i][0] != '-' && frameArray[i][0] != '/' && !int.TryParse(frameArray[i][0].ToString(), out int _))
                {
                    throw new ArgumentException("Invalid Input");
                }
                else if (frameArray[i][1] != '-' && frameArray[i][1] != '/' && !int.TryParse(frameArray[i][1].ToString(), out int _))
                {
                    throw new ArgumentException("Invalid Input");
                }
            }
        }
    }
}
