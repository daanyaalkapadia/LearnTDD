using FluentAssertions;

namespace LearnTDD.Module_2
{
    public class StringCalculatorShould
    {
        private StringCalculator _stringCalculator;
        public StringCalculatorShould()
        {
            _stringCalculator = new StringCalculator();
        }
        [Theory]
        [InlineData("")]
        public void Return_0(string input)
        {
            //Act
            int result = _stringCalculator.Add(input);

            //Assert
            result.Should().Be(0);
        }
        [Theory]
        [InlineData("1")]
        [InlineData("3")]
        [InlineData("99")]
        public void Return_Input_Number(string input)
        {
            //Act
            int result = _stringCalculator.Add(input);

            //Assert
            result.Should().Be(Convert.ToInt32(input));
        }
        [Theory]
        [InlineData("1,4", 5)]
        [InlineData("3,9", 12)]
        [InlineData("99,50", 149)]
        [InlineData("1,56,44", 101)]
        public void Return_Addition_Of_Comma_Seperated_Input_Numbers(string input, int output)
        {
            //Act
            int result = _stringCalculator.Add(input);

            //Assert
            result.Should().Be(output);
        }
        [Theory]
        [InlineData("1\n4\n1", 6)]
        [InlineData("3\n9\n5", 17)]
        [InlineData("99,50\n1", 150)]
        [InlineData("1\n56,44", 101)]
        public void Return_Addition_Of_Comma_Or_New_Line_Seperated_Input_Numbers(string input, int output)
        {
            //Act
            int result = _stringCalculator.Add(input);

            //Assert
            result.Should().Be(output);
        }
        [Theory]
        [InlineData("1\n4,\n1")]
        [InlineData("1\n,1")]
        public void Handle_Invalid_Input(string input)
        {
            //Act
            Action act = () => _stringCalculator.Add(input);

            //Assert
            act.Should().Throw<ArgumentException>()
            .WithMessage("*Invalid Input*");
        }
        [Theory]
        [InlineData("//;\n1;3", 4)]
        [InlineData("//(\n4(3", 7)]
        [InlineData("//||\n4||3||9", 16)]
        public void Return_Addition_With_Custom_Delimiters(string input, int output)
        {
            //Act
            int result = _stringCalculator.Add(input);

            //Assert
            result.Should().Be(output);
        }

        [Theory]
        [InlineData("1\n4\n-1", "-1")]
        [InlineData("//||\n-4||3||-9", "-4,-9")]
        public void Handle_Negative_Input(string input,string negativeNumbers)
        {
            //Act
            Action act = () => _stringCalculator.Add(input);

            //Assert
            act.Should().Throw<ArgumentException>()
            .WithMessage("*negatives not allowed*" + negativeNumbers + "*");
        }
    }
    public class StringCalculator
    {
        public int Add(string input)
        {
            int sum = 0;
            string[]? separators = null;
            List<int> negativeNumbers = new List<int>();

            if (input == string.Empty)
            {
                return 0;
            }

            // Check if the input starts with a custom delimiter
            if (input.StartsWith("//"))
            {
                // Find the position of the first new line character
                int firstOccuranceOfNewLine = input.IndexOf('\n');

                // Extract the custom delimiter from the input
                string delimeter = input.Substring(2, firstOccuranceOfNewLine - 2);
                separators = [delimeter];

                // Update the input string to remove custom delimiter
                input = input.Substring(firstOccuranceOfNewLine + 1);
            }
            else
            {
                // Use default separators: new line and comma
                separators = ["\n", ","];
            }

            var inputArray = input.Split(separators, StringSplitOptions.None);
            foreach (var element in inputArray)
            {
                if (!int.TryParse(element, out int intValue))
                {
                    throw new ArgumentException("Invalid Input");
                }
                if(intValue < 0)
                {
                    negativeNumbers.Add(intValue);
                    continue; // Skip adding to sum for negative numbers
                }
                sum += intValue;
            }

            if (negativeNumbers.Any())
            {
                // Create a comma-separated string of negative numbers
                string commaSeperatedNumbers = string.Join(",", negativeNumbers);
                throw new ArgumentException("negatives not allowed :" + commaSeperatedNumbers);
            }
            return sum;
        }
    }
}
