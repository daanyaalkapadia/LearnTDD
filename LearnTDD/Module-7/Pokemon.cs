namespace LearnTDD.Module_7
{
    public enum PokemonType { Fire, Water, Grass, Rock }

    public enum PokemonClassification { FilthyCasual = 1, GodTier = 2 }

    public class Pokemon
    {
        public string Name { get; set; }
        public PokemonType Type { get; set; }
    }
}