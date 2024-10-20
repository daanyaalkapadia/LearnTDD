using FluentAssertions;

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
            yield return new object[] { 0.1, 0.1, new float[] { 0.0f } };
            yield return new object[] { 0.02, 0.01, new float[] { 0.01f } };
            yield return new object[] { 0.03, 0.01, new float[] { 0.01f, 0.01f } };
            yield return new object[] { 0.04, 0.01, new float[] { 0.01f, 0.01f, 0.01f } };
            yield return new object[] { 0.05, 0.01, new float[] { 0.01f, 0.01f, 0.01f, 0.01f } };
            yield return new object[] { 0.06, 0.01, new float[] { 0.05f } };
            yield return new object[] { 0.07, 0.01, new float[] { 0.05f, 0.01f } };
            yield return new object[] { 0.08, 0.01, new float[] { 0.05f, 0.01f, 0.01f } };
            yield return new object[] { 0.09, 0.01, new float[] { 0.05f, 0.01f, 0.01f, 0.01f } };
            yield return new object[] { 0.10, 0.01, new float[] { 0.05f, 0.01f, 0.01f, 0.01f, 0.01f } };
            yield return new object[] { 0.11, 0.01, new float[] { 0.10f } };
            yield return new object[] { 0.20, 0.01, new float[] { 0.10f, 0.05f, 0.01f, 0.01f, 0.01f, 0.01f } };
            yield return new object[] { 0.21, 0.01, new float[] { 0.10f, 0.10f } };
            yield return new object[] { 0.26, 0.01, new float[] { 0.25f } };
            yield return new object[] { 0.41, 0.01, new float[] { 0.25f, 0.10f, 0.05f } };
            yield return new object[] { 0.51, 0.01, new float[] { 0.50f } };
            yield return new object[] { 0.51, 0.01, new float[] { 0.50f } };
            yield return new object[] { 1.1, 0.1, new float[] { 1 } };
            yield return new object[] { 5.1, 0.1, new float[] { 5 } };
            yield return new object[] { 10.1, 0.1, new float[] { 10 } };
            yield return new object[] { 20.1, 0.1, new float[] { 20 } };
            yield return new object[] { 50.1, 0.1, new float[] { 50 } };
            yield return new object[] { 100.1, 0.1, new float[] { 100 } };
            yield return new object[] { 500, 224.99, new float[] { 100, 100, 50, 25, 0.01f } };
            yield return new object[] { 500, 250, new float[] { 100, 100, 50 } };
        }
    }
    public class ChangeCalculator
    {
        List<float> denomination = new List<float>() { 100f, 50f, 20f, 10f, 5f, 1f, 0.50f, 0.25f, 0.10f, 0.05f, 0.01f };
        internal List<float> GetChange(float given, float toPay)
        {
            List<float> result = new List<float>();
            float change = (float)Math.Round(given - toPay, 2);
            if (change == 0)
            {
                return result;
            }
            if (denomination.Any(x => x >= change))
            {
                float denominationValue = denomination.FirstOrDefault(x => x <= change);
                result.Add(denominationValue);
                change = (float)Math.Round(change - denominationValue, 2);
                result.AddRange(GetChange(change + change, change));
            }
            return result;
        }
    }
}
