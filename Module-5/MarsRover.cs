﻿using FluentAssertions;
using LearnTDD.Module_4;
using Microsoft.VisualStudio.TestPlatform.Utilities;

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
        [Theory]
        [MemberData(nameof(ChangeTestDataForValidInput))]
        public void Return_Valid_Result_For_Happy_Case(string position, string command,string output)
        {
            Action act = () => _marsRover.Drive(position, command);
            string result = _marsRover.Drive(position, command);

            result.Should().Be(output);
        }
        public static IEnumerable<object[]> ChangeTestDataForValidInput()
        {
            yield return new object[] { "N,0,0", "", "N,0,0" };
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
            if(position =="N,0,0" && command == "")
            {
                return position;
            }
            return string.Empty;
        }
    }
}
