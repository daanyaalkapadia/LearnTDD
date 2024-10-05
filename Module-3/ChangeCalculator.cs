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
        [MemberData(nameof(ChangeTestData))]
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
        public static IEnumerable<object[]> ChangeTestData()
        {
            yield return new object[] { 0.1f, 0.1f, new float[] { 0.0f } };
            yield return new object[] { 0.02f, 0.01f, new float[] { 0.01f } };

        }
    }
    public class ChangeCalculator
    {
        internal List<float> GetChange(float given, float toPay)
        {
            List<float> result = new List<float>();
            float change = given - toPay;
            if(change > 0)
            {
                result.Add(change);
            }
            return result;
        }
    }
}
