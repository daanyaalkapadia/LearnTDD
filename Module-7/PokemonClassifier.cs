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
        [Fact]
        public void Classify_Pokemon()
        {
            var classification = _pokemonClassifier.Classify(1);
            classification.Should().Be(PokemonClassification.GodTier);
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
