namespace LearnTDD.Module_6
{
    public interface IRandomGenerator { int GetRandomBetween1And100(); }
    public class RandomGenerator : IRandomGenerator
    {
        private static Random _random = new Random(); 
        public int GetRandomBetween1And100()
        {
            return _random.Next(1, 101); // Returns a random number between 1 and 100 } }
        }
    }
}
