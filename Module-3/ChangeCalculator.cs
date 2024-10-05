using FluentAssertions;
using LearnTDD.Module_2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            yield return new object[] { 0.03f, 0.01f, new float[] { 0.01f, 0.01f } };
            yield return new object[] { 0.04f, 0.01f, new float[] { 0.01f, 0.01f, 0.01f } };
            yield return new object[] { 0.05f, 0.01f, new float[] { 0.01f, 0.01f, 0.01f, 0.01f } };
            yield return new object[] { 0.06f, 0.01f, new float[] { 0.05f } };
            yield return new object[] { 0.07f, 0.01f, new float[] { 0.05f, 0.01f } };
            yield return new object[] { 0.08f, 0.01f, new float[] { 0.05f, 0.01f, 0.01f } };
            yield return new object[] { 0.09f, 0.01f, new float[] { 0.05f, 0.01f, 0.01f, 0.01f } };
            yield return new object[] { 0.10f, 0.01f, new float[] { 0.05f, 0.01f, 0.01f, 0.01f, 0.01f } };
        }
    }
    public class ChangeCalculator
    {
        internal List<float> GetChange(float given, float toPay)
        {
            List<float> result = new List<float>();
            float change = (float)Math.Round(given - toPay, 2);
            if(change >= 0.05)
            {
                result.Add(0.05f);
                change = change - 0.05f;
            }
            while(change > 0)
            {
                result.Add(0.01f);
                change = (float)Math.Round(change - 0.01f, 2);
            }
            return result;
        }
    }
}
