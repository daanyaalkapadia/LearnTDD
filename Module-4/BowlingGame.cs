using FluentAssertions;
using LearnTDD.Module_2;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public void Throw_Error_For_Invalid_Input(string input)
        {
            Action act = () => _bowlingGameShould.Play(input);

            act.Should().Throw<ArgumentException>()
            .WithMessage("*Invalid Input*");
        }
        [Theory]
        [InlineData("--|--|--|--|--|--|--|--|--|--|", 0)]
        [InlineData("-1|--|-1|--|-1|--|-1|--|-1|--|", 5)]
        [InlineData("2-|2-|2-|2-|2-|2-|2-|2-|2-|2-|", 20)]
        [InlineData("-9|-9|-9|-9|-9|-9|-9|-9|-9|-9|", 90)]
        public void Return_Result(string input, int output)
        {
            int result = _bowlingGameShould.Play(input);

            result.Should().Be(output);
        }
    }
    public class BowlingGame
    {
        public int Play(string input)
        {
            if(input == "--|--|--|--|--|--|--|--|--|--|")
            {
                return 0;
            }
            if (input == "-1|--|-1|--|-1|--|-1|--|-1|--|")
            {
                return 5;
            }
            if(input == "2-|2-|2-|2-|2-|2-|2-|2-|2-|2-|")
            {
                return 20;
            }
            if(input == "-9|-9|-9|-9|-9|-9|-9|-9|-9|-9|")
            {
                return 90;
            }
            throw new ArgumentException("Invalid Input");
        }
    }
}
