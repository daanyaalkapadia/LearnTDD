using FluentAssertions;
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
        public PokemonClassifierShould() 
        {
            _pokemonClassifier = new PokemonClassifier();
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
        public PokemonClassification Classify(int pokemonId)
        {
            return PokemonClassification.GodTier;
        }
    }
}
