﻿using FluentAssertions;
using Moq;

namespace LearnTDD.Module_6
{
    public class OddOrEvenDetectorShould
    {
        private Mock<IRandomGenerator> _randomGenerator;
        private OddOrEvenDetector _oddOrEvenDetector;
        public OddOrEvenDetectorShould()
        {
            _randomGenerator = new Mock<IRandomGenerator>();
            _oddOrEvenDetector = new OddOrEvenDetector(_randomGenerator.Object);
        }
        [Fact]
        public void GetRandomBetween1And100ShouldBeCalled()
        {
            _oddOrEvenDetector.IsRandomNumberOdd();
            _randomGenerator.Verify(x => x.GetRandomBetween1And100(), Times.Once);
        }
        [Theory]
        [InlineData(1, true)]
        [InlineData(3, true)]
        [InlineData(4, false)]
        [InlineData(8, false)]
        public void Return_IsRandomNumberOdd(int input, bool output)
        {
            _randomGenerator.Setup(x => x.GetRandomBetween1And100()).Returns(input);
            var result = _oddOrEvenDetector.IsRandomNumberOdd();
            result.Should().Be(output);
        }
    }
    public class OddOrEvenDetector
    {
        private IRandomGenerator _randomGenerator;
        public OddOrEvenDetector(IRandomGenerator randomGenerator)
        {
            _randomGenerator = randomGenerator;
        }

        public bool IsRandomNumberOdd()
        {
            var number = _randomGenerator.GetRandomBetween1And100();

            return number % 2 == 0 ? false : true;
        }
    }

}
