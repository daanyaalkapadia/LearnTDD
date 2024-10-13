using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnTDD.Module_6
{
    public class OddOrEvenDetectorShould
    {
        [Fact]
        public void ReturnIsRandomNumberOdd()
        {
            var randomGeneratorMock = new Mock<IRandomGenerator>();
            var oddDectector = new OddOrEvenDetector(randomGeneratorMock.Object);
            oddDectector.IsRandomNumberOdd();
            randomGeneratorMock.Verify(x => x.GetRandomBetween1And100(), Times.Once);
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
            if(number % 2 == 0)
            {
                return true;
            }
            return false;
        }
    }

}
