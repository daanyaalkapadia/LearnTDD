using FluentAssertions;
using LearnTDD.Module_2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnTDD.Module_3
{
    public class ChangeCalculatorShould
    {
        private ChangeCalculator _changeCalculator;
        public ChangeCalculatorShould()
        {
            _changeCalculator = new ChangeCalculator();
        }
        [Theory]
        [InlineData(0.1,0.1)]
        public void ReturnChange(float given, float toPay, params float[] change)
        {
            //Act
            List<float> result = _changeCalculator.GetChange(given, toPay);

            //Assert
            for (int i = 0; i < result.Count; i++) 
            {
                result[i].Should().Be(change[i]);
            }
        }
    }
    public class ChangeCalculator
    {
        internal List<float> GetChange(float given, float toPay)
        {
            List<float> result = new List<float>();
            return result;
        }
    }
}
