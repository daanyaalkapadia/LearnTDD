using FluentAssertions;
using LearnTDD.Module_6;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnTDD.Module_7
{
    public class PokemonClassifierShould
    {
        private PokemonClassifier _pokemonClassifier;
        private Mock<IPokemonAPI> _pokemonAPI;
        public PokemonClassifierShould() 
        {
            _pokemonClassifier = new PokemonClassifier();
            _pokemonAPI = new Mock<IPokemonAPI>();
        }
        [Theory]
        [InlineData(1, PokemonClassification.GodTier)]
        public void Classify_Pokemon(int pokemonId, PokemonClassification expectedclassification)
        {
            _pokemonAPI.Setup(x => x.GetPokemonById(pokemonId)).Returns(new Pokemon() { Name = "Charmander", Type = PokemonType.Fire });
            var actualclassification = _pokemonClassifier.Classify(pokemonId);
            actualclassification.Should().Be(expectedclassification);
        }
    }
    public class PokemonClassifier
    {
        private IPokemonAPI _pokemonAPI;
        public PokemonClassifier() 
        {
            _pokemonAPI = new PokemonAPI();
        }
        public PokemonClassification Classify(int pokemonId)
        {
            var pokemon = _pokemonAPI.GetPokemonById(pokemonId);
            if(pokemon.Type == PokemonType.Fire)
            {
                return PokemonClassification.GodTier;
            }
            return PokemonClassification.FilthyCasual;
        }
    }
}
