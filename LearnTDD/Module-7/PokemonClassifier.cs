using FluentAssertions;
using Moq;

namespace LearnTDD.Module_7
{
    public class PokemonClassifierShould
    {
        private PokemonClassifier _pokemonClassifier;
        private Mock<IPokemonAPI> _pokemonAPI;
        public PokemonClassifierShould()
        {
            _pokemonAPI = new Mock<IPokemonAPI>();
            _pokemonClassifier = new PokemonClassifier(_pokemonAPI.Object);
        }
        [Theory]
        [MemberData(nameof(ChangeTestData))]
        public void Classify_Pokemon(int pokemonId, Pokemon pokemon, PokemonClassification expectedclassification)
        {
            _pokemonAPI.Setup(x => x.GetPokemonById(pokemonId)).Returns(pokemon);
            var actualclassification = _pokemonClassifier.Classify(pokemonId);

            _pokemonAPI.Verify(x => x.GetPokemonById(pokemonId), Times.Once);
            
            actualclassification.Should().Be(expectedclassification);
        }
        public static IEnumerable<object[]> ChangeTestData()
        {
            yield return new object[] { 1, new Pokemon() { Name = "Charmander", Type = PokemonType.Fire }, PokemonClassification.GodTier };
            yield return new object[] { 2, new Pokemon() { Name = "Squirtle", Type = PokemonType.Water }, PokemonClassification.FilthyCasual };
        }
    }
    public class PokemonClassifier
    {
        private IPokemonAPI _pokemonAPI;
        public PokemonClassifier(IPokemonAPI pokemonAPI)
        {
            _pokemonAPI = pokemonAPI;
        }
        public PokemonClassification Classify(int pokemonId)
        {
            var pokemon = _pokemonAPI.GetPokemonById(pokemonId);
            if (pokemon.Type == PokemonType.Fire)
            {
                return PokemonClassification.GodTier;
            }
            return PokemonClassification.FilthyCasual;
        }
    }
}
